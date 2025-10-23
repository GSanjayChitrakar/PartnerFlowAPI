using Microsoft.EntityFrameworkCore;
using PartnerFlowAPI.Database.Context;
using PartnerFlowAPI.Database.Entities;
using PartnerFlowAPI.Services.Interfaces;
using System;

namespace PartnerFlowAPI.Services.Implementation
{
    public class FieldService : IFieldService
    {
        private readonly ApplicationDbContext _context;

        public FieldService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MST_PFA_Field>> GetFieldsByDataTypeAsync(string dataType)
        {
            // Fetch all columns, only non-deleted rows
            return await _context.Fields
                .Where(f => f.DataType == dataType && !f.IsDeleted)
                .ToListAsync();
        }
    }
}
