using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Models.Dtos
{
    public class ValidationResultModel
    {
        public bool Success { get; set; } = false;
        public List<string> InvalidFields { get; set; } = new List<string>();
        public string? ErrorMessage { get; set; }
    }

    public class FieldDefinition
    {
        public string FieldName { get; set; }
        public string DataType { get; set; }
        public int? Length { get; set; }
    }
}
