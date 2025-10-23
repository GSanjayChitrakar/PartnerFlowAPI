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
    public class MST_PFA_SectionConfiguration : IEntityTypeConfiguration<MST_PFA_Section>
    {
        public void Configure(EntityTypeBuilder<MST_PFA_Section> builder)
        {
            builder.ToTable("MST_PFA_Section");

            builder.HasKey(s => s.SectionId);

            builder.Property(s => s.SectionName)
              .HasMaxLength(200)
             .IsRequired();

            builder.Property(s => s.SectionId)
                .ValueGeneratedOnAdd();

            builder.Property(s => s.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(s => s.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(s => s.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(true);

            builder.Property(s => s.ModifiedAt)
                .IsRequired(false);
        }
    }
}
