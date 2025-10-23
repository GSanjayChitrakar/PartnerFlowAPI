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
    public class tblPartnerDataConfiguration : IEntityTypeConfiguration<tblPartnerData>
    {
        public void Configure(EntityTypeBuilder<tblPartnerData> builder)
        {
            builder.ToTable("tblPartnerData");

            // Primary Key
            builder.HasKey(p => p.PartnerID)
                   .HasName("PK_tblPartnerData");

            builder.Property(p => p.PartnerID)
                   .HasColumnName("intPartnerID")
                   .ValueGeneratedOnAdd();

            // Columns
            builder.Property(p => p.GUID).HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ApplicationNumber).HasColumnName("vcApplicationNumber").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.CustId).HasColumnName("vcCustId").HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.AdvisorCode).HasColumnName("vcAdvisorCode").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.Source).HasColumnName("vcSource").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.SourceKey).HasColumnName("vcSourceKey").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.IsLAPrposerSame).HasDefaultValue(false);
            builder.Property(p => p.SalesDataReqd).HasColumnName("salesDataReqd").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.DependentFlag).HasColumnName("dependentFlag").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerDOB).HasColumnName("dtProposer_dob");
            builder.Property(p => p.ProposerMobileNumber).HasColumnName("vcProposer_MobileNumber").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerPanNumber).HasColumnName("vcProposer_PanNumber").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerEmail).HasColumnName("vcProposer_Email").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerGender).HasColumnName("vcProposer_Gender").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerFirstName).HasColumnName("vcProposer_FirstName").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerMiddleName).HasColumnName("vcProposer_MiddleName").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerLastName).HasColumnName("vcProposer_LastName").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerCKYCNumber).HasColumnName("vcProposer_CKYCNumber").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerAadhaarNumber).HasColumnName("vcProposer_AadhaarNumber").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerBankAccountNumber).HasColumnName("vcProposer_BankAccountNumber").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerIFSC).HasColumnName("VcProposerIFSC").HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.ProposerPincode).HasColumnName("vcProposer_Pincode").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerLandMark).HasColumnName("vcProposer_LandMark").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerState).HasColumnName("vcProposer_State").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerAddressLine1).HasColumnName("vcProposer_AddressLine1").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerAddressLine2).HasColumnName("vcProposer_AddressLine2").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerAddressLine3).HasColumnName("vcProposer_AddressLine3").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerCity).HasColumnName("vcProposer_City").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerCountry).HasColumnName("vcProposer_Country").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerAnnualIncome).HasColumnName("vcProposer_AnnualIncome").HasColumnType("decimal(18,0)");
            builder.Property(p => p.ProposerMaritalStatus).HasColumnName("vcProposer_MaritalStatus");
            builder.Property(p => p.LifeAssureMaritalStatus).HasColumnName("vcLifeAssure_MaritalStatus");
            builder.Property(p => p.LifeAssureDOB).HasColumnName("dtLifeAssure_dob");
            builder.Property(p => p.LifeAssureMobileNumber).HasColumnName("vcLifeAssure_MobileNumber").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssurePanNumber).HasColumnName("vcLifeAssure_PanNumber").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureEmail).HasColumnName("vcLifeAssure_Email").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureGender).HasColumnName("vcLifeAssure_Gender").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureFirstName).HasColumnName("vcLifeAssure_FirstName").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureMiddleName).HasColumnName("vcLifeAssure_MiddleName").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureLastName).HasColumnName("vcLifeAssure_LastName").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureCKYCNumber).HasColumnName("vcLifeAssure_CKYCNumber").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureAadhaarNumber).HasColumnName("vcLifeAssure_AadhaarNumber").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureBankAccountNumber).HasColumnName("vcLifeAssure_BankAccountNumber").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureIFSC).HasColumnName("VcLifeAssureIFSC").HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.LifeAssurePincode).HasColumnName("vcLifeAssure_Pincode").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureLandMark).HasColumnName("vcLifeAssure_LandMark").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureState).HasColumnName("vcLifeAssure_State").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureAddressLine1).HasColumnName("vcLifeAssure_AddressLine1").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureAddressLine2).HasColumnName("vcLifeAssure_AddressLine2").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureAddressLine3).HasColumnName("vcLifeAssure_AddressLine3").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureCity).HasColumnName("vcLifeAssure_City").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureCountry).HasColumnName("vcLifeAssure_Country").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureAnnualIncome).HasColumnName("vcLifeAssure_AnnualIncome").HasColumnType("decimal(18,0)");
            builder.Property(p => p.ChannelType).HasColumnName("vcChannelType").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.CustomerBankAccount).HasColumnName("vcCustomerBankAccount").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.PartnerUniqueId1).HasColumnName("vcPartnerUniqueId1").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.BankName).HasColumnName("vcBankName").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.NeedRiskProfile).HasColumnName("btneedRiskProfile").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.CSRLimCode).HasColumnName("vcCSRLimCode").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.CafosCode).HasColumnName("vcCafosCode").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.OppId).HasColumnName("vcOppId").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.IFSCCode).HasColumnName("vcIFSCCode").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.SPCode).HasColumnName("spCode").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.BankBranch).HasColumnName("vcbankBrnch").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.SubChannel).HasColumnName("subChannel").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssurePermanentAddressPincode).HasColumnName("vcLifeAssurePermanentAddressPincode").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssurePermanentAddressLandMark).HasColumnName("vcLifeAssurePermanentAddressLandMark").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssurePermanentAddressState).HasColumnName("vcLifeAssurePermanentAddressState").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssurePermanentAddressAddressLine1).HasColumnName("vcLifeAssurePermanentAddressAddressLine1").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssurePermanentAddressAddressLine2).HasColumnName("vcLifeAssurePermanentAddressAddressLine2").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssurePermanentAddressAddressLine3).HasColumnName("vcLifeAssurePermanentAddressAddressLine3").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssurePermanentAddressCity).HasColumnName("vcLifeAssurePermanentAddressCity").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssurePermanentCountry).HasColumnName("vcLifeAssurePermanentCountry").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerPermanentPincode).HasColumnName("VcProposerPermanentPincode").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerPermanentLandMark).HasColumnName("VcProposerPermanentLandMark").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerPermanentState).HasColumnName("VcProposerPermanentState").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerPermanentAddressLine1).HasColumnName("VcProposerPermanentAddressLine1").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerPermanentAddressLine2).HasColumnName("VcProposerPermanentAddressLine2").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerPermanentAddressLine3).HasColumnName("VcProposerPermanentAddressLine3").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerPermanentCity).HasColumnName("VcProposerPermanentCity").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ProposerPermanentCountry).HasColumnName("VcProposerPermanentCountry").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.RMID).HasColumnName("vcRMID").HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.AgentCode).HasColumnName("vcAgentCode").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LastAccessIP).HasColumnName("vcLastAccessIP").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.CreatedBy).HasColumnName("vcCreatedBy").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.CreateDate).HasColumnName("dtCreateDate");
            builder.Property(p => p.ModifiedBy).HasColumnName("vcModifiedBy").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.ModifiedDate).HasColumnName("dtModifiedDate");
            builder.Property(p => p.DeletedDate).HasColumnName("dtDeletedDate");
            builder.Property(p => p.IsDeleted).HasColumnName("bitIsDeleted").HasDefaultValue(false);
            builder.Property(p => p.AlternateNumber).HasColumnName("vcAlternateNumber").HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.SPName).HasColumnName("vcSPName").HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.SPLocation).HasColumnName("vcSPLocation").HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.ProposerTitle).HasColumnName("vcProposer_Title").HasMaxLength(20).IsUnicode(true);
            builder.Property(p => p.ProposerOccupation).HasColumnName("vcProposer_Occupation").HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.ProposerEducationQualification).HasColumnName("vcProposer_EducationQualification").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.LifeAssureTitle).HasColumnName("vcLifeAssure_Title").HasMaxLength(20).IsUnicode(true);
            builder.Property(p => p.LifeAssureOccupation).HasColumnName("vcLifeAssure_Occupation").HasMaxLength(100).IsUnicode(true);
            builder.Property(p => p.LifeAssureEducationQualification).HasColumnName("vcLifeAssure_EducationQualification").HasMaxLength(200).IsUnicode(true);
            builder.Property(p => p.QuotationID).HasColumnName("vcQuotationID").HasMaxLength(200).IsUnicode(true);

            // Indexes
            builder.HasIndex(p => p.ApplicationNumber).HasDatabaseName("tblPartnerData_ApplicationNumber");
            builder.HasIndex(p => new { p.IsDeleted, p.DeletedDate }).HasDatabaseName("tblPartnerData_IsDeleted_DeletedDate");
            builder.HasIndex(p => p.ApplicationNumber).HasDatabaseName("PartnerAppNo");
        }
    }
}
