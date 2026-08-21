using MediatR;
using Microsoft.EntityFrameworkCore;
using ProposalFormService.Domain.Entities.Document;
using ProposalFromService.Application.Configuration.Data;

namespace ProposalFromService.Application.Features.DocumentRequired.Query
{
    public class GetDocumentByTypeQuery : IRequest<AppTblPF_DocumentRequired>
    {
        public string Type { get; set; }
        public string ApplicationNumber { get; set; }
        public class GetDocumentByTypeQueryHandler : IRequestHandler<GetDocumentByTypeQuery, AppTblPF_DocumentRequired>
        {
            private readonly IApplicationDbContext _context;

            public GetDocumentByTypeQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<AppTblPF_DocumentRequired> Handle(GetDocumentByTypeQuery request, CancellationToken cancellationToken)
            {
                AppTblPF_DocumentRequired? doc = await (from _doc in _context.AppTblPF_DocumentRequired
                                                        join _type in _context.AppDocumentType
                                                        on _doc.IntDocumentTypeID equals _type.IntDocumentTypeID
                                                        where _type.VcDocumentTypeName.ToLower() == request.Type.ToLower()
                                                        && _doc.VcApplicationNumber == request.ApplicationNumber
                                                        && _doc.vcBlobFilePath != null
                                                        select _doc
                                                       )
                                                       .AsNoTracking()
                                                       .FirstOrDefaultAsync();
                return doc;
            }
        }
    }

}
