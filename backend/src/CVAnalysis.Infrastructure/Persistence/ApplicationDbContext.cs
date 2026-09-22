using CVAnalysis.Application.Common.Interfaces;
using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CVAnalysis.Infrastructure.Persistence;

/// <summary>
/// Entity Framework Core DbContext cho toàn bộ hệ thống CVAnalysis (15 bảng SQL Server).
/// </summary>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<EmailHashRegistry> EmailHashRegistries => Set<EmailHashRegistry>();
    public DbSet<UserBalance> UserBalances => Set<UserBalance>();
    public DbSet<CVFile> CVFiles => Set<CVFile>();
    public DbSet<JobDescription> JobDescriptions => Set<JobDescription>();
    public DbSet<AnalysisRequest> AnalysisRequests => Set<AnalysisRequest>();
    public DbSet<Report> Reports => Set<Report>();
    public DbSet<OptimizedBulletPoint> OptimizedBulletPoints => Set<OptimizedBulletPoint>();
    public DbSet<CreditPackage> CreditPackages => Set<CreditPackage>();
    public DbSet<PaymentOrder> PaymentOrders => Set<PaymentOrder>();
    public DbSet<CreditLedger> CreditLedgers => Set<CreditLedger>();
    public DbSet<SupportTicket> SupportTickets => Set<SupportTicket>();
    public DbSet<SupportAccessGrant> SupportAccessGrants => Set<SupportAccessGrant>();
    public DbSet<CompensationRequest> CompensationRequests => Set<CompensationRequest>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
