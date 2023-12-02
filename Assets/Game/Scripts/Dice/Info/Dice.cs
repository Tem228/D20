using System.Collections.Generic;
using UnityEngine;

public class Dice
{
    public int EdgesAmount { get; private set; }

    public DiceType Type { get; private set; }

    private Dictionary<int, Sprite> _edgesSprites;

    public const int MIN_EDGES_AMOUNT = 1;

    public Dice(int edgesAmount, DiceType diceType, DiceEdgeSprite[] edgeSprites)
    {
        EdgesAmount = edgesAmount < MIN_EDGES_AMOUNT ? MIN_EDGES_AMOUNT : edgesAmount; // количество сторон должно быть больше 1

        Type = diceType;

        InitializeEdgesSprites(edgeSprites);
    }

    public int Roll()
    {
        return Random.Range(MIN_EDGES_AMOUNT, EdgesAmount + 1);
    }

    #region EdgesSprites

    // преобразование массива спрайтов в словарь (для оптимизации и для упрощения доступа к спрайтам
    private void InitializeEdgesSprites(DiceEdgeSprite[] edgeSprites)
    {
        _edgesSprites = new Dictionary<int, Sprite>();

        for (int i = 0; i < edgeSprites.Length; i++)
        {
            DiceEdgeSprite edgeSprite = edgeSprites[i];

            _edgesSprites.Add(edgeSprite.ID, edgeSprite.Sprite);
        }
    }

    public Sprite GetEdgeSpriteByID(int id)
    {
        if (!_edgesSprites.ContainsKey(id))
        {
            return null;
        }

        return _edgesSprites[id];
    }

    #endregion
}
