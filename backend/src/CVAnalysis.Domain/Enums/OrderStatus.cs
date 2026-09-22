namespace CVAnalysis.Domain.Enums;

/// <summary>
/// Trạng thái đơn mua gói credit VietQR (hết hạn 15 phút, chờ đối soát 2 ngày).
/// </summary>
public enum OrderStatus
{
    Pending,
    Completed,
    Expired,
    AwaitingReconciliation
}
