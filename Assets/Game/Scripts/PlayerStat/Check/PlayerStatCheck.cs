
/// <summary>
/// Проверка статы игрока
/// </summary>
public class PlayerStatCheck 
{
    private int _difficultyClass;

    public int DifficultyClass
    {
        get => _difficultyClass;

        private set
        {
            if(value < MIN_DIFFICULTY_CLASS
            || value > MAX_DIFFICULTY_CLASS)
            {
                throw new System.Exception($"DiffucultyClass не может принимать значение меньше {MIN_DIFFICULTY_CLASS} и больше {MAX_DIFFICULTY_CLASS}");
            }

            _difficultyClass = value;
        }
    }

    public PlayerStat CheckableStat { get; private set; }

    public Dice Dice { get; private set; }

    public const int MIN_DIFFICULTY_CLASS = 1;
    public const int MAX_DIFFICULTY_CLASS = 99;

    public PlayerStatCheck(int difficultyClass, PlayerStat checkableStat, Dice dice) 
    {
        DifficultyClass = difficultyClass;

        CheckableStat = checkableStat;

        Dice = dice;
    }

    /// <summary>
    /// Метод для проверкы статы игрока
    /// </summary>
    public PlayerStatCheckResult Check(PlayerStatBonus[] bonuses)
    {
        int rollResult = Dice.Roll();

        int totalBonusAmount = CalculateTotalBonusAmount(bonuses);

        int rollResultWithBonus = rollResult + totalBonusAmount;

        PlayerStatCheckResultType resultType = CalculateResultType(rollResultWithBonus);

        return new PlayerStatCheckResult(rollResult, totalBonusAmount, rollResultWithBonus, resultType);
    }

    private int CalculateTotalBonusAmount(PlayerStatBonus[] bonuses)
    {
        int result = 0;
       
        for(int i = 0; i < bonuses.Length; i++)
        {
            result += bonuses[i].BonusAmount;
        }

        return result;
    }

    private PlayerStatCheckResultType CalculateResultType(int rollResultWithBonus)
    {
        if (rollResultWithBonus == MIN_DIFFICULTY_CLASS)
        {
            return PlayerStatCheckResultType.CriticalFail;
        }

        if (rollResultWithBonus == Dice.EdgesAmount)
        {
            return PlayerStatCheckResultType.CriticalSuccess;
        }

        if (rollResultWithBonus < DifficultyClass)
        {
            return PlayerStatCheckResultType.Fail;
        }

        return PlayerStatCheckResultType.Success;
    }
}
