using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Services.Configuration
{
    public class FileValidationOptions
    {
        public int MaxFileSizeMB { get; set; }
        public int MaxFileSizeBytes => MaxFileSizeMB * 1024 * 1024;
    }
}
