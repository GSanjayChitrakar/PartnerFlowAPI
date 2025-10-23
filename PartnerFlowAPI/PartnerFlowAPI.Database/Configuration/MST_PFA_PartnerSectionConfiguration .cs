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
    public class MST_PFA_PartnerSectionConfiguration : IEntityTypeConfiguration<MST_PFA_PartnerSection>
    {
        public void Configure(EntityTypeBuilder<MST_PFA_PartnerSection> builder)
        {
            builder.ToTable("MST_PFA_PartnerSection");

            builder.HasKey(ps => ps.PartnerSectionId);

            builder.Property(ps => ps.PartnerSectionId)
                .ValueGeneratedOnAdd();

            builder.Property(ps => ps.PartnerId)
                .IsRequired();

            builder.Property(ps => ps.SectionId)
                .IsRequired();

            builder.Property(ps => ps.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(ps => ps.CreatedBy)
                .HasMaxLength(100);

            builder.Property(ps => ps.ModifiedBy)
                .HasMaxLength(100);

            // Foreign keys
            builder.HasOne(ps => ps.Partner)
                .WithMany()
                .HasForeignKey(ps => ps.PartnerId)
                .HasConstraintName("FK_PartnerSection_Partner");

            builder.HasOne(ps => ps.Section)
                .WithMany()
                .HasForeignKey(ps => ps.SectionId)
                .HasConstraintName("FK_PartnerSection_Sections");
        }
    }
}
