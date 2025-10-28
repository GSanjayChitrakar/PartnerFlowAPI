using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerFlowAPI.Entities;

namespace PartnerFlowAPI.Configurations
{
    public class tblPF_NRIDetailsConfiguration : IEntityTypeConfiguration<tblPF_NRIDetails>
    {
        public void Configure(EntityTypeBuilder<tblPF_NRIDetails> builder)
        {
            builder.ToTable("tblPF_NRIDetails");

            builder.HasKey(e => e.intNRIID)
                   .HasName("PK_tblPF_NRIDetails");

            builder.HasIndex(e => e.vcApplicationNumber)
                   .HasDatabaseName("tblPF_NRIDetails_ApplicationNumber");

            builder.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                   .HasDatabaseName("tblPF_NRIDetails_IsDeleted_DeletedDate");

            builder.Property(e => e.intNRIID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.vcApplicationNumber).HasMaxLength(200).IsRequired();
            builder.Property(e => e.intAssureTypeID);
            builder.Property(e => e.vcTitle).HasMaxLength(20);
            builder.Property(e => e.vcFirstName).HasMaxLength(510);
            builder.Property(e => e.vcMiddleName).HasMaxLength(510);
            builder.Property(e => e.vcLastName).HasMaxLength(510);
            builder.Property(e => e.vcNationality).HasMaxLength(510);
            builder.Property(e => e.vcCurrentResidenceCountry).HasMaxLength(510);
            builder.Property(e => e.vcCountryVisited).HasMaxLength(510);
            builder.Property(e => e.dtDateOfLeaving);
            builder.Property(e => e.intIntentedDuration);
            builder.Property(e => e.vcAddressLine1).HasMaxLength(1000);
            builder.Property(e => e.vcAddressLine2).HasMaxLength(1000);
            builder.Property(e => e.vcCountry).HasMaxLength(510);
            builder.Property(e => e.vcState).HasMaxLength(510);
            builder.Property(e => e.vcCity).HasMaxLength(510);
            builder.Property(e => e.vcPincode).HasMaxLength(200);
            builder.Property(e => e.vcPurposeofstaying).HasMaxLength(510);
            builder.Property(e => e.vcPassportnumber).HasMaxLength(200);
            builder.Property(e => e.vcDateofissue).HasMaxLength(510);
            builder.Property(e => e.dtPassportvalidity);
            builder.Property(e => e.vcRecententry).HasMaxLength(510);
            builder.Property(e => e.vcCountryofresidence).HasMaxLength(510);
            builder.Property(e => e.dtResidencedate);
            builder.Property(e => e.vcResidentialstatusfortax).HasMaxLength(510);
            builder.Property(e => e.vcPermanentAccount).HasMaxLength(200);
            builder.Property(e => e.btDoyouNriAccount);
            builder.Property(e => e.vcBankname).HasMaxLength(510);
            builder.Property(e => e.vcBankAddressLine1).HasMaxLength(510);
            builder.Property(e => e.vcBankAddressLine2).HasMaxLength(510);
            builder.Property(e => e.vcBankcountry).HasMaxLength(510);
            builder.Property(e => e.vcBankstate).HasMaxLength(510);
            builder.Property(e => e.vcBankcity).HasMaxLength(510);
            builder.Property(e => e.vcBankpincode).HasMaxLength(200);
            builder.Property(e => e.vcTypeofaccount).HasMaxLength(100);
            builder.Property(e => e.vcBankaccountnumber).HasMaxLength(200);
            builder.Property(e => e.vcDispatchedname).HasMaxLength(510);
            builder.Property(e => e.vcDispatchaddress1).HasMaxLength(510);
            builder.Property(e => e.vcDispatchaddress2).HasMaxLength(510);
            builder.Property(e => e.vcDispatchcountry).HasMaxLength(510);
            builder.Property(e => e.vcDispatchstate).HasMaxLength(510);
            builder.Property(e => e.vcDispatchcity).HasMaxLength(510);
            builder.Property(e => e.vcDispatchpincode).HasMaxLength(200);
            builder.Property(e => e.vcPaymentmanner).HasMaxLength(510);
            builder.Property(e => e.vcEcsEnterBankName).HasMaxLength(200);
            builder.Property(e => e.vcLastAccessIP).HasMaxLength(100).IsRequired();
            builder.Property(e => e.vcCreatedBy).HasMaxLength(100).IsRequired();
            builder.Property(e => e.dtCreateDate).IsRequired();
            builder.Property(e => e.vcModifiedBy).HasMaxLength(100);
            builder.Property(e => e.dtModifiedDate);
            builder.Property(e => e.dtDeletedDate);
            builder.Property(e => e.bitIsDeleted);
        }
    }
}
