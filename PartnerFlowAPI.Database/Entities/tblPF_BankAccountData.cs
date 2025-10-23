using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Database.Entities
{
    public class tblPF_BankAccountData
    {
        
        [Key]
        public int IntBankAccountDataID { get; set; }

        [Required]
        [MaxLength(200)]
        public string VcApplicationNumber { get; set; } = null!;

        [Required]
        public int IntAssureType { get; set; }

        [Required]
        [MaxLength(200)]
        public string VcRequestAccountName { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string VcRequestAccountNumber { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string VcRequestAccountIFSC { get; set; } = null!;

        [MaxLength(200)]
        public string? VcResponseAccountName { get; set; }

        [MaxLength(200)]
        public string? VcResponseAccountNumber { get; set; }

        [MaxLength(200)]
        public string? VcResponseAccountIFSC { get; set; }

        [MaxLength(200)]
        public string? VcResponseBankResponse { get; set; }

        public bool? BtResponseBankTxnStatus { get; set; }

        [MaxLength(100)]
        public string? VcResponseBankRRN { get; set; }

        [MaxLength(100)]
        public string? VcResponseStatusCode { get; set; }

        public bool? BtResponseIsValid { get; set; }

        [MaxLength(100)]
        public string? VcResponseIdentifier { get; set; }

        public double? FtResponseNameMatchScore { get; set; }

        [MaxLength(100)]
        public string? VcResponseValidity { get; set; }

        [MaxLength(100)]
        public string? VcStatus { get; set; }

        [MaxLength(400)]
        public string? VcErrorDescription { get; set; }

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
    }
}
