using System;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextsService : MonoBehaviour
{
    [Header("View")]
    [SerializeField]
    private FloatingTextView _viewPrefab;
    [SerializeField]
    private RectTransform _viewsParent;

    private List<FloatingTextView> _views;

    public void Initialize()
    {
        _views = new List<FloatingTextView>();      
    }

    public void CreateFloatingText(string message, Vector2 startPoint, Vector2 endPoint, Action<FloatingTextView> effectCompleted)
    {
        effectCompleted += OnViewEffectCompleted;

        FloatingTextView view = Instantiate(_viewPrefab, startPoint, Quaternion.identity, _viewsParent);

        view.Initialize(message, endPoint, effectCompleted);

        _views.Add(view);
    }

    #region EventsHandlers

    private void OnViewEffectCompleted(FloatingTextView view)
    {
        _views.Remove(view);
    }

    #endregion
}
