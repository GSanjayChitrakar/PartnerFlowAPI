using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Database.Entities
{
    public class MST_PFA_Field
    {
        public int FieldId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string DataType { get; set; } = string.Empty;
        public int? Length { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        // Navigation Property
        [NotMapped]
        public virtual ICollection<MST_PFA_SectionField> SectionFields { get; set; } = new HashSet<MST_PFA_SectionField>();
    }
}
