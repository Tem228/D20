
public class PlayerStatBonus
{
    public int ID { get; private set; }

    public int BonusAmount { get; private set; }

    public string Name { get; private set; }

    public PlayerStat Stat { get; private set; }

    public PlayerStatBonus(int id, int bonusAmount, string name, PlayerStat stat)
    {
        ID = id;

        BonusAmount = bonusAmount;

        Name = name;

        Stat = stat;
    }
}
