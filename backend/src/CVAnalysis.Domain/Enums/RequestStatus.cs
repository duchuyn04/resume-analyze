namespace CVAnalysis.Domain.Enums;

/// <summary>
/// Trạng thái vòng đời của một yêu cầu phân tích CV (Circuit Breaker 90s).
/// </summary>
public enum RequestStatus
{
    Queued,
    Processing,
    Completed,
    Failed,
    TimedOut,
    Cancelled
}
