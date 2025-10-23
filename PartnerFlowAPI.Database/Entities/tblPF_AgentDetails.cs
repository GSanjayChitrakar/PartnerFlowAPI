using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Database.Entities
{
    [Table("tblPF_AgentDetails")]
    public class tblPF_AgentDetails
    {
        [Key]
        public int IntAgentDetailID { get; set; }

        [MaxLength(100)]
        public string? VcApplicationNumber { get; set; }

        [MaxLength(100)]
        public string? AgentFirstName { get; set; }

        [MaxLength(100)]
        public string? AgentMiddleName { get; set; }

        [MaxLength(100)]
        public string? AgentLastName { get; set; }

        [MaxLength(200)]
        public string? AgentCode { get; set; }

        [MaxLength(200)]
        public string? ChannelCode { get; set; }

        [MaxLength(30)]
        public string? MobileNumber { get; set; }

        [MaxLength(200)]
        public string? BranchName { get; set; }

        [MaxLength(100)]
        public string? BranchCode { get; set; }

        [MaxLength(200)]
        public string? DesignationCode { get; set; }

        [MaxLength(1000)]
        public string? DesignationDescription { get; set; }

        [MaxLength(200)]
        public string? EmailAddress { get; set; }

        [MaxLength(200)]
        public string? MasterAgencyCode { get; set; }

        [MaxLength(200)]
        public string? Title { get; set; }

        [MaxLength(100)]
        public string? VcLastAccessIp { get; set; }

        public DateTime? DtCreateDate { get; set; }

        [MaxLength(100)]
        public string? VcModifiedBy { get; set; }

        public DateTime? DtModifiedDate { get; set; }

        public DateTime? DtDeletedDate { get; set; }

        [MaxLength(200)]
        public string? VcCreatedBy { get; set; }

        [Column("bitIsDeleted")]
        public bool BitIsDeleted { get; set; } = false;
    }
}
