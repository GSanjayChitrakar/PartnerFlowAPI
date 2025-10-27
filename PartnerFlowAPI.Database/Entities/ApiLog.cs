using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartnerFlowAPI.Database.Entities
{
    [Table("Tbl_PFA_ApiLogs")]
    public class ApiLog
    {
        [Key]
        [Column("LogId")]
        public long LogId { get; set; }

        [Column("CorrelationId")]
        public Guid CorrelationId { get; set; } = Guid.NewGuid();

        [Column("RequestTime")]
        public DateTime RequestTime { get; set; } = DateTime.UtcNow;

        [Column("ResponseTime")]
        public DateTime? ResponseTime { get; set; }

        [Column("DurationMs")]
        public int? DurationMs { get; set; }

        // Request details
        [Required]
        [MaxLength(10)]
        [Column("HttpMethod")]
        public string HttpMethod { get; set; } = string.Empty;

        [Required]
        [MaxLength(2048)]
        [Column("RequestUrl")]
        public string RequestUrl { get; set; } = string.Empty;

        [Column("RequestHeaders")]
        public string? RequestHeaders { get; set; }

        [Column("RequestBody")]
        public string? RequestBody { get; set; }

        // Response details
        [Column("StatusCode")]
        public int? StatusCode { get; set; }

        [Column("ResponseHeaders")]
        public string? ResponseHeaders { get; set; }

        [Column("ResponseBody")]
        public string? ResponseBody { get; set; }

        // Exception details
        [Column("ExceptionMessage")]
        public string? ExceptionMessage { get; set; }

        [Column("ExceptionStackTrace")]
        public string? ExceptionStackTrace { get; set; }

        // Extra metadata
        [MaxLength(50)]
        [Column("ClientIp")]
        public string? ClientIp { get; set; }

        [MaxLength(512)]
        [Column("UserAgent")]
        public string? UserAgent { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        
        [Column("IpAddress")]
        public string? IpAddress { get; set; }
    }
}
