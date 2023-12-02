using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

/// <summary>
/// Сервис для хранения/получения бонусов статов
/// </summary>
public class PlayerStatsBonusesService : MonoBehaviour
{
    [Header("Xml")]
    [SerializeField]
    private TextAsset _bonusesXmlFile;

    private Dictionary<int, PlayerStatBonus> _bonuses;

    public void Initialize()
    {
        InitializeBonuses();
    }

    private void InitializeBonuses()
    {
        XmlDocument document = new XmlDocument();

        document.LoadXml(_bonusesXmlFile.text);

        PlayerStatBonus[] bonuses = document["Bonuses"].SelectNodes("Bonus").GetPlayerStatsBonuses(); // получение всех бонусов из xml

        _bonuses = new Dictionary<int, PlayerStatBonus>();

        for (int i = 0; i < bonuses.Length; i++)
        {
            PlayerStatBonus bonus = bonuses[i];

            _bonuses.Add(bonus.ID, bonus);
        }
    }

    public PlayerStatBonus[] GetBonusesByStat(PlayerStat stat)
    {
        List<PlayerStatBonus> result = new List<PlayerStatBonus>();

        PlayerStatBonus[] bonuses = _bonuses.Values.ToArray();

        for(int i = 0; i < bonuses.Length; i++)
        {
            PlayerStatBonus bonus = bonuses[i];

            if (bonus.Stat == stat)
            {
                result.Add(bonus);
            }
        }

        return result.ToArray();
    }
}
