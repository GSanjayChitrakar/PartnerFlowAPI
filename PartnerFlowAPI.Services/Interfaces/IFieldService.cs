using PartnerFlowAPI.Database.Context;
using PartnerFlowAPI.Database.Entities;

namespace PartnerFlowAPI.Services.Interfaces
{
    public interface IFieldService
    {
        Task<IEnumerable<MST_PFA_Field>> GetFieldsByDataTypeAsync(string dataType);
    }
}
