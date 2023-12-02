using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Визуализирует сторону кости
/// </summary>
[RequireComponent(typeof(Image))]
public class DiceVisibleEdgeView : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private Image _image;

    private int _id;

    private Dice _diceInfo;

    public int ID
    {
        get => _id;

        set
        {
            int minEdgesAmount = Dice.MIN_EDGES_AMOUNT;
            int edgesAmount = _diceInfo.EdgesAmount;

            if(value < minEdgesAmount)
            {
                _id = minEdgesAmount;
            }
            else if(value > edgesAmount)
            {
                _id = edgesAmount;
            }
            else
            {
                _id = value;
            }

            _image.sprite = _diceInfo.GetEdgeSpriteByID(_id);
        }
    }

    private void OnValidate()
    {
        if(_image == null)
        {
            _image = GetComponent<Image>();
        }
    }

    public void Initialize(Dice diceInfo)
    {
        _diceInfo = diceInfo;

        ID = _diceInfo.EdgesAmount;
    }
}