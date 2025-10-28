using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerFlowAPI.Entities;

namespace PartnerFlowAPI.Configurations
{
    public class tblPF_HealthConditionsConfiguration : IEntityTypeConfiguration<tblPF_HealthConditions>
    {
        public void Configure(EntityTypeBuilder<tblPF_HealthConditions> entity)
        {
            entity.ToTable("tblPF_HealthConditions");

            entity.HasKey(e => e.intHealthConditionId)
                  .HasName("PK_tblPF_HealthQuestion");

            entity.HasIndex(e => e.vcApplicationNumber)
                  .HasDatabaseName("tblPF_HealthConditions_ApplicationNumber");

            entity.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                  .HasDatabaseName("tblPF_HealthConditions_IsDeleted_DeletedDate");

            // Primary Key
            entity.Property(e => e.intHealthConditionId)
                  .HasColumnName("intHealthConditionId");

            // Required nvarchar fields
            entity.Property(e => e.vcApplicationNumber)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.vcLastAccessIP)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.vcCreatedBy)
                  .IsRequired()
                  .HasMaxLength(100);

            // Optional nvarchar fields
            entity.Property(e => e.vcWeightGainOrLost).HasMaxLength(100);
            entity.Property(e => e.vcCauseOfWeightIncrease).HasMaxLength(510);
            entity.Property(e => e.vcGynaecologicalProblemDetail).HasMaxLength(1000);
            entity.Property(e => e.vcSpouseSufferingHivOrHepatitisDetail).HasMaxLength(1000);
            entity.Property(e => e.vcExactTreatmentMedicalDetail).HasMaxLength(1000);
            entity.Property(e => e.vcFamilySufferAnyNeurologicalDisorderDetail).HasMaxLength(1000);
            entity.Property(e => e.vcDiagnosisOfAnyChestRelatedCheckupsDetail).HasMaxLength(1000);
            entity.Property(e => e.vcDiagnosisOfAnyChestRelatedCheckupsOngoing).HasMaxLength(1000);
            entity.Property(e => e.vcArthritisDiagnosis).HasMaxLength(1000);
            entity.Property(e => e.vcAffectedjoints).HasMaxLength(1000);
            entity.Property(e => e.vcInvestigationsTreatment).HasMaxLength(100);
            entity.Property(e => e.vcTreatmentDetails).HasMaxLength(510);
            entity.Property(e => e.CauseOfChange).HasMaxLength(1000);
            entity.Property(e => e.ProvideDetails).HasMaxLength(1000);
            entity.Property(e => e.ProvidePregencyDetails).HasMaxLength(1000);
            entity.Property(e => e.DurationInWeek).HasMaxLength(100);

            // Float fields
            entity.Property(e => e.ftHeightInCms).HasColumnType("float");
            entity.Property(e => e.ftHeightInInches).HasColumnType("float");
            entity.Property(e => e.ftHeightInFeet).HasColumnType("float");
            entity.Property(e => e.ftWeightinKgs).HasColumnType("float");
            entity.Property(e => e.ftWeightGainOrLostInKgs).HasColumnType("float");

            // Bit fields
            entity.Property(e => e.btWeightChange6M).HasColumnType("bit");
            entity.Property(e => e.btWeightChange1Year).HasColumnType("bit");
            entity.Property(e => e.btIsPregnent).HasColumnType("bit");
            entity.Property(e => e.btComplicationInPregnency).HasColumnType("bit");
            entity.Property(e => e.btGynaecologicalProblem).HasColumnType("bit");
            entity.Property(e => e.btGynaecologicalComplications).HasColumnType("bit");
            entity.Property(e => e.btDiagnosedMedicalTreatment).HasColumnType("bit");
            entity.Property(e => e.btSpouseSufferingHivOrHepatitis).HasColumnType("bit");
            entity.Property(e => e.btLaTreatmentSame).HasColumnType("bit");
            entity.Property(e => e.btIsFamilySufferAnyNeurologicalDisorder).HasColumnType("bit");
            entity.Property(e => e.btIsAnyChestRelatedCheckups).HasColumnType("bit");
            entity.Property(e => e.btAsthmaOrTuberculosis).HasColumnType("bit");
            entity.Property(e => e.btUlcerOrPencreatic).HasColumnType("bit");
            entity.Property(e => e.btArthritisOrBone).HasColumnType("bit");
            entity.Property(e => e.btBloodDisorder).HasColumnType("bit");
            entity.Property(e => e.btCancerOrTumour).HasColumnType("bit");
            entity.Property(e => e.btChestPainOrHeartAttack).HasColumnType("bit");
            entity.Property(e => e.btCongenitalOrHereditary).HasColumnType("bit");
            entity.Property(e => e.btDiabetesOrsugarInUrine).HasColumnType("bit");
            entity.Property(e => e.btEyeNoseEarSkinDisorder).HasColumnType("bit");
            entity.Property(e => e.btHypertension).HasColumnType("bit");
            entity.Property(e => e.btHivTesting).HasColumnType("bit");
            entity.Property(e => e.btLiverGallbladderJaundice).HasColumnType("bit");
            entity.Property(e => e.btMentalHealthDisorders).HasColumnType("bit");
            entity.Property(e => e.btPhysicalImpairment).HasColumnType("bit");
            entity.Property(e => e.btNeurologicalDisorders).HasColumnType("bit");
            entity.Property(e => e.btHormonalDisorders).HasColumnType("bit");
            entity.Property(e => e.btUrologicalReproductiveDisorders).HasColumnType("bit");
            entity.Property(e => e.btMedicalTreatmentHistoryLast5Years).HasColumnType("bit");
            entity.Property(e => e.btCirculatorySystemDisorder).HasColumnType("bit");
            entity.Property(e => e.btCurrentlySufferingOtherThanAboveDetails).HasColumnType("bit");
            entity.Property(e => e.btDigestiveDisorder).HasColumnType("bit");
            entity.Property(e => e.btKidneyStoneDisorder).HasColumnType("bit");
            entity.Property(e => e.btMedicalHistory).HasColumnType("bit");
            entity.Property(e => e.btPsychiatricDisorder).HasColumnType("bit");
            entity.Property(e => e.btSpouseAdvisedTest).HasColumnType("bit");
            entity.Property(e => e.btSurgeryOrInvetigations).HasColumnType("bit");
            entity.Property(e => e.btThyroidDisorder).HasColumnType("bit");
            entity.Property(e => e.GyneacologicalDiagnosis).HasColumnType("bit").HasDefaultValue(false);
            entity.Property(e => e.HealthDisorder).HasColumnType("bit").HasDefaultValue(false);
            entity.Property(e => e.HospitalizedOrUndergoneAnySurgery).HasColumnType("bit").HasDefaultValue(false);
            entity.Property(e => e.AccidentInsuranceBeenDeclinedEver).HasColumnType("bit").HasDefaultValue(false);
            entity.Property(e => e.AnyFamilyMemberUndergoneForCovid19Test).HasColumnType("bit").HasDefaultValue(false);
            entity.Property(e => e.btAnyOtherIllness).HasColumnType("bit").HasDefaultValue(false);
            entity.Property(e => e.btHypertensionHeartattack).HasColumnType("bit");
            entity.Property(e => e.btParalysisStroke).HasColumnType("bit");
            entity.Property(e => e.btMoreInformation).HasColumnType("bit");
            entity.Property(e => e.btBrainEyeEar).HasColumnType("bit");

            // DateTime fields
            entity.Property(e => e.dtDateOfLastDelivery).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtApproxDueDateOfDelivery).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtDateOfFirstDiagnosisForHivOrHepatitis).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtFirstDiagnosisOfAnyChestRelatedCheckupsDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtDiagnosisDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.vcStillHaveSymptoms).HasColumnType("datetime2(7)");
            entity.Property(e => e.DueDateOfDelivery).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtCreateDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtModifiedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtDeletedDate).HasColumnType("datetime2(7)");

            // Required bool
            entity.Property(e => e.bitIsDeleted);
        }
    }
}
