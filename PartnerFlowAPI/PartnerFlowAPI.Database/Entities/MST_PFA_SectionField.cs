using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Database.Entities
{
    public class MST_PFA_SectionField
    {
        public int SectionFieldId { get; set; }
        public int SectionId { get; set; }
        public int FieldId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        // Navigation Properties
        public  MST_PFA_Section Section { get; set; }
        public  MST_PFA_Field Field { get; set; } 
    }
}
