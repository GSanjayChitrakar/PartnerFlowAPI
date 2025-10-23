using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Database.Entities
{
    public class MST_PFA_Section
    {
        public int SectionId { get; set; }
        public string? SectionName { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }

        // Navigation Property
        public virtual ICollection<MST_PFA_PartnerSection> PartnerSections { get; set; } = new HashSet<MST_PFA_PartnerSection>();
    }
}
