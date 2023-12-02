using System.Xml;
using System;
using UnityEngine.U2D;
using UnityEngine;

/// <summary>
/// »спользуетс€ дл€ преобразовани€ XmlNode в нужный тип данных
/// </summary>
public static class XmlNodeExtensions 
{
    #region Bool

    public static bool GetBool(this XmlNode node)
    {
        return bool.Parse(node.InnerText);
    }

    public static bool[] GetBools(this XmlNodeList nodes)
    {
        bool[] result = new bool[nodes.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = nodes[i].GetBool();
        }

        return result;
    }

    #endregion

    #region String

    public static string GetString(this XmlNode node)
    {
        return node.InnerText.Replace("\\n", "\n");
    }

    public static string[] GetStrings(this XmlNodeList nodes)
    {
        string[] result = new string[nodes.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = nodes[i].GetString();
        }

        return result;
    }

    #endregion

    #region Char

    public static char GetChar(this XmlNode node)
    {
        return char.Parse(node.InnerText);
    }

    public static char[] GetChars(this XmlNodeList nodes)
    {
        char[] result = new char[nodes.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = nodes[i].GetChar();
        }

        return result;
    }

    #endregion

    #region Byte

    public static byte GetByte(this XmlNode node)
    {
        return byte.Parse(node.InnerText);
    }

    public static byte[] GetBytes(this XmlNodeList nodes)
    {
        byte[] result = new byte[nodes.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = nodes[i].GetByte();
        }

        return result;
    }

    #endregion

    #region Short

    public static short GetShort(this XmlNode node)
    {
        return short.Parse(node.InnerText);
    }

    public static short[] GetShorts(this XmlNodeList nodes)
    {
        short[] result = new short[nodes.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = nodes[i].GetShort();
        }

        return result;
    }

    #endregion

    #region UShort

    public static ushort GetUShort(this XmlNode node)
    {
        return ushort.Parse(node.InnerText);
    }

    public static ushort[] GetUShorts(this XmlNodeList nodes)
    {
        ushort[] result = new ushort[nodes.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = nodes[i].GetUShort();
        }

        return result;
    }

    #endregion

    #region Int

    public static int GetInt(this XmlNode node)
    {
        return int.Parse(node.InnerText);
    }

    public static int[] GetInt(this XmlNodeList nodes)
    {
        int[] result = new int[nodes.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = nodes[i].GetInt();
        }

        return result;
    }

    #endregion

    #region Float

    public static float GetFloat(this XmlNode node)
    {
        return float.Parse(node.InnerText, System.Globalization.CultureInfo.InvariantCulture);
    }

    public static float[] GetFloats(this XmlNodeList nodes)
    {
        float[] result = new float[nodes.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = nodes[i].GetFloat();
        }

        return result;
    }

    #endregion

    #region TimeSpan

    public static TimeSpan GetTimeSpan(this XmlNode node)
    {
        string[] parts = node.InnerText.Split(':');

        int hours = int.Parse(parts[0]);
        int minutes = int.Parse(parts[1]);
        int seconds = int.Parse(parts[2]);

        return new TimeSpan(hours, minutes, seconds);
    }

    public static TimeSpan[] GetTimeSpans(this XmlNodeList nodes)
    {
        TimeSpan[] result = new TimeSpan[nodes.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = nodes[i].GetTimeSpan();
        }

        return result;
    }

    #endregion

    #region Enum

    public static T GetEnum<T>(this XmlNode node) where T : Enum
    {
        return (T)Enum.Parse(typeof(T), node.GetString(), true);
    }

    public static T[] GetEnum<T>(this XmlNodeList nodes) where T : Enum
    {
        T[] result = new T[nodes.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = nodes[i].GetEnum<T>();
        }

        return result;
    }

    #endregion

    #region SpriteAtlas

    public static SpriteAtlas GetSpriteAtlas(this XmlNode node)
    {
        XmlAttributeCollection nodeAttributes = node.Attributes;

        string pathToAtlas = nodeAttributes["path"].GetString();

        return Resources.Load<SpriteAtlas>(pathToAtlas);
    }

    public static SpriteAtlas[] GetSpriteAtlases(this XmlNodeList nodes)
    {
        SpriteAtlas[] result = new SpriteAtlas[nodes.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = nodes[i].GetSpriteAtlas();
        }

        return result;
    }

    #endregion

    #region Dice

    public static Dice GetDice(this XmlNode node)
    {
        XmlNode spriteAtlasNode = node["EdgesSprites"];

        SpriteAtlas spriteAtlas = spriteAtlasNode.GetSpriteAtlas();

        Dice result = new Dice(
            node["EdgesAmount"].GetInt(),
            node["Type"].GetEnum<DiceType>(),
            spriteAtlasNode.SelectNodes("Sprite").GetDicesEdgeSprites(spriteAtlas));

        return result;
    }

    public static Dice[] GetDices(this XmlNodeList nodes)
    {
        Dice[] result = new Dice[nodes.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = nodes[i].GetDice();
        }

        return result;
    }

    #endregion

    #region DiceEdgeSprites

    public static DiceEdgeSprite GetDiceEdgeSprite(this XmlNode node, SpriteAtlas spriteAtlas)
    {
        string spriteName = node["Name"].GetString();

        DiceEdgeSprite result = new DiceEdgeSprite(
            node["ID"].GetInt(),
            spriteAtlas.GetSprite(spriteName));

        return result;
    }

    public static DiceEdgeSprite[] GetDicesEdgeSprites(this XmlNodeList nodes, SpriteAtlas spriteAtlas)
    {
        DiceEdgeSprite[] result = new DiceEdgeSprite[nodes.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = nodes[i].GetDiceEdgeSprite(spriteAtlas);
        }

        return result;
    }

    #endregion

    #region PlayerStatBonus

    public static PlayerStatBonus GetPlayerStatBonus(this XmlNode node)
    {
        PlayerStatBonus result = new PlayerStatBonus(
            node["ID"].GetInt(),
            node["BonusAmount"].GetInt(),
            node["Name"].GetString(),
            node["Stat"].GetEnum<PlayerStat>());

        return result;
    }

    public static PlayerStatBonus[] GetPlayerStatsBonuses(this XmlNodeList nodes)
    {
        PlayerStatBonus[] result = new PlayerStatBonus[nodes.Count];

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = nodes[i].GetPlayerStatBonus();
        }

        return result;
    }

    #endregion
}
