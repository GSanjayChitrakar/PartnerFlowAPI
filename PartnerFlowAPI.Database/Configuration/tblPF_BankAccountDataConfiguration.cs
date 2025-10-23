using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerFlowAPI.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Database.Configuration
{
    public class tblPF_BankAccountDataConfiguration : IEntityTypeConfiguration<tblPF_BankAccountData>
    {
        public void Configure(EntityTypeBuilder<tblPF_BankAccountData> entity)
        {
            // Table mapping
            entity.ToTable("tblPF_BankAccountData");

            // Primary Key
            entity.HasKey(e => e.IntBankAccountDataID)
                  .HasName("PK_tblPF_BankAccountData");

            // Indexes
            entity.HasIndex(e => e.VcApplicationNumber)
                  .HasDatabaseName("tblPF_BankAccountData_ApplicationNumber");

            entity.HasIndex(e => new { e.BitIsDeleted, e.DtDeletedDate })
                  .HasDatabaseName("tblPF_BankAccountData_IsDeleted_DeletedDate");

            // Property configurations
            entity.Property(e => e.IntBankAccountDataID)
                  .ValueGeneratedOnAdd();

            entity.Property(e => e.VcApplicationNumber)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.IntAssureType)
                  .IsRequired();

            entity.Property(e => e.VcRequestAccountName)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.VcRequestAccountNumber)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.VcRequestAccountIFSC)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.VcResponseAccountName).HasMaxLength(200);
            entity.Property(e => e.VcResponseAccountNumber).HasMaxLength(200);
            entity.Property(e => e.VcResponseAccountIFSC).HasMaxLength(200);
            entity.Property(e => e.VcResponseBankResponse).HasMaxLength(200);
            entity.Property(e => e.BtResponseBankTxnStatus);
            entity.Property(e => e.VcResponseBankRRN).HasMaxLength(100);
            entity.Property(e => e.VcResponseStatusCode).HasMaxLength(100);
            entity.Property(e => e.BtResponseIsValid);
            entity.Property(e => e.VcResponseIdentifier).HasMaxLength(100);
            entity.Property(e => e.FtResponseNameMatchScore);
            entity.Property(e => e.VcResponseValidity).HasMaxLength(100);
            entity.Property(e => e.VcStatus).HasMaxLength(100);
            entity.Property(e => e.VcErrorDescription).HasMaxLength(400);

            entity.Property(e => e.VcLastAccessIP)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.VcCreatedBy)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.DtCreateDate)
                  .IsRequired()
                  .HasColumnType("datetime2(7)");

            entity.Property(e => e.VcModifiedBy).HasMaxLength(100);
            entity.Property(e => e.DtModifiedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.DtDeletedDate).HasColumnType("datetime2(7)");

            entity.Property(e => e.BitIsDeleted);
                  
        }
    }
}
