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
    public class MST_PFA_SectionFieldConfiguration : IEntityTypeConfiguration<MST_PFA_SectionField>
    {
        public void Configure(EntityTypeBuilder<MST_PFA_SectionField> builder)
        {
            builder.ToTable("MST_PFA_SectionFields");

            builder.HasKey(sf => sf.SectionFieldId)
                .HasName("PK_MST_PFA_SectionFields");

            builder.Property(sf => sf.SectionFieldId)
                .ValueGeneratedOnAdd();

            builder.Property(sf => sf.SectionId)
                .IsRequired();

            builder.Property(sf => sf.FieldId)
                .IsRequired();

            builder.Property(sf => sf.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(sf => sf.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(true)
                .IsRequired();

            builder.Property(sf => sf.ModifiedBy)
                .HasMaxLength(100)
                .IsUnicode(true);

            // Foreign key relationships
            builder.HasOne(sf => sf.Section)
                .WithMany()
                .HasForeignKey(sf => sf.SectionId)
                .HasConstraintName("FK_SectionFields_Section");

            builder.HasOne(sf => sf.Field)
                .WithMany()
                .HasForeignKey(sf => sf.FieldId)
                .HasConstraintName("FK_SectionFields_Field");
        }
    }
}
