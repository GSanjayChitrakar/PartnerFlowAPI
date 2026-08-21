using FGLI_SharedLibrary.Core.Common.ApplicationExceptions;
using FGLI_SharedLibrary.Core.Common.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProposalFormService.Domain.Entities.Document;
using ProposalFormService.Domain.Enums;
using ProposalFromService.Application.Configuration.Data;

namespace ProposalFromService.Application.Features.DocumentRequired.Command
{
    public class UpdateUploadedDocumentsCommand : IRequest<string>
    {
        public string ApplicationNumber { get; set; }
        public AssureType AssureType { get; set; }
        public string DMSURL { get; set; }
        public string BlobURL { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public int? DocumentTypeId { get; set; }
        public int? SubTypeId { get; set; }
        public string? Comment { get; set; }
        public int RowId { get; set; }
        public bool? IsResubmited { get; set; }
        public int? UWRequirementID { get; set; }
        public string? UWFollowupCode { get; set; }
        public bool? IsForEditApp { get; set; }
        public class UpdateUploadedDocumentsCommandHandler : IRequestHandler<UpdateUploadedDocumentsCommand, string>
        {
            private readonly IApplicationDbContext _context;
            string returnMessage = string.Empty;
            public UpdateUploadedDocumentsCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(UpdateUploadedDocumentsCommand request, CancellationToken cancellationToken)
            {
                // await Validate(request);
                var query = _context.AppTblPF_DocumentRequired.AsQueryable();

                // Common conditions
                query = query.Where(c =>
                    c.VcApplicationNumber == request.ApplicationNumber &&
                    c.IntAssureType == request.AssureType &&
                    c.btResubmit == request.IsResubmited &&
                    c.btAutoGenrated == false &&
                    c.IntRow == request.RowId &&
                    c.intResubmissionStatus != 1
                );

                // Conditional DocumentType / UWRequirement logic
                if (request.DocumentTypeId == 9999)
                {
                    query = query.Where(c => c.intUWRequirementID == request.UWRequirementID);
                }
                else
                {
                    query = query.Where(c => c.IntDocumentTypeID == request.DocumentTypeId);
                }

                // Final execution
                AppTblPF_DocumentRequired? entity = await query.FirstOrDefaultAsync();

                if (Guard.IsNotNull(entity))
                {
                    entity!.VcDocumentFileName = request.FileName;
                    entity.IntAssureType = request.AssureType;
                    entity.VcDocumentFileType = request.FileType;
                    entity.IntSubTypeID = request.SubTypeId;
                    entity.VcDocumentComment = request.Comment;
                    entity.vcDMSFilePath = request.DMSURL;
                    entity.vcBlobFilePath = request.BlobURL;
                    entity.IntStatus = 1;
                    if (request.UWRequirementID.HasValue)
                    {
                        entity.intUWRequirementID = request.UWRequirementID.Value;
                        entity.vcUWFollowupCode = request.UWFollowupCode;
                    }
                    entity.intResubmissionStatus = 0;
                    entity.btIsForEditApp = request.IsForEditApp;
                    returnMessage = "Document Updated";
                }
                else
                {
                    entity = new AppTblPF_DocumentRequired
                    {
                        IntAssureType = request.AssureType,
                        VcApplicationNumber = request.ApplicationNumber,
                        IntDocumentTypeID = (int)request.DocumentTypeId!,
                        IntSubTypeID = request.SubTypeId!,
                        VcDocumentFileName = request.FileName,
                        VcDocumentFileType = request.FileType,
                        VcDocumentComment = request.Comment,
                        vcDMSFilePath = request.DMSURL,
                        vcBlobFilePath = request.BlobURL,
                        IntRow = request.RowId,
                        btResubmit = request.IsResubmited,
                        IntStatus = 1,
                        intResubmissionStatus = 0,
                        btIsForEditApp = request.IsForEditApp
                    };

                    if (request.UWRequirementID.HasValue)
                    {
                        entity.intUWRequirementID = request.UWRequirementID.Value;
                        entity.vcUWFollowupCode = request.UWFollowupCode;
                    }

                    _context.AppTblPF_DocumentRequired.Add(entity);
                    returnMessage = "Document Added";
                }

                await _context.SaveChangesAsync(cancellationToken);
                return returnMessage;
            }

            private async Task Validate(UpdateUploadedDocumentsCommand request)
            {
                bool IsDocumentExistWithApplication = await _context.AppTblPF_DocumentRequired
                                                                      .AnyAsync(c => c.VcApplicationNumber == request.ApplicationNumber);

                if (!IsDocumentExistWithApplication) throw new ApiException("No pending documents found");
            }
        }
    }
}
