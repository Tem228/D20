
public struct PlayerStatCheckResult
{
    public int RollResult { get; private set; }

    public int TotalBonusAmount { get; private set; }

    public int RollResultWithBonus { get; private set; }

    public PlayerStatCheckResultType Type { get; private set; }

    public bool BonusesUsed => TotalBonusAmount != 0;

    public PlayerStatCheckResult(int rollResult, int totalBonusAmount, int rollResultWithBonus, PlayerStatCheckResultType type)
    {
        RollResult = rollResult;

        TotalBonusAmount = totalBonusAmount;

        RollResultWithBonus = rollResultWithBonus;

        Type = type;
    }
}
