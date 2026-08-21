using FGLI_SharedLibrary.Abstractions;
using FGLI_SharedLibrary.Core.Common.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProposalFormService.Domain.Entities.Document;
using ProposalFromService.Application.Configuration.Data;
using ProposalFromService.Application.Model;

namespace ProposalFromService.Application.Features.DocumentRequired.Command
{
    public class DeleteDocumentCommand : IRequest<string>
    {
        public DeleteDocumentRequest DeleteDocumentRequest { get; set; }
        public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, string>
        {
            private readonly IApplicationDbContext _context;
            private readonly IDateTimeService _dateTimeService;
            public DeleteDocumentCommandHandler(IApplicationDbContext context, IDateTimeService dateTimeService)
            {
                _context = context;
                _dateTimeService = dateTimeService;
            }

            public async Task<string> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
            {
                AppTblPF_DocumentRequired? entity = await _context.AppTblPF_DocumentRequired
                                                    .Where(c => c.IntPFDocumentRequiredID == request.DeleteDocumentRequest.docrequirementid)
                                                     .FirstOrDefaultAsync();

                if (!Guard.IsNotNull(entity)) throw new KeyNotFoundException("No data found");

                if (entity.IntRow == 1)
                {
                    //null all details
                    entity.IntSubTypeID = null;
                    entity.VcDocumentFileName = null;
                    entity.VcDocumentFileType = null;
                    entity.VcDocumentComment = null;
                    entity.vcDMSFilePath = null;
                    entity.vcBlobFilePath = null;
                    entity.IntStatus = 0;
                    entity.vcDMSIdexNo = null;
                    entity.dtDMSDate = null;
                }
                else
                {
                    entity.BitIsDeleted = true;
                    entity.dtDeletedDate = _dateTimeService.Now;

                    List<AppTblPF_DocumentRequired>? ShiftRowentity = await _context.AppTblPF_DocumentRequired
                                                    .Where(c => c.IntPFDocumentRequiredID == request.DeleteDocumentRequest.docrequirementid)
                                                    .ToListAsync();
                    if (Guard.IsListNotNull(ShiftRowentity))
                    {
                        foreach (AppTblPF_DocumentRequired item in ShiftRowentity)
                        {
                            item.IntRow = item.IntRow - 1;
                        }
                    }
                }
                await _context.SaveChangesAsync(cancellationToken);
                return "Document deleted";
            }
        }
    }
}
