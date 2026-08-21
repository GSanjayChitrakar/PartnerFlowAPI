using FGLI_DS_SharedLibrary.Infrastructure.Extensions;
using FGLI_SharedLibrary.Abstractions;
using FGLI_SharedLibrary.Core.Common.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProposalFormService.Domain.Entities;
using ProposalFormService.Domain.Enums;
using ProposalFromService.Application.Configuration.Data;
using ProposalFromService.Application.Interfaces;

namespace ProposalFromService.Application.Features.DocumentRequired.Query
{
    public class ChangeApplicationStatusCommand : IRequest<string>
    {
        public string ApplicationNumber { get; set; }

        public class ChangeApplicationStatusCommandHandler : IRequestHandler<ChangeApplicationStatusCommand, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly ICurrentUserService _currentUserService;
            private readonly IApplicationMovementService _applicationMovementService;
            public ChangeApplicationStatusCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IApplicationMovementService applicationMovementService)
            {
                _context = context;
                _currentUserService = currentUserService;
                _applicationMovementService = applicationMovementService;
            }

            public async Task<string> Handle(ChangeApplicationStatusCommand request, CancellationToken cancellationToken)
            {
                int resubmissionStatus = 0;
                string result = "Failed";
                AppTBLPF_Applications? appTBLPF_Applications = null;
                List<AppTblPF_PersonalDetails>? appTBLPF_PersonalDetails = null;
                appTBLPF_Applications = await _context.AppTBLPF_Applications.Where(a => a.vcApplicationNumber == request.ApplicationNumber).FirstOrDefaultAsync();
                appTBLPF_PersonalDetails = await _context.AppTblPF_PersonalDetails.Where(a => a.VcApplicationNumber == request.ApplicationNumber).ToListAsync();

                if (!Guard.IsNotNull(appTBLPF_Applications)) throw new KeyNotFoundException("Invalid Application Number");

                ApplicationStatus currentStatus = appTBLPF_Applications.intApplicationStatus;

                appTBLPF_Applications.intImageQCStatus = ApplicationImageQCStatus.ResubmissionCompleted;
                resubmissionStatus = 1;

                if (appTBLPF_PersonalDetails.Count > 0)
                    appTBLPF_PersonalDetails.ForEach(x => x.intImageQCStatus = 1);



                if (appTBLPF_Applications != null)
                {
                    _context.AppTBLPF_Applications.Update(appTBLPF_Applications);

                    _context.AppTblPF_DocumentRequired
                        .Where(x => x.VcApplicationNumber == request.ApplicationNumber && x.btResubmit == true && x.intResubmissionStatus == 0 && x.IntDocumentTypeID != 9999)
                            .ExecuteUpdate(setters => setters
                                .SetProperty(x => x.intResubmissionStatus, resubmissionStatus)); // Set new value
                }


                int affectedEntity = await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                if (affectedEntity > 0)
                {
                    result = "Success";
                }
                await _applicationMovementService.AddApplicationMovement(new AppTblApplicationMovements
                {
                    vcApplicationNumber = request.ApplicationNumber,
                    vcUserIdFrom = _currentUserService.SafeUserId(),
                    vcUserIdTo = appTBLPF_Applications.vcAssignedTo,
                    vcMoveFromStage = "QC Requirement Fulfillment",
                    vcMoveToStage = "QC Requirement Completed",
                });


                return result;
            }
        }
    }

}
