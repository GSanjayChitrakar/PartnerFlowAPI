using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerFlowAPI.Entities;

namespace PartnerFlowAPI.Configurations
{
    public class tblPF_HealthConditionsDetailsConfiguration : IEntityTypeConfiguration<tblPF_HealthConditionsDetails>
    {
        public void Configure(EntityTypeBuilder<tblPF_HealthConditionsDetails> entity)
        {
            entity.ToTable("tblPF_HealthConditionsDetails");

            entity.HasKey(e => e.intHealthConditionDetailsId)
                  .HasName("PK_tblPF_HealthDetailsCondition");

            entity.HasIndex(e => e.vcApplicationNumber)
                  .HasDatabaseName("tblPF_HealthConditionsDetails_ApplicationNumber");

            entity.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                  .HasDatabaseName("tblPF_HealthConditionsDetails_IsDeleted_DeletedDate");

            // Columns
            entity.Property(e => e.intHealthConditionDetailsId).HasColumnName("intHealthConditionDetailsId");
            entity.Property(e => e.vcApplicationNumber).IsRequired().HasMaxLength(200);
            entity.Property(e => e.intReflexQuestionTypeID);
            entity.Property(e => e.vcParentFieldName).HasMaxLength(1000);
            entity.Property(e => e.vcNameOfIllness).HasMaxLength(1000);
            entity.Property(e => e.dtFirstDiagnosis).HasColumnType("datetime2(7)");
            entity.Property(e => e.vcTreatmentDetails).HasMaxLength(1000);
            entity.Property(e => e.vcCurrentStatus).HasMaxLength(1000);
            entity.Property(e => e.btSameTreatment).HasColumnType("bit");
            entity.Property(e => e.btAnyCOmplecationConditions).HasColumnType("bit").HasDefaultValue(false);
            entity.Property(e => e.vcFollowUpAdvise).HasMaxLength(1000);
            entity.Property(e => e.vcAdditionalRemarks).HasMaxLength(1000);
            entity.Property(e => e.vcMedicineDosageDetails).HasMaxLength(1000);
            entity.Property(e => e.vcnameOfTreatingDoctor).HasMaxLength(1000);
            entity.Property(e => e.vcLastAccessIP).IsRequired().HasMaxLength(100);
            entity.Property(e => e.vcCreatedBy).IsRequired().HasMaxLength(100);
            entity.Property(e => e.dtCreateDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.vcModifiedBy).HasMaxLength(100);
            entity.Property(e => e.dtModifiedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtDeletedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.bitIsDeleted);
            entity.Property(e => e.btPregnancyDetailsDisorder).HasColumnType("bit");
        }
    }
}
