using MediatR;
using Microsoft.EntityFrameworkCore;
using ProposalFormService.Domain.Entities.Document;
using ProposalFromService.Application.Configuration.Data;

namespace ProposalFromService.Application.Features.DocumentRequired.Query
{
    public class GetDocumentTypeQuery : IRequest<AppDocumentType>
    {
        public int? TypeId { get; set; }
        public int? SubType { get; set; }

        public class GetDocumentTypeQueryHandler : IRequestHandler<GetDocumentTypeQuery, AppDocumentType>
        {
            private readonly IApplicationDbContext _context;

            public GetDocumentTypeQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<AppDocumentType?> Handle(GetDocumentTypeQuery request, CancellationToken cancellationToken)
            {

                var query = _context.AppDocumentType.AsQueryable();

                if (request.SubType == null)
                {
                    query = query.Where(c => c.IntDocumentTypeID == request.TypeId);
                }
                else
                {
                    query = query.Where(c => c.IntDocumentTypeID == request.SubType);

                }
                return await query.AsNoTracking().FirstOrDefaultAsync();
            }
        }
    }
}
