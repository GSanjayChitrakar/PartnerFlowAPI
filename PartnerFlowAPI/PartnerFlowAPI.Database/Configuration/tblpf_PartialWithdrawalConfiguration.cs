using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourNamespace.Entities;

namespace YourNamespace.Configurations
{
    public class tblpf_PartialWithdrawalConfiguration : IEntityTypeConfiguration<tblpf_PartialWithdrawal>
    {
        public void Configure(EntityTypeBuilder<tblpf_PartialWithdrawal> builder)
        {
            builder.ToTable("tblpf_PartialWithdrawal");

            builder.HasKey(e => e.intWithdrawalId)
                   .HasName("PK_tblpf_PartialWithdrawal");

            builder.HasIndex(e => e.vcApplicationNumber)
                   .HasDatabaseName("tblpf_PartialWithdrawal_ApplicationNumber");

            builder.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                   .HasDatabaseName("tblpf_PartialWithdrawal_IsDeleted_DeletedDate");

            builder.Property(e => e.intWithdrawalId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.vcApplicationNumber).HasMaxLength(100).IsRequired();
            builder.Property(e => e.intAssureType).IsRequired();
            builder.Property(e => e.btIsSystematicWithdrawal).HasDefaultValue(false);
            builder.Property(e => e.dtWithdrawalStartDate);
            builder.Property(e => e.dcWithdrawalMonthlyAmount).HasColumnType("decimal(18,2)");
            builder.Property(e => e.intWithdrawalNumber).HasDefaultValue(0);
            builder.Property(e => e.vcLastAccessIP).HasMaxLength(100).IsRequired();
            builder.Property(e => e.vcCreatedBy).HasMaxLength(100).IsRequired();
            builder.Property(e => e.dtCreateDate).IsRequired();
            builder.Property(e => e.vcModifiedBy).HasMaxLength(100);
            builder.Property(e => e.dtModifiedDate);
            builder.Property(e => e.dtDeletedDate);
            builder.Property(e => e.bitIsDeleted);
        }
    }
}
