using MediatR;
using Microsoft.EntityFrameworkCore;
using ProposalFormService.Domain.Enums;
using ProposalFromService.Application.Configuration.Data;
using ProposalFromService.Application.Features.DocumentRequired.Model;

namespace ProposalFromService.Application.Features.DocumentRequired.Query
{
    public class GetDocumentTypeByApplicationQuery : IRequest<DocumnetTypeModel>
    {
        public string ApplicationNumber { get; set; }
        public AssureType AssureType { get; set; }
        public class GetDocumentTypeByApplicationQueryHandler : IRequestHandler<GetDocumentTypeByApplicationQuery, DocumnetTypeModel>
        {
            private readonly IApplicationDbContext _context;

            public GetDocumentTypeByApplicationQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<DocumnetTypeModel> Handle(GetDocumentTypeByApplicationQuery request, CancellationToken cancellationToken)
            {
                DocumnetTypeModel res = new DocumnetTypeModel();
                List<DocTypeMode> DocTypeList = await (from _doctypes in _context.AppDocumentType
                                                       join _reqdoc in _context.AppTblPF_DocumentRequired
                                                       on _doctypes.IntDocumentTypeID equals _reqdoc.IntDocumentTypeID
                                                       where _reqdoc.VcApplicationNumber == request.ApplicationNumber &&
                                                       _doctypes.intAssureType == request.AssureType && _reqdoc.btAutoGenrated == false
                                                       select new DocTypeMode
                                                       {
                                                           TypeId = _doctypes.IntDocumentTypeID,
                                                           CommonTypeId = _doctypes.intCommonTypeId,
                                                           Name = _doctypes.VcDocumentTypeName,
                                                           IsMultiple = _doctypes.BitIsMultiple,
                                                           SubType = _context.AppDocumentType
                                                                        .Where(c => c.intParentId != 0
                                                                                && c.intParentId == _doctypes.IntDocumentTypeID
                                                                                && c.intAssureType == request.AssureType
                                                                            )
                                                                        .Select(s => new SubTypeModel
                                                                        {
                                                                            SubTypeId = s.IntDocumentTypeID,
                                                                            Name = s.VcDocumentTypeName
                                                                        })
                                                                        .ToList()
                                                       }
                                               )
                                               .GroupBy(s => s.TypeId)
                                               .Select(s => s.FirstOrDefault()!)
                                               .AsNoTracking()
                                               .ToListAsync();

                res.DocumentType = DocTypeList;
                return res;
            }
        }
    }
}
