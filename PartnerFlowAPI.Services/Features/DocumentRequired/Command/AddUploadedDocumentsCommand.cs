using FGLI_SharedLibrary.Core.Common.ApplicationExceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PartnerFlowAPI.Database.Context;
using PartnerFlowAPI.Database.Entities;
using PartnerFlowAPI.Database.Enums;


namespace PartnerFlowAPI.Services.Features.DocumentRequired.Command
{
    public class AddUploadedDocumentsCommand : IRequest<string>
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
        public class AddUploadedDocumentsCommandHandler : IRequestHandler<AddUploadedDocumentsCommand, string>
        {
            private readonly ApplicationDbContext _context;

            public AddUploadedDocumentsCommandHandler(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<string> Handle(AddUploadedDocumentsCommand request, CancellationToken cancellationToken)
            {
                await Validate(request);
                //#region[Make Sure Old Document Will Be Deleted When New One Inserted]
                //var DocumentCleanOperation = await _context.AppTblPF_DocumentRequired.Where(x => x.VcApplicationNumber == request.ApplicationNumber && !x.BitIsDeleted && x.IntDocumentTypeID == request.DocumentTypeId)
                //                              .ExecuteUpdateAsync(x => x.SetProperty(prp => prp.BitIsDeleted , prp => true));
                //#endregion
                AppTblPF_DocumentRequired entity = new AppTblPF_DocumentRequired
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
                    btIsForEditApp = request.IsForEditApp,
                };

                if (request.UWRequirementID.HasValue)
                {
                    entity.intUWRequirementID = request.UWRequirementID.Value;
                    entity.vcUWFollowupCode = request.UWFollowupCode;
                }

                _context.appTblPF_DocumentRequireds.Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return "Document added.";
            }
            private async Task Validate(AddUploadedDocumentsCommand request)
            {
                bool IsDocumentExistWithApplication = await _context.appTblPF_DocumentRequireds
                                                                      .AnyAsync(c => c.VcApplicationNumber == request.ApplicationNumber);

                if (!IsDocumentExistWithApplication) throw new ApiException("No pending documents found");
            }
        }
    }
}
