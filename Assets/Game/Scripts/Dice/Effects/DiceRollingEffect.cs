using DG.Tweening;
using System;
using UnityEngine;

[RequireComponent(typeof(DiceVisibleEdgeView))]
public class DiceRollingEffect : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private Dice20RollingEffectParameters _parameters;

    [Header("Components")]
    [SerializeField]
    private DiceVisibleEdgeView _visibleEdge;

    private Tween _tween;

    private Dice _diceInfo;

    public event Action Activated;
    public event Action Deactivated;

    private void OnValidate()
    {
        if(_visibleEdge == null)
        {
            _visibleEdge = GetComponent<DiceVisibleEdgeView>();
        }
    }

    public void Initialize(Dice diceInfo)
    {
        _diceInfo = diceInfo;
    }

    public void Activate()
    {
        if (_tween == null)
        {
            _tween = transform.DORotate(new Vector3(0, _parameters.Angle, 0), _parameters.Duration, _parameters.RotateMode) // инициализации анимации вращения спрайта кости
                 .SetLoops(_parameters.StepAmount, LoopType.Yoyo)
                 .OnStepComplete(OnStepCompleted)
                 .OnComplete(OnComplete)
                 .SetAutoKill(false);
        }
        else
        {
            _tween.Restart();
        }

        Activated?.Invoke();
    }

    public void Deactivate()
    {
        if (_tween.IsPlaying())
        {
            _tween.Pause();
        }

        transform.rotation = Quaternion.Euler(Vector2.zero);

        Deactivated?.Invoke();
    }

    #region EventsHandlers

    private void OnComplete()
    {
        Deactivate();
    }

    private void OnStepCompleted()
    {
        _visibleEdge.ID = _diceInfo.Roll(); // каждое вращение задаем рандомный спрайт стороны кости
    }

    #endregion
}
