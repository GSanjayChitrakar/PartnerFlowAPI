using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Models.Dtos
{
    public class CommonResultModel
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public List<SectionResultModel> SectionResults { get; set; } = new();
    }

    public class SectionResultModel
    {
        public string Section { get; set; }
        public bool Success { get; set; }
        public List<string>? InvalidFields { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
