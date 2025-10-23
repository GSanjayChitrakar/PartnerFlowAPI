using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourNamespace.Entities;
using System;

namespace YourNamespace.Configurations
{
    public class tblPF_PersonalDetailsConfiguration : IEntityTypeConfiguration<tblPF_PersonalDetails>
    {
        public void Configure(EntityTypeBuilder<tblPF_PersonalDetails> builder)
        {
            builder.ToTable("tblPF_PersonalDetails");

            // Primary Key
            builder.HasKey(e => e.intPersonalId)
                   .HasName("PK_tblApp_PersonalDetails");

            // Indexes
            builder.HasIndex(e => e.vcApplicationNumber)
                   .HasDatabaseName("tblPF_PersonalDetails_ApplicationNumber");

            builder.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                   .HasDatabaseName("tblPF_PersonalDetails_IsDeleted_DeletedDate");

            // Column configurations
            builder.Property(e => e.intPersonalId)
                   .HasColumnName("intPersonalId")
                   .IsRequired();

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

            builder.Property(e => e.vcCompanyName)
                   .HasMaxLength(200)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcFirstName)
                   .HasMaxLength(510)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcMiddleName)
                   .HasMaxLength(510)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcLastName)
                   .HasMaxLength(510)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcNameBeforeMarriageTitle)
                   .HasMaxLength(20)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcNameBeforeMarriageFirstName)
                   .HasMaxLength(510)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcNameBeforeMarriageMiddleName)
                   .HasMaxLength(510)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcNameBeforeMarriageLastName)
                   .HasMaxLength(510)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcAdharNumber)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcPANNumber)
                   .HasMaxLength(20)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.intMaritalStatus)
                   .IsRequired(false);

            builder.Property(e => e.intNumberOfChildren)
                   .IsRequired(false);

            builder.Property(e => e.vcCKYCNumber)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcEducationQualification)
                   .HasMaxLength(200)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.dcAnnualIncome)
                   .HasColumnType("decimal(18,7)")
                   .IsRequired(false);

            builder.Property(e => e.vcAUCustomerId)
                   .HasMaxLength(200)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcAUSavingsOrLoanAccountNumber)
                   .HasMaxLength(200)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.chGender)
                   .HasColumnType("char(1)")
                   .IsRequired(false);

            builder.Property(e => e.dtDOB)
                   .HasColumnType("datetime2")
                   .IsRequired(false);

            builder.Property(e => e.vcCountryOfBirth)
                   .HasMaxLength(200)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.intNationality)
                   .IsRequired(false);

            builder.Property(e => e.btIsCriminalRecord)
                   .IsRequired(false);

            builder.Property(e => e.vcCriminalCaseDescription)
                   .HasMaxLength(2000)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.btRelatedToPoliticalParty)
                   .IsRequired(false);

            builder.Property(e => e.vcPoliticalDescription)
                   .HasMaxLength(2000)
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

            builder.Property(e => e.bitIsDeleted)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.intDeDupeStatus)
                   .HasDefaultValue(0)
                   .IsRequired();

            builder.Property(e => e.intImageQCStatus)
                   .HasDefaultValue(0)
                   .IsRequired();

            builder.Property(e => e.intDocQCStatus)
                   .HasDefaultValue(0)
                   .IsRequired();

            builder.Property(e => e.vcLifeAsiaClientId)
                   .HasMaxLength(20)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.dtClientIDGeneratedOn)
                   .HasColumnType("datetime2")
                   .IsRequired(false);

            builder.Property(e => e.SysStartTime)
                   .HasColumnType("datetime2")
                   .HasDefaultValueSql("sysutcdatetime()")
                   .IsRequired();

            builder.Property(e => e.SysEndTime)
                   .HasColumnType("datetime2")
                   .HasDefaultValue(DateTime.Parse("9999-12-31 23:59:59"))
                   .IsRequired();

            builder.Property(e => e.vcRelationWithLA)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcInterNationalNumber)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcCountryOfResidence)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.btPanVerified)
                   .HasDefaultValue(false)
                   .IsRequired(false);

            builder.Property(e => e.btPanDOBMatch)
                   .HasDefaultValue(false)
                   .IsRequired(false);

            builder.Property(e => e.btPanNameMatch)
                   .HasDefaultValue(false)
                   .IsRequired(false);

            builder.Property(e => e.vcPanDublicate)
                   .HasMaxLength(200)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcRequestId)
                   .HasMaxLength(400)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.btisPanVerify)
                   .IsRequired(false);

            builder.Property(e => e.intAMLStatus)
                   .IsRequired(false);

            builder.Property(e => e.vcCibilScore)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcIncomeEstimator)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcIIBScore)
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcDedupeMode)
                   .HasMaxLength(200)
                   .IsUnicode()
                   .IsRequired(false);
        }
    }
}
