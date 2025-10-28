using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerFlowAPI.Entities;
using System;

namespace PartnerFlowAPI.Configurations
{
    public class tblPF_MinorDetailsConfiguration : IEntityTypeConfiguration<tblPF_MinorDetails>
    {
        public void Configure(EntityTypeBuilder<tblPF_MinorDetails> builder)
        {
            builder.ToTable("tblPF_MinorDetails");

            // Primary Key
            builder.HasKey(e => e.MinorFormId)
                   .HasName("PK_tblPF_MinorDetails");

            // Indexes
            builder.HasIndex(e => e.vcApplicationNumber)
                   .HasDatabaseName("tblPF_MinorDetails_ApplicationNumber");

            builder.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                   .HasDatabaseName("tblPF_MinorDetails_IsDeleted_DeletedDate");

            // Columns
            builder.Property(e => e.MinorFormId)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.vcApplicationNumber)
                   .HasColumnType("varchar(50)")
                   .IsUnicode(false)
                   .IsRequired(false);

            builder.Property(e => e.vcLAStudingInClass)
                   .HasColumnType("nvarchar(100)")
                   .IsRequired(false);

            builder.Property(e => e.vcNameOfSchool)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btAnyPhysicalProblem)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcPhysicalProblemDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btAnyMedicationRegular)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcAnyMedicationRegularDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btEpilepsyConvulsions)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcEpilepsyConvulsionsDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btHeartLung)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcHeartLungDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btDiabetes)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcDiabetesDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btEczema)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcEczemaDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btEatingdisorders)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcEatingdisordersDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btWhoopingcough)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcWhoopingcoughDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btGlandularfever)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcGlandularfeverDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btEarinfection)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcEarinfectionDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btMeaslesMumps)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcMeaslesMumpsDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btConvulsions)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcConvulsionsDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btChickenpox)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcChickenpoxDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btScarletFever)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcScarletFeverDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btBronchitisAsthma)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcBronchitisAsthmaDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.btHearingProblem)
                   .HasColumnType("bit")
                   .IsRequired();

            builder.Property(e => e.vcHearingProblemDesc)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.vcProvideDetailsOfAboveSelected)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.vcRelevantInformatiom)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);

            builder.Property(e => e.vcDetailsOfVaccination)
                   .HasColumnType("nvarchar(1000)")
                   .IsRequired(false);

            builder.Property(e => e.intAssureType)
                   .HasColumnType("int")
                   .IsRequired(false);

            builder.Property(e => e.vcLastAccessIP)
                   .HasColumnType("nvarchar(100)")
                   .IsRequired(false);

            builder.Property(e => e.vcCreatedBy)
                   .HasColumnType("nvarchar(100)")
                   .IsRequired(false);

            builder.Property(e => e.dtCreateDate)
                   .HasColumnType("datetime2")
                   .IsRequired(false);

            builder.Property(e => e.vcModifiedBy)
                   .HasColumnType("nvarchar(100)")
                   .IsRequired(false);

            builder.Property(e => e.dtModifiedDate)
                   .HasColumnType("datetime2")
                   .IsRequired(false);

            builder.Property(e => e.dtDeletedDate)
                   .HasColumnType("datetime2")
                   .IsRequired(false);

            builder.Property(e => e.bitIsDeleted);
                 

            builder.Property(e => e.vcotherVaccination)
                   .HasColumnType("nvarchar(200)")
                   .IsRequired(false);
        }
    }
}
