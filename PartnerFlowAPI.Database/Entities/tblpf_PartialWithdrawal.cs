using System;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Entities
{
    public class tblpf_PartialWithdrawal
    {
        [Key]
        public int intWithdrawalId { get; set; }

        [StringLength(100)]
        public string vcApplicationNumber { get; set; } = null!;

        public int intAssureType { get; set; }

        public bool? btIsSystematicWithdrawal { get; set; } = false;

        public DateTime? dtWithdrawalStartDate { get; set; }

        public decimal? dcWithdrawalMonthlyAmount { get; set; } 

        public int? intWithdrawalNumber { get; set; } = 0;

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
