using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class FloatingTextView : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private float _effectDuration;
    private Ease _effectEase;

    [Header("Info")]
    [SerializeField]
    private TMP_Text _text;

    private Action<FloatingTextView> _effectCompleted;

    private void OnValidate()
    {
        if (_text == null)
        {
            _text = GetComponent<TMP_Text>();
        }
    }

    public void Initialize(string message, Vector2 endPoint, Action<FloatingTextView> effectCompleted)
    {
        _text.text = message;

        _effectCompleted = effectCompleted;

        DOTween.Sequence()
            .Append(_text.DOFade(0, _effectDuration))
            .Join(transform.DOMove(endPoint, _effectDuration))
            .SetEase(_effectEase)
            .OnComplete(OnEffectCompleted);
    }

    #region EventsHandlers

    private void OnEffectCompleted()
    {
        _effectCompleted?.Invoke(this);

       Destroy(gameObject);
    }

    #endregion
}
