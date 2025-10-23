using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourNamespace.Entities
{
    [Table("tblPF_EmploymentDetails")]
    public class tblPF_EmploymentDetails
    {
        [Key]
        public int intEmploymentDetailId { get; set; }

        [Required, MaxLength(200)]
        public string vcApplicationNumber { get; set; } = string.Empty;

        public int intAssureType { get; set; }

        [MaxLength(100)]
        public string? vcOccupation { get; set; }

        public bool btHazardousEnvironment { get; set; } = false;

        [MaxLength(200)]
        public string? vcWorkDomain { get; set; }

        [MaxLength(200)]
        public string? vcFirmOrEmployerName { get; set; }

        [MaxLength(200)]
        public string? vcNatureOfBusiness { get; set; }

        [MaxLength(200)]
        public string? vcNatureOfDuties { get; set; }

        [MaxLength(200)]
        public string? vcDesignation { get; set; }

        [Column(TypeName = "decimal(18,7)")]
        public decimal? dcAnnualIncome { get; set; }

        [MaxLength(510)]
        public string? vcBusinessAddress1 { get; set; }

        [MaxLength(510)]
        public string? vcBusinessAddress2 { get; set; }

        [MaxLength(510)]
        public string? vcBusinessAddress3 { get; set; }

        [MaxLength(510)]
        public string? vcBusinessRoadName { get; set; }

        [MaxLength(510)]
        public string? vcBusinessAddressLandmark { get; set; }

        [MaxLength(510)]
        public string? vcBusinessCity { get; set; }

        [MaxLength(510)]
        public string? vcBusinessState { get; set; }

        [MaxLength(510)]
        public string? vcBusinessCountry { get; set; }

        [MaxLength(510)]
        public string? vcBusinessPincode { get; set; }

        [MaxLength(100)]
        public string? vcStudingInClass { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? dcInsuranceCover { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? dcParentAnnualIncome { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? dcSiblingsInsuranceCover { get; set; }

        [MaxLength(100)]
        public string? vcLastAccessIP { get; set; }

        [MaxLength(100)]
        public string? vcCreatedBy { get; set; }

        public DateTime? dtCreateDate { get; set; }

        [MaxLength(100)]
        public string? vcModifiedBy { get; set; }

        public DateTime? dtModifiedDate { get; set; }

        public DateTime? dtDeletedDate { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool bitIsDeleted { get; set; } 
    }
}
