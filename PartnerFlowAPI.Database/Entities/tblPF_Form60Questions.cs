using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerFlowAPI.Entities
{
    public class tblPF_Form60Questions
    {
        [Key]
        public int intForm60Id { get; set; }

        [MaxLength(200)]
        [Required]
        public string vcApplicationNumber { get; set; } = null!;

        public int intAssureType { get; set; }

        public bool? btAppliedForPan { get; set; }

        [MaxLength(510)]
        public string? vcIdentityDocument { get; set; }

        [MaxLength(510)]
        public string? vcIdentityDocumentCode { get; set; }

        [MaxLength(510)]
        public string? vcIdentityDocumentNumber { get; set; }

        [MaxLength(510)]
        public string? vcDocumentAddressLine1 { get; set; }

        [MaxLength(510)]
        public string? vcDocumentAddressLine2 { get; set; }

        [MaxLength(510)]
        public string? vcDocumentCountry { get; set; }

        [MaxLength(510)]
        public string? vcDocumentState { get; set; }

        [MaxLength(510)]
        public string? vcDocumentCity { get; set; }

        [MaxLength(510)]
        public string? vcDocumentPincode { get; set; }

        [MaxLength(510)]
        public string? vcSupportOfAddressDocument { get; set; }

        [MaxLength(510)]
        public string? vcSupportOfAddressDocumentCode { get; set; }

        [MaxLength(510)]
        public string? vcSupportOfAddressIdentificationNumber { get; set; }

        [MaxLength(510)]
        public string? vcIssuingDocumentAddressLine1 { get; set; }

        [MaxLength(510)]
        public string? vcIssuingDocumentAddressLine2 { get; set; }

        [MaxLength(510)]
        public string? vcIssuingDocumentCountry { get; set; }

        [MaxLength(510)]
        public string? vcIssuingDocumentState { get; set; }

        [MaxLength(510)]
        public string? vcIssuingDocumentCity { get; set; }

        [MaxLength(510)]
        public string? vcIssuingDocumentPincode { get; set; }

        [MaxLength(100)]
        [Required]
        public string vcLastAccessIP { get; set; } = null!;

        [MaxLength(100)]
        [Required]
        public string vcCreatedBy { get; set; } = null!;

        [Required]
        public DateTime dtCreateDate { get; set; }

        [MaxLength(100)]
        public string? vcModifiedBy { get; set; }

        public DateTime? dtModifiedDate { get; set; }

        public DateTime? dtDeletedDate { get; set; }

        [Required]
        public bool bitIsDeleted { get; set; }

        [MaxLength(200)]
        public string? vcNameOfDocumentIssuer { get; set; }

        [MaxLength(200)]
        public string? vcSupportOfNameOfDocumentIssuer { get; set; }

        [MaxLength(20)]
        public string? vcAgriculturalIncome { get; set; }

        [MaxLength(20)]
        public string? vcOtherAgriculturalIncome { get; set; }

        public DateTime? dtDateOfPanApplication { get; set; }

        [MaxLength(40)]
        public string? vcAcknowledgementNumber { get; set; }
    }
}
