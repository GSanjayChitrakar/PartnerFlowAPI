using Microsoft.EntityFrameworkCore;
using PartnerFlowAPI.Database.Configuration;
using PartnerFlowAPI.Database.Entities;
using PartnerFlowAPI.Domain.Entities;
using PartnerFlowAPI.Infrastructure.Persistence.Configurations;
using PartnerFlowAPI.Configurations;
using PartnerFlowAPI.Domain.Entities;
using PartnerFlowAPI.Entities;
using PartnerFlowAPI.Infrastructure.Persistence.Configurations;

namespace PartnerFlowAPI.Database.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
        public DbSet<MST_PFA_Partner> Partners { get; set; }
        public DbSet<ApiLog> ApiLogs { get; set; }
        public DbSet<MST_PFA_PartnerSection> PartnerSections { get; set; }
        public DbSet<MST_PFA_Section> Sections { get; set; }
        public DbSet<MST_PFA_SectionField> SectionFields { get; set; }
        public DbSet<MST_PFA_Field> Fields { get; set; }
        public DbSet<tblPartnerSuitability> partnerSuitabilities { get; set; }
        public DbSet<tblPartnerData> tblPartnerDatas { get; set; }
        public DbSet<tblPF_AgentDetails> tblPF_AgentDetails { get; set; }
        public DbSet<tblPF_BankAccountData> tblPF_BankAccountDatas { get; set; }
        public DbSet<tblPF_CommunicationDetails> tblPF_CommunicationDetails { get; set; }
        public DbSet<tblPF_EmploymentDetails> tblPF_EmploymentDetails { get; set; }
        public DbSet<tblPF_FamilyDetails> tblPF_FamilyDetails { get; set; }
        public DbSet<tblpf_FatcaDetails> tblpf_FatcaDetails { get; set; }
        public DbSet<tblpf_financialQuestion> tblpf_FinancialQuestions { get; set; }
        public DbSet<tblPF_Form60Questions> tblPF_Form60Questions { get; set; }
        public DbSet<tblPF_HealthConditions> tblPF_HealthConditions { get; set; }
        public DbSet<tblPF_HealthConditionsDetails> tblPF_HealthConditionsDetails { get; set; }
        public DbSet<tblPF_InsuranceHistory> tblPF_InsuranceHistories { get; set; }

        public DbSet<tblPF_LifeStyleDetails> tblPF_LifeStyleDetails { get; set; }
        public DbSet<tblPF_MandateDetails> tblPF_MandateDetails { get; set; }
        public DbSet<tblPF_MinorDetails> tblPF_MinorDetails { get; set; }
        public DbSet<tblPF_NomineeDetails> tblPF_NomineeDetails { get; set; }
        public DbSet<tblPF_NRIDetails> tblPF_NRIDetails { get; set; }
        public DbSet<tblPF_OtherInsurances> tblPF_OtherInsurances { get; set; }
        public DbSet<tblpf_PartialWithdrawal> tblpf_PartialWithdrawals { get; set; }
        public DbSet<tblPF_PaymentDetails> tblPF_PaymentDetails { get; set; }
        public DbSet<tblPF_PersonalDetails> tblPF_PersonalDetails { get; set; }
        public DbSet<Tblpf_SummaryDetails> tblpf_SummaryDetails { get; set; }
        public DbSet<tblPF_ProductDetails> productDetails { get; set; }
        public DbSet<tblPF_RiderDetails> riderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.ApplyConfiguration(new MST_PFA_PartnerConfiguration());
            modelBuilder.ApplyConfiguration(new ApiLogConfiguration());
            modelBuilder.ApplyConfiguration(new MST_PFA_PartnerSectionConfiguration());
            modelBuilder.ApplyConfiguration(new MST_PFA_SectionConfiguration());
            modelBuilder.ApplyConfiguration(new MST_PFA_SectionFieldConfiguration());
            modelBuilder.ApplyConfiguration(new MST_PFA_FieldConfiguration());
            modelBuilder.ApplyConfiguration(new tblPartnerSuitabilityConfiguration());
            modelBuilder.ApplyConfiguration(new tblPartnerDataConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_AgentDetailConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_BankAccountDataConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_CommunicationDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_EmploymentDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_FamilyDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new tblpf_FatcaDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new tblpf_financialQuestionConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_Form60QuestionsConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_HealthConditionsConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_HealthConditionsDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_InsuranceHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_LifeStyleDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_MandateDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_MinorDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_NomineeDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_NRIDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_OtherInsurancesConfiguration());
            modelBuilder.ApplyConfiguration(new tblpf_PartialWithdrawalConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_PaymentDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_PersonalDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new Tblpf_SummaryDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new ProductDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new tblPF_RiderDetailsConfiguration());
        }
    }
}
