using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CVAnalysis.Application.Common.Interfaces;

/// <summary>
/// Hợp đồng truy xuất cơ sở dữ liệu trừu tượng cho tầng Application.
/// </summary>
public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<EmailHashRegistry> EmailHashRegistries { get; }
    DbSet<UserBalance> UserBalances { get; }
    DbSet<CVFile> CVFiles { get; }
    DbSet<JobDescription> JobDescriptions { get; }
    DbSet<AnalysisRequest> AnalysisRequests { get; }
    DbSet<Report> Reports { get; }
    DbSet<OptimizedBulletPoint> OptimizedBulletPoints { get; }
    DbSet<CreditPackage> CreditPackages { get; }
    DbSet<PaymentOrder> PaymentOrders { get; }
    DbSet<CreditLedger> CreditLedgers { get; }
    DbSet<SupportTicket> SupportTickets { get; }
    DbSet<SupportAccessGrant> SupportAccessGrants { get; }
    DbSet<CompensationRequest> CompensationRequests { get; }
    DbSet<AuditLog> AuditLogs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
