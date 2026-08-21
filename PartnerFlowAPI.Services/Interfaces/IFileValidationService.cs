using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Services.Interfaces
{
    public interface IFileValidationService
    {
        Task<(bool IsValid, string ErrorMessage)> ValidateAsync(string base64Data, string fileName);
    }
}
