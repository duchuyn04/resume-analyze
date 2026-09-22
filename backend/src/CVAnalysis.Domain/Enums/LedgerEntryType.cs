namespace CVAnalysis.Domain.Enums;

/// <summary>
/// Các loại bút toán ghi nhận biến động số dư credit trong sổ cái bất biến (CreditLedger).
/// </summary>
public enum LedgerEntryType
{
    FreeTrialGranted,
    Hold,
    ReleaseHold,
    ConsumePaid,
    ConsumeFree,
    ManualCreditAdd,
    Compensation
}
