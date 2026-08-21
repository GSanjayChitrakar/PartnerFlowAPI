using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Database.Common
{
    public abstract class AuditEntity
    {
        public string vcLastAccessIP { get; set; }
        public string? vcCreatedBy { get; set; }
        public DateTime dtCreateDate { get; set; }
        public string? vcModifiedBy { get; set; } = null;
        public DateTime? dtModifiedDate { get; set; }
        public bool BitIsDeleted { get; set; }
        public DateTime? dtDeletedDate { get; set; }
    }
}
