using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(DiceVisibleEdgeView), 
                  typeof(DiceRollingEffect))]
public class DiceView : MonoBehaviour, IPointerClickHandler
{
    [field : SerializeField]
    public DiceVisibleEdgeView VisibleEdge { get; private set; }

    [field: SerializeField]
    public DiceRollingEffect RollingEffect { get; private set; }

    public Dice Info { get; private set; }

    public event Action Clicked;

    private void OnValidate()
    {
        if(VisibleEdge == null)
        {
            VisibleEdge = GetComponent<DiceVisibleEdgeView>();
        }

        if(RollingEffect == null)
        {
            RollingEffect = GetComponent<DiceRollingEffect>();
        }
    }

    public void Initialize(Dice info)
    {
        Info = info;

        VisibleEdge.Initialize(Info);

        RollingEffect.Initialize(Info);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke();
    }
}
