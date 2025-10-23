using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Database.Entities
{
    public class MST_PFA_PartnerSection
    {
        public int PartnerSectionId { get; set; }
        public int PartnerId { get; set; }
        public int SectionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        // Navigation properties
        public MST_PFA_Partner? Partner { get; set; }
        public MST_PFA_Section? Section { get; set; }
    }
}
