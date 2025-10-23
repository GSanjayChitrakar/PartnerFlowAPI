using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartnerFlowAPI.Domain.Entities
{
    [Table("tblpf_FatcaDetails")]
    public class tblpf_FatcaDetails
    {
        [Key]
        public int intFatcaId { get; set; }

        [Required, MaxLength(100)]
        public string vcApplicationNumber { get; set; } = string.Empty;

        public int intAssureType { get; set; }

        public bool? btFatca { get; set; }

        [MaxLength(510)]
        public string? vcFatcaAddressJurisdiction { get; set; }

        [MaxLength(510)]
        public string? vcFatcaTaxIdentificationNumber { get; set; }

        [MaxLength(510)]
        public string? vcFatcaValidityOfDocumentaryEvidence { get; set; }

        [MaxLength(510)]
        public string? vcFatcaTaxResidencyCountry { get; set; }

        [MaxLength(510)]
        public string? vcFatcaTINNumberIssuingCountry { get; set; }

        [Required, MaxLength(100)]
        public string vcLastAccessIP { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string vcCreatedBy { get; set; } = string.Empty;

        [Required]
        public DateTime dtCreateDate { get; set; }

        [MaxLength(100)]
        public string? vcModifiedBy { get; set; }

        public DateTime? dtModifiedDate { get; set; }

        public DateTime? dtDeletedDate { get; set; }

        [Required]
        public bool bitIsDeleted { get; set; } = false;
    }
}
