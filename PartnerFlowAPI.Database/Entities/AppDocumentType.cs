using PartnerFlowAPI.Database.Common;
using PartnerFlowAPI.Database.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Database.Entities
{
    public class AppDocumentType : AuditEntity
    {
        public int IntDocumentTypeID { get; set; }
        public string VcDocumentTypeName { get; set; }
        public int intParentId { get; set; }
        public int? intCommonTypeId { get; set; }
        public bool BitIsMultiple { get; set; }
        public AssureType intAssureType { get; set; }
        public string? vcDMSName { get; set; }
    }
}
