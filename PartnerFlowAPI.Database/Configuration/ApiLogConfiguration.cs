using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerFlowAPI.Database.Entities;

namespace PartnerFlowAPI.Database.Configuration
{
    public class ApiLogConfiguration : IEntityTypeConfiguration<ApiLog>
    {
        public void Configure(EntityTypeBuilder<ApiLog> builder)
        {
            // Table mapping
            builder.ToTable("Tbl_PFA_ApiLogs");

            // Primary Key
            builder.HasKey(a => a.LogId);

            // Identity column
            builder.Property(a => a.LogId)
                   .HasColumnName("LogId")
                   .UseIdentityColumn()
                   .IsRequired();

            // Columns
            builder.Property(a => a.CorrelationId)
                   .HasColumnName("CorrelationId")
                   .HasDefaultValueSql("NEWID()")
                   .IsRequired();

            builder.Property(a => a.RequestTime)
                   .HasColumnName("RequestTime")
                   .HasDefaultValueSql("SYSUTCDATETIME()")
                   .IsRequired();

            builder.Property(a => a.ResponseTime)
                   .HasColumnName("ResponseTime");

            builder.Property(a => a.DurationMs)
                   .HasColumnName("DurationMs");

            builder.Property(a => a.HttpMethod)
                   .HasColumnName("HttpMethod")
                   .HasMaxLength(10)
                   .IsRequired()
                   .IsUnicode(false);

            builder.Property(a => a.RequestUrl)
                   .HasColumnName("RequestUrl")
                   .HasMaxLength(2048)
                   .IsRequired()
                   .IsUnicode(true);

            builder.Property(a => a.RequestHeaders)
                   .HasColumnName("RequestHeaders")
                   .IsUnicode(true);

            builder.Property(a => a.RequestBody)
                   .HasColumnName("RequestBody")
                   .IsUnicode(true);

            builder.Property(a => a.StatusCode)
                   .HasColumnName("StatusCode");

            builder.Property(a => a.ResponseHeaders)
                   .HasColumnName("ResponseHeaders")
                   .IsUnicode(true);

            builder.Property(a => a.ResponseBody)
                   .HasColumnName("ResponseBody")
                   .IsUnicode(true);

            builder.Property(a => a.ExceptionMessage)
                   .HasColumnName("ExceptionMessage")
                   .IsUnicode(true);

            builder.Property(a => a.ExceptionStackTrace)
                   .HasColumnName("ExceptionStackTrace")
                   .IsUnicode(true);

            builder.Property(a => a.IpAddress)
                .HasMaxLength(50)
                .HasColumnName("IpAddress");

            builder.Property(a => a.ClientIp)
                   .HasColumnName("ClientIp")
                   .HasMaxLength(50)
                   .IsUnicode(false);

            builder.Property(a => a.UserAgent)
                   .HasColumnName("UserAgent")
                   .HasMaxLength(512)
                   .IsUnicode(true);

            builder.Property(a => a.CreatedAt)
                   .HasColumnName("CreatedAt")
                   .HasDefaultValueSql("SYSUTCDATETIME()")
                   .IsRequired();

            // Indexes
            builder.HasIndex(a => a.RequestTime)
                   .HasDatabaseName("IX_ApiLogs_RequestTime");

            builder.HasIndex(a => a.StatusCode)
                   .HasDatabaseName("IX_ApiLogs_StatusCode");

            builder.HasIndex(a => a.CorrelationId)
                   .HasDatabaseName("IX_ApiLogs_CorrelationId");
        }
    }
}
