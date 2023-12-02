using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBonusView : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField]
    private TMP_Text _nameText;
    [SerializeField]
    private TMP_Text _bonusAmountText;

    [Header("Buttons")]
    [SerializeField]
    private Button _selectButton;

    [Header("Toggles")]
    [SerializeField]
    private Toggle _selectToggle;

    private bool _subscribedToEvents;

    private bool _isSelected => _selectToggle.isOn;

    private Action<PlayerStatBonusView> _selected;
    private Action<PlayerStatBonusView> _unSelected;

    public PlayerStatBonus Info { get; private set; }

    private void OnDestroy()
    {
        if (_subscribedToEvents)
        {
            UnSubscribeFromEvents();
        }
    }

    public void Initialize(PlayerStatBonus info, Action<PlayerStatBonusView> selected, Action<PlayerStatBonusView> unSelected)
    {
        Info = info;

        _nameText.text = Info.Name;

        _bonusAmountText.text = Info.BonusAmount.ToString();

        _selected = selected;
        _unSelected = unSelected;

        SubscribeToEvents();
    }

    public void MarkAsSelected()
    {
        _selectToggle.isOn = true;
    }

    public void MarkAsUnSelected()
    {
        _selectToggle.isOn = false;
    }

    #region EventsHandlers

    private void SubscribeToEvents()
    {
        _selectButton.onClick.AddListener(OnSelectButtonClick);

        _subscribedToEvents = true;
    }

    private void UnSubscribeFromEvents()
    {
        _selectButton.onClick.RemoveListener(OnSelectButtonClick);

        _subscribedToEvents = false;
    }

    private void OnSelectButtonClick()
    {
        if (_isSelected)
        {
            _unSelected?.Invoke(this);
            return;
        }

        _selected?.Invoke(this);
    }

    #endregion
}
