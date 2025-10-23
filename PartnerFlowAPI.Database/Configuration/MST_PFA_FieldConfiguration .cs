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
    public class MST_PFA_FieldConfiguration : IEntityTypeConfiguration<MST_PFA_Field>
    {
        public void Configure(EntityTypeBuilder<MST_PFA_Field> builder)
        {
            builder.ToTable("MST_PFA_Fields");

            builder.HasKey(f => f.FieldId)
                   .HasName("PK_MST_PFA_Fields");

            builder.Property(f => f.FieldId)
                   .ValueGeneratedOnAdd();

            builder.Property(f => f.FieldName)
                   .HasMaxLength(255)
                   .IsUnicode(true)
                   .IsRequired();

            builder.HasIndex(f => f.FieldName)
                   .IsUnique()
                   .HasDatabaseName("UQ_MST_PFA_Fields_FieldName");

            builder.Property(f => f.DataType)
                   .HasMaxLength(50)
                   .IsUnicode(true)
                   .IsRequired();

            builder.Property(f => f.Length)
                   .IsRequired(false);

            builder.Property(f => f.IsDeleted)
                   .HasDefaultValue(false)
                   .IsRequired();

            builder.Property(f => f.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.Property(f => f.CreatedBy)
                   .HasMaxLength(100)
                   .IsUnicode(true)
                   .IsRequired();

            builder.Property(f => f.ModifiedBy)
                   .HasMaxLength(100)
                   .IsUnicode(true);

            builder.Property(f => f.ModifiedAt)
                   .IsRequired(false);
        }
    }
}
