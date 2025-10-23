using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartnerFlowAPI.Database.Entities;

[Table("MST_PFA_Partner")]
public class MST_PFA_Partner
{
    [Key]
    [Column("PartnerID")]
    public int PartnerID { get; set; }
    
    [Required]
    [MaxLength(50)]
    [Column("Code")]
    public string Code { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(200)]
    [Column("Name")]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(1000)]
    [Column("APIKey")]
    public string? APIKey { get; set; }
    
    [Required]
    [MaxLength(1000)]
    [Column("RedirectionURL")]
    public string RedirectionURL { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(1000)]
    [Column("GCRedirectionURL")]
    public string GCRedirectionURL { get; set; } = string.Empty;
    
    [Column("IsActive")]
    public bool IsActive { get; set; }
    
    [Column("IsDeleted")]
    public bool IsDeleted { get; set; }
    
    [Column("DeletedDate")]
    public DateTime? DeletedDate { get; set; }
    
    [Required]
    [Column("CreatedDate")]
    public DateTime CreatedDate { get; set; }
    
    [Required]
    [MaxLength(50)]
    [Column("CreatedBy")]
    public string CreatedBy { get; set; } = string.Empty;
    
    [Column("ModifiedDate")]
    public DateTime? ModifiedDate { get; set; }
    
    [MaxLength(50)]
    [Column("ModifiedBy")]
    public string? ModifiedBy { get; set; }

    public string? PartnerSource { get; set; }

    public string? vcPartnerType { get; set; }
}