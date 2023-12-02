using UnityEngine;

public class DiceEdgeSprite 
{
    public int ID { get; private set; }

    public Sprite Sprite { get; private set; }

    public DiceEdgeSprite(int id, Sprite sprite)
    {
        ID = id;

        Sprite = sprite;
    }
}
