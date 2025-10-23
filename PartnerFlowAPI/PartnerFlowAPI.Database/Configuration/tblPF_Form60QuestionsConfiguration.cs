using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourNamespace.Entities;

namespace YourNamespace.Configurations
{
    public class tblPF_Form60QuestionsConfiguration : IEntityTypeConfiguration<tblPF_Form60Questions>
    {
        public void Configure(EntityTypeBuilder<tblPF_Form60Questions> entity)
        {
            entity.ToTable("tblPF_Form60Questions");

            entity.HasKey(e => e.intForm60Id)
                  .HasName("PK__tblPF_Fo__A533F07856C29B67");

            entity.HasIndex(e => e.vcApplicationNumber)
                  .HasDatabaseName("tblPF_Form60Questions_ApplicationNumber");

            entity.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                  .HasDatabaseName("tblPF_Form60Questions_IsDeleted_DeletedDate");

            // Column configurations
            entity.Property(e => e.intForm60Id).HasColumnName("intForm60Id");

            entity.Property(e => e.vcApplicationNumber)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.vcLastAccessIP)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.vcCreatedBy)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.dtCreateDate)
                  .HasColumnType("datetime2(7)");

            entity.Property(e => e.dtModifiedDate)
                  .HasColumnType("datetime2(7)");

            entity.Property(e => e.dtDeletedDate)
                  .HasColumnType("datetime2(7)");

            entity.Property(e => e.dtDateOfPanApplication)
                  .HasColumnType("datetime2(7)");
            entity.Property(e => e.bitIsDeleted);
            // All varchar(510) fields
            var nvarchar510 = new[]
            {
                nameof(tblPF_Form60Questions.vcIdentityDocument),
                nameof(tblPF_Form60Questions.vcIdentityDocumentCode),
                nameof(tblPF_Form60Questions.vcIdentityDocumentNumber),
                nameof(tblPF_Form60Questions.vcDocumentAddressLine1),
                nameof(tblPF_Form60Questions.vcDocumentAddressLine2),
                nameof(tblPF_Form60Questions.vcDocumentCountry),
                nameof(tblPF_Form60Questions.vcDocumentState),
                nameof(tblPF_Form60Questions.vcDocumentCity),
                nameof(tblPF_Form60Questions.vcDocumentPincode),
                nameof(tblPF_Form60Questions.vcSupportOfAddressDocument),
                nameof(tblPF_Form60Questions.vcSupportOfAddressDocumentCode),
                nameof(tblPF_Form60Questions.vcSupportOfAddressIdentificationNumber),
                nameof(tblPF_Form60Questions.vcIssuingDocumentAddressLine1),
                nameof(tblPF_Form60Questions.vcIssuingDocumentAddressLine2),
                nameof(tblPF_Form60Questions.vcIssuingDocumentCountry),
                nameof(tblPF_Form60Questions.vcIssuingDocumentState),
                nameof(tblPF_Form60Questions.vcIssuingDocumentCity),
                nameof(tblPF_Form60Questions.vcIssuingDocumentPincode)
            };

            foreach (var prop in nvarchar510)
                entity.Property(prop).HasMaxLength(510).IsUnicode();

            // smaller nvarchars
            entity.Property(e => e.vcAcknowledgementNumber).HasMaxLength(40);
            entity.Property(e => e.vcAgriculturalIncome).HasMaxLength(20);
            entity.Property(e => e.vcOtherAgriculturalIncome).HasMaxLength(20);
            entity.Property(e => e.vcNameOfDocumentIssuer).HasMaxLength(200);
            entity.Property(e => e.vcSupportOfNameOfDocumentIssuer).HasMaxLength(200);
        }
    }
}
