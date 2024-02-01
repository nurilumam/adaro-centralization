using Adaro.Centralize.JobScheduler;
using Adaro.Centralize.SAPConnector;
using Adaro.Centralize.MasterDataRequest;
using Adaro.Centralize.MasterData;
using Adaro.Centralize.Travel;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Adaro.Centralize.Authorization.Delegation;
using Adaro.Centralize.Authorization.Roles;
using Adaro.Centralize.Authorization.Users;
using Adaro.Centralize.Chat;
using Adaro.Centralize.Editions;
using Adaro.Centralize.Friendships;
using Adaro.Centralize.MultiTenancy;
using Adaro.Centralize.MultiTenancy.Accounting;
using Adaro.Centralize.MultiTenancy.Payments;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.EntityFrameworkCore
{
    public class CentralizeDbContext : AbpZeroDbContext<Tenant, Role, User, CentralizeDbContext>
    {
        public virtual DbSet<JobSynchronize> JobSynchronizes { get; set; }

        public virtual DbSet<CostCenter> CostCenters { get; set; }

        public virtual DbSet<DataProduction> DataProductions { get; set; }

        public virtual DbSet<MaterialRequest> MaterialRequests { get; set; }

        public virtual DbSet<EnumTable> EnumTables { get; set; }

        public virtual DbSet<Material> Materials { get; set; }

        public virtual DbSet<GeneralLedgerMapping> GeneralLedgerMappings { get; set; }

        public virtual DbSet<MaterialGroup> MaterialGroups { get; set; }

        public virtual DbSet<UNSPSC> UNSPSCs { get; set; }

        public virtual DbSet<TravelRequest> TravelRequests { get; set; }

        public virtual DbSet<Airport> Airports { get; set; }

        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public virtual DbSet<SubscriptionPaymentExtensionData> SubscriptionPaymentExtensionDatas { get; set; }

        public virtual DbSet<UserDelegation> UserDelegations { get; set; }

        public virtual DbSet<RecentPassword> RecentPasswords { get; set; }

        public CentralizeDbContext(DbContextOptions<CentralizeDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobSynchronize>(j =>
            {
                j.HasIndex(e => new { e.TenantId });
            });
            modelBuilder.Entity<CostCenter>(c =>
                       {
                           c.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<DataProduction>(d =>
                       {
                           d.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Material>(m =>
                       {
                           m.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<MaterialRequest>(m =>
                       {
                           m.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Material>(m =>
                       {
                           m.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<UNSPSC>(u =>
                       {
                           u.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<MaterialGroup>(m =>
                       {
                           m.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<GeneralLedgerMapping>(g =>
                       {
                           g.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EnumTable>(x =>
                       {
                           x.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<MaterialRequest>(m =>
                       {
                           m.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<EnumTable>(x =>
                       {
                           x.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Material>(m =>
                       {
                           m.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<GeneralLedgerMapping>(g =>
                       {
                           g.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<UNSPSC>(u =>
                       {
                           u.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<MaterialGroup>(m =>
                       {
                           m.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<UNSPSC>(u =>
                       {
                           u.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<TravelRequest>(t =>
                       {
                           t.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<Airport>(a =>
                       {
                           a.HasIndex(e => new { e.TenantId });
                       });
            modelBuilder.Entity<BinaryObject>(b =>
                       {
                           b.HasIndex(e => new { e.TenantId });
                       });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { PaymentId = e.ExternalPaymentId, e.Gateway });
            });

            modelBuilder.Entity<SubscriptionPaymentExtensionData>(b =>
            {
                b.HasQueryFilter(m => !m.IsDeleted)
                    .HasIndex(e => new { e.SubscriptionPaymentId, e.Key, e.IsDeleted })
                    .IsUnique()
                    .HasFilter("[IsDeleted] = 0");
            });

            modelBuilder.Entity<UserDelegation>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.SourceUserId });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId });
            });
        }
    }
}