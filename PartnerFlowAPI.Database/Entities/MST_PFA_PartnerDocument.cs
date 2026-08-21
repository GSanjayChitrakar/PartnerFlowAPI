using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartnerFlowAPI.Database.Entities
{
    [Table("MST_PFA_PartnerDocument")]
    public class MST_PFA_PartnerDocument
    {
        [Key]
        [Column("intPartnerDocumentID")]
        public int PartnerDocumentID { get; set; }

        [Required]
        [Column("intPartnerID")]
        public int PartnerID { get; set; }

        [Required]
        [Column("intDocumentTypeID")]
        public int DocumentTypeID { get; set; }

        [MaxLength(50)]
        [Column("vcLastAccessIP")]
        public string? LastAccessIP { get; set; }

        [MaxLength(50)]
        [Column("vcCreatedBy")]
        public string? CreatedBy { get; set; }

        [Required]
        [Column("dtCreatedDate")]
        public DateTime CreatedDate { get; set; }

        [MaxLength(50)]
        [Column("vcModifiedBy")]
        public string? ModifiedBy { get; set; }

        [Column("dtModifiedDate")]
        public DateTime? ModifiedDate { get; set; }

        // Note: SQL shows column name "vdDeletedBy" — preserve that exact name in mapping
        [MaxLength(50)]
        [Column("vdDeletedBy")]
        public string? DeletedBy { get; set; }

        [Column("vcDeletedDate")]
        public DateTime? DeletedDate { get; set; }

        [Required]
        [Column("bitIsDeleted")]
        public bool IsDeleted { get; set; }
    }
}