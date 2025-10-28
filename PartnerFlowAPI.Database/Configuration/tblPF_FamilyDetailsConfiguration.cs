using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerFlowAPI.Entities;
using System;

namespace PartnerFlowAPI.Configurations
{
    public class tblPF_FamilyDetailsConfiguration : IEntityTypeConfiguration<tblPF_FamilyDetails>
    {
        public void Configure(EntityTypeBuilder<tblPF_FamilyDetails> builder)
        {
            builder.ToTable("tblPF_FamilyDetails");

            // Primary Key
            builder.HasKey(e => e.intFamilyDetailsId)
                   .HasName("PK_tblPF_FamilyDetails");

            // Indexes (if needed, add indexes here)
            builder.HasIndex(e => e.vcApplicationNumber)
                   .HasDatabaseName("tblPF_FamilyDetails_ApplicationNumber");

            builder.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                   .HasDatabaseName("tblPF_FamilyDetails_IsDeleted_DeletedDate");

            // Column configurations
            builder.Property(e => e.intFamilyDetailsId)
                   .IsRequired();

            builder.Property(e => e.vcRelation)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcApplicationNumber)
                   .HasMaxLength(200)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(e => e.intAssureType)
                   .IsRequired();

            builder.Property(e => e.vcTitle)
                   .HasMaxLength(20)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcFirstName)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(e => e.vcMiddleName)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcLastName)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(e => e.dtDOB)
                   .HasColumnType("datetime2")
                   .IsRequired(false);

            builder.Property(e => e.chGender)
                   .HasMaxLength(2)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.dcAnnualIncome)
                   .HasColumnType("decimal(18,0)")
                   .IsRequired(false);

            builder.Property(e => e.vcMobileNumber)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.intTotalLIfeSA)
                   .IsRequired(false);

            builder.Property(e => e.intVitalStatus)
                   .IsRequired(false);

            builder.Property(e => e.vcCauseOfDeath)
                   .HasMaxLength(200)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.intAgeAtDeath)
                   .IsRequired(false);

            builder.Property(e => e.vcHealthStatus)
                   .HasMaxLength(20)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcOccupation)
                   .HasMaxLength(200)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcLastAccessIP)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(e => e.vcCreatedBy)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(e => e.dtCreateDate)
                   .HasColumnType("datetime2")
                   .IsRequired();

            builder.Property(e => e.vcModifiedBy)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.dtModifiedDate)
                   .HasColumnType("datetime2")
                   .IsRequired(false);

            builder.Property(e => e.dtDeletedDate)
                   .HasColumnType("datetime2")
                   .IsRequired(false);

            builder.Property(e => e.bitIsDeleted);
                   

            builder.Property(e => e.btIsAppointee)
                   .IsRequired(false);

            builder.Property(e => e.vcNameOfAppointee)
                   .HasMaxLength(1000)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcAppointeeDob)
                   .HasColumnType("datetime2")
                   .IsRequired(false);

            builder.Property(e => e.chAppointeeGender)
                   .HasMaxLength(2)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcAppointeeContactNumber)
                   .HasMaxLength(40)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcAppointeeCkycNumber)
                   .HasMaxLength(20)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcAppointeeAddress1)
                   .HasMaxLength(1000)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcAppointeeAddress2)
                   .HasMaxLength(1000)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcAppointeeLandmark)
                   .HasMaxLength(1000)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcAppointeeCity)
                   .HasMaxLength(1000)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcAppointeeState)
                   .HasMaxLength(1000)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcAppointeeCountry)
                   .HasMaxLength(1000)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcAppointeePincode)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.btAppointeeAddressSameAsNominee)
                   .IsRequired(false);

            builder.Property(e => e.vcRelationshipWithNominee)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcAppointeeSignature)
                   .HasMaxLength(1000)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.intAgeAtOnset)
                   .IsRequired(false);

            builder.Property(e => e.vcLivingOrDeceased)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcDiagnosis)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.btIsNominee)
                   .IsRequired(false);

            builder.Property(e => e.ftNomineePercentage)
                   .IsRequired(false);
        }
    }
}
