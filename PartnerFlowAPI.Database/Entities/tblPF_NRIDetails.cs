using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerFlowAPI.Entities
{
    public class tblPF_NRIDetails
    {
        [Key]
        public int intNRIID { get; set; }

        [StringLength(200)]
        public string vcApplicationNumber { get; set; } = null!;

        public int? intAssureTypeID { get; set; }

        [StringLength(20)]
        public string? vcTitle { get; set; }

        [StringLength(510)]
        public string? vcFirstName { get; set; }

        [StringLength(510)]
        public string? vcMiddleName { get; set; }

        [StringLength(510)]
        public string? vcLastName { get; set; }

        [StringLength(510)]
        public string? vcNationality { get; set; }

        [StringLength(510)]
        public string? vcCurrentResidenceCountry { get; set; }

        [StringLength(510)]
        public string? vcCountryVisited { get; set; }

        public DateTime? dtDateOfLeaving { get; set; }

        public int? intIntentedDuration { get; set; }

        [StringLength(1000)]
        public string? vcAddressLine1 { get; set; }

        [StringLength(1000)]
        public string? vcAddressLine2 { get; set; }

        [StringLength(510)]
        public string? vcCountry { get; set; }

        [StringLength(510)]
        public string? vcState { get; set; }

        [StringLength(510)]
        public string? vcCity { get; set; }

        [StringLength(200)]
        public string? vcPincode { get; set; }

        [StringLength(510)]
        public string? vcPurposeofstaying { get; set; }

        [StringLength(200)]
        public string? vcPassportnumber { get; set; }

        [StringLength(510)]
        public string? vcDateofissue { get; set; }

        public DateTime? dtPassportvalidity { get; set; }

        [StringLength(510)]
        public string? vcRecententry { get; set; }

        [StringLength(510)]
        public string? vcCountryofresidence { get; set; }

        public DateTime? dtResidencedate { get; set; }

        [StringLength(510)]
        public string? vcResidentialstatusfortax { get; set; }

        [StringLength(200)]
        public string? vcPermanentAccount { get; set; }

        public bool? btDoyouNriAccount { get; set; }

        [StringLength(510)]
        public string? vcBankname { get; set; }

        [StringLength(510)]
        public string? vcBankAddressLine1 { get; set; }

        [StringLength(510)]
        public string? vcBankAddressLine2 { get; set; }

        [StringLength(510)]
        public string? vcBankcountry { get; set; }

        [StringLength(510)]
        public string? vcBankstate { get; set; }

        [StringLength(510)]
        public string? vcBankcity { get; set; }

        [StringLength(200)]
        public string? vcBankpincode { get; set; }

        [StringLength(100)]
        public string? vcTypeofaccount { get; set; }

        [StringLength(200)]
        public string? vcBankaccountnumber { get; set; }

        [StringLength(510)]
        public string? vcDispatchedname { get; set; }

        [StringLength(510)]
        public string? vcDispatchaddress1 { get; set; }

        [StringLength(510)]
        public string? vcDispatchaddress2 { get; set; }

        [StringLength(510)]
        public string? vcDispatchcountry { get; set; }

        [StringLength(510)]
        public string? vcDispatchstate { get; set; }

        [StringLength(510)]
        public string? vcDispatchcity { get; set; }

        [StringLength(200)]
        public string? vcDispatchpincode { get; set; }

        [StringLength(510)]
        public string? vcPaymentmanner { get; set; }

        [StringLength(200)]
        public string? vcEcsEnterBankName { get; set; }

        [StringLength(100)]
        public string vcLastAccessIP { get; set; } = null!;

        [StringLength(100)]
        public string vcCreatedBy { get; set; } = null!;

        public DateTime dtCreateDate { get; set; }

        [StringLength(100)]
        public string? vcModifiedBy { get; set; }

        public DateTime? dtModifiedDate { get; set; }
        public DateTime? dtDeletedDate { get; set; }

        public bool bitIsDeleted { get; set; } = false;
    }
}
