using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ProposalFromService.Application.Configuration.Data;
using ProposalFromService.Application.Features.DocumentRequired.Model;
using ProposalFromService.Application.Model;

namespace ProposalFromService.Application.Features.DocumentRequired.Query
{
    public class GetDocumentsByApplicationNumberQuery : IRequest<DocumentModel>
    {
        public GetDocumentListRequest GetDocumentListRequest { get; set; }

        public class GetDocumentsByApplicationNumberQueryHandler : IRequestHandler<GetDocumentsByApplicationNumberQuery, DocumentModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IWebHostEnvironment _env;

            public GetDocumentsByApplicationNumberQueryHandler(IApplicationDbContext context, IWebHostEnvironment env)
            {
                _context = context;
                _env = env;
            }

            public async Task<DocumentModel> Handle(GetDocumentsByApplicationNumberQuery request, CancellationToken cancellationToken)
            {
                bool application = await _context.AppTBLPF_Applications.AsNoTracking().AnyAsync(a => a.vcApplicationNumber == request.GetDocumentListRequest.ApplicationNumber
                                  && request.GetDocumentListRequest.Stage == "2");

                IQueryable<DocList> query = (from _doc in _context.AppTblPF_DocumentRequired.AsNoTracking()
                                             join _mstDOC in _context.AppDocumentType on _doc.IntDocumentTypeID equals _mstDOC.IntDocumentTypeID
                                             join _phr in _context.AppTblPHRequirement
                                             on _doc.IntPHRequirementID equals _phr.IntPhrequirementId into joined
                                             from _docLeftphr in joined.DefaultIfEmpty()
                                             where _doc.VcApplicationNumber == request.GetDocumentListRequest.ApplicationNumber
                                             select new DocList
                                             {
                                                 docrequirementid = _doc.IntPFDocumentRequiredID,
                                                 AssureType = (int)_doc.IntAssureType,
                                                 DocumentTypeId = _doc.IntDocumentTypeID,
                                                 SubType = _doc.IntSubTypeID,
                                                 DMSName = _mstDOC.vcDMSName,
                                                 DocumentName = _doc.VcDocumentFileName,
                                                 DocumentType = _doc.VcDocumentFileType,
                                                 Comment = _doc.vcRequirementComment,
                                                 DocComment = _doc.VcDocumentComment,
                                                 FileUrl = _doc.vcBlobFilePath,
                                                 Row = _doc.IntRow,
                                                 Resubmit = _doc.btResubmit,
                                                 Status = _doc.IntStatus,
                                                 Reason = (_docLeftphr != null) ? _docLeftphr.VcLookupData : null,
                                                 AutoGenrated = _doc.btAutoGenrated,
                                                 CreatedOn = _doc.dtCreateDate,
                                                 ModifiedOn = _doc.dtModifiedDate,
                                                 ResubmissionStatus = (int)_doc.intResubmissionStatus,
                                                 UWfollowupcode = _doc.vcUWFollowupCode,
                                                 UWrequirementID = _doc.intUWRequirementID,
                                                 IsForEditApp = _doc.btIsForEditApp
                                             }).AsQueryable();

                //if (!string.IsNullOrEmpty(request.GetDocumentListRequest.Stage) && (request.GetDocumentListRequest.Stage == "1"))
                //{
                //    query = query.Where(x => x.FileUrl != null);
                //}

                if (!string.IsNullOrEmpty(request.GetDocumentListRequest.Stage)
                    && (request.GetDocumentListRequest.Stage == "1"))
                {
                    query = query.Where(x => x.Resubmit == false && x.AutoGenrated == false);
                }

                if (application!)
                {
                    query = query.Where(x => x.Resubmit == true && x.ResubmissionStatus == 0);
                }

                if (request.GetDocumentListRequest.IsForEditApp)
                {
                    query = query.Where(x => x.IsForEditApp == true);
                }

                List<DocList> result = await query.OrderByDescending(x => x.CreatedOn).AsNoTracking().ToListAsync();

                //if (result.Count == 0) throw new KeyNotFoundException("No data found");
                return new DocumentModel
                {
                    ApplicationNumber = request.GetDocumentListRequest.ApplicationNumber,
                    Documents = result
                };
            }
        }
    }
}
