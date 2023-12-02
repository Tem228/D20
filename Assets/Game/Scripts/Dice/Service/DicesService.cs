using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/// <summary>
/// —ервис дл€ получени€/хранени€ костей по типу
/// </summary>
public class DicesService : MonoBehaviour
{
    [Header("Xml")]
    [SerializeField]
    private TextAsset _dicesXmlFile;

    private Dictionary<DiceType, Dice> _dices;

    public void Initialize()
    {
        InitializeDices();
    }

    private void InitializeDices()
    {
        XmlDocument document = new XmlDocument();

        document.LoadXml(_dicesXmlFile.text);

        Dice[] dices = document["Dices"].SelectNodes("Dice").GetDices(); // получение всех костей из xml файла

        _dices = new Dictionary<DiceType, Dice>();

        for(int i = 0; i < dices.Length; i++)
        {
            Dice dice = dices[i];

            _dices.Add(dice.Type, dice);
        }
    }

    public Dice GetDiceByType(DiceType type)
    {
        if (!_dices.ContainsKey(type))
        {
            return null;
        }

        return _dices[type];
    }
}
