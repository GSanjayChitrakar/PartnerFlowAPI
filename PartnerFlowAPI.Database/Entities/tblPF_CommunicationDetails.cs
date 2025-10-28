using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartnerFlowAPI.Domain.Entities
{
    [Table("tblPF_CommunicationDetails")]
    public class tblPF_CommunicationDetails
    {
        [Key]
        public int IntCommunicationId { get; set; }

        [Required]
        [MaxLength(200)]
        public string VcApplicationNumber { get; set; } = null!;

        [Required]
        public int IntAssureType { get; set; }

        [MaxLength(510)]
        public string? VcCAAddressLine1 { get; set; }

        [MaxLength(510)]
        public string? VcCAAddressLine2 { get; set; }

        [MaxLength(510)]
        public string? VcCAAddressLine3 { get; set; }

        [MaxLength(510)]
        public string? VcCALandmark { get; set; }

        [MaxLength(20)]
        public string? VcCAPincode { get; set; }

        [MaxLength(510)]
        public string? VcCACity { get; set; }

        [MaxLength(510)]
        public string? VcCAState { get; set; }

        [MaxLength(510)]
        public string? VcCACountry { get; set; }

        [Required]
        public bool BtIsPASameCA { get; set; }

        [MaxLength(510)]
        public string? VcPAAddressLine1 { get; set; }

        [MaxLength(510)]
        public string? VcPAAddressLine2 { get; set; }

        [MaxLength(510)]
        public string? VcPAAddressLine3 { get; set; }

        [MaxLength(510)]
        public string? VcPALandmark { get; set; }

        [MaxLength(20)]
        public string? VcPAPincode { get; set; }

        [MaxLength(510)]
        public string? VcPACity { get; set; }

        [MaxLength(510)]
        public string? VcPAState { get; set; }

        [MaxLength(510)]
        public string? VcPACountry { get; set; }

        [MaxLength(40)]
        public string? VcMobileNumber { get; set; }

        [MaxLength(40)]
        public string? VcAlternateNumber { get; set; }

        [MaxLength(40)]
        public string? VcWorkContactNumber { get; set; }

        [MaxLength(100)]
        public string? VcInterNationalNumber { get; set; }

        [MaxLength(510)]
        public string? VcEmailAddress { get; set; }

        [Required]
        public bool BtIsEditable { get; set; }

        [Required]
        [MaxLength(100)]
        public string VcLastAccessIP { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string VcCreatedBy { get; set; } = null!;

        [Required]
        public DateTime DtCreateDate { get; set; }

        [MaxLength(100)]
        public string? VcModifiedBy { get; set; }

        public DateTime? DtModifiedDate { get; set; }

        public DateTime? DtDeletedDate { get; set; }

        [Required]
        public bool BitIsDeleted { get; set; }

        public bool? BtKYCAddressUpdate { get; set; }
    }
}
