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
    public class tblPF_AgentDetailConfiguration : IEntityTypeConfiguration<tblPF_AgentDetails>
    {
        public void Configure(EntityTypeBuilder<tblPF_AgentDetails> entity)
        {
            // Table mapping
            entity.ToTable("tblPF_AgentDetails");

            // Primary Key
            entity.HasKey(e => e.IntAgentDetailID)
                  .HasName("PK_tblPF_AgentDetails");

            // Indexes
            entity.HasIndex(e => e.VcApplicationNumber)
                  .HasDatabaseName("tblPF_AgentDetails_ApplicationNumber");

            entity.HasIndex(e => new { e.BitIsDeleted, e.DtDeletedDate })
                  .HasDatabaseName("tblPF_AgentDetails_IsDeleted_DeletedDate");

            // Property configurations
            entity.Property(e => e.IntAgentDetailID)
                  .ValueGeneratedOnAdd();

            entity.Property(e => e.VcApplicationNumber).HasMaxLength(100);
            entity.Property(e => e.AgentFirstName).HasMaxLength(100);
            entity.Property(e => e.AgentMiddleName).HasMaxLength(100);
            entity.Property(e => e.AgentLastName).HasMaxLength(100);
            entity.Property(e => e.AgentCode).HasMaxLength(200);
            entity.Property(e => e.ChannelCode).HasMaxLength(200);
            entity.Property(e => e.MobileNumber).HasMaxLength(30);
            entity.Property(e => e.BranchName).HasMaxLength(200);
            entity.Property(e => e.BranchCode).HasMaxLength(100);
            entity.Property(e => e.DesignationCode).HasMaxLength(200);
            entity.Property(e => e.DesignationDescription).HasMaxLength(1000);
            entity.Property(e => e.EmailAddress).HasMaxLength(200);
            entity.Property(e => e.MasterAgencyCode).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.VcLastAccessIp).HasMaxLength(100);
            entity.Property(e => e.VcModifiedBy).HasMaxLength(100);
            entity.Property(e => e.VcCreatedBy).HasMaxLength(200);

            entity.Property(e => e.BitIsDeleted);

            entity.Property(e => e.DtCreateDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.DtModifiedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.DtDeletedDate).HasColumnType("datetime2(7)");
        }
    }
}
