using System;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Entities
{
    public class Tblpf_SummaryDetails
    {
        [Key]
        public int intSummaryDeatils { get; set; }

        [StringLength(200)]
        public string? vcApplicationNumber { get; set; }

        [StringLength(8000)]
        public string? vcSummary { get; set; }

        [StringLength(100)]
        public string vcLastAccessIP { get; set; } = null!;

        [StringLength(100)]
        public string vcCreatedBy { get; set; } = null!;

        public DateTime dtCreateDate { get; set; }

        [StringLength(100)]
        public string? vcModifiedBy { get; set; }

        public DateTime? dtModifiedDate { get; set; }

        public DateTime? dtDeletedDate { get; set; }

        public bool bitIsDeleted { get; set; }
    }
}
