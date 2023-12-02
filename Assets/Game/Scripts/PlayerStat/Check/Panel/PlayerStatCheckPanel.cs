using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// Панель для визуализации проверки статы игрока
/// </summary>
public class PlayerStatCheckPanel : MonoBehaviour
{
    [Header("Effects parameters")]
    [SerializeField]
    private float _durationTextFadeEffect;
    [SerializeField]
    private float _durationDiceShakeEffect;
    [SerializeField]
    private float _strengthDiceShakeEffect;
    [SerializeField]
    private int _vibratoDiceShakeEffect;
    [SerializeField]
    private float _randomessDiceShakeEffect;

    [Header("Texts")]
    [SerializeField]
    private TMP_Text _titleText;
    [SerializeField]
    private TMP_Text _difficultyClassText;
    [SerializeField]
    private TMP_Text _statusText;

    [Header("Dice")]
    [SerializeField]
    private DiceView _diceView;

    [Header("Panels")]
    [SerializeField]
    private LockPanel _lockPanel;
    [SerializeField]
    private PlayerStatsBonusesSelectorPanel _bonusesSelectorPanel;

    [Header("Services")]
    [SerializeField]
    private FloatingTextsService _floatingTextsService;

    private bool _subscribedToEvents;

    private PlayerStatCheck _info;

    private void OnDestroy()
    {
        if (_subscribedToEvents)
        {
            UnSubscribeFromEvents();
        }
    }

    public void Initialize(PlayerStatCheck info)
    {
        _info = info;

        _titleText.text = $"{_info.CheckableStat} check";

        _difficultyClassText.text = $"DIFFICULTY\nCLASS\n{_info.DifficultyClass}";

        _diceView.Initialize(_info.Dice);

        _bonusesSelectorPanel.Initialize(_info.CheckableStat);

        SubscribeToEvents();
    }

    #region EventsHandlers

    private void SubscribeToEvents()
    {
        _diceView.Clicked += OnDiceViewClicked;

        _diceView.RollingEffect.Deactivated += OnDiceViewRollingEffectDeactivated;

        _subscribedToEvents = true;
    }

    private void UnSubscribeFromEvents()
    {
        _diceView.Clicked -= OnDiceViewClicked;

        _diceView.RollingEffect.Deactivated -= OnDiceViewRollingEffectDeactivated;

        _subscribedToEvents = false;
    }

    private void OnDiceViewClicked()
    {
        if (_lockPanel.IsVisible)
        {
            return;
        }

        _statusText.DOFade(0, _durationTextFadeEffect);

        _diceView.RollingEffect.Activate(); // по нажатию на кости активируем Roll эффект и активируем LockPanel чтобы нельзя было выбрать бонусы и запустить Roll по новой

        _lockPanel.SetVisible(true);
    }

    private void OnDiceViewRollingEffectDeactivated()
    {
        PlayerStatCheckResult checkResult = _info.Check(_bonusesSelectorPanel.GetSelectedBonuses());

        _diceView.VisibleEdge.ID = checkResult.RollResult;

        _statusText.text = checkResult.Type.ToString();

        _statusText.DOFade(1, _durationTextFadeEffect);

        _lockPanel.SetVisible(false);

        if(checkResult.BonusesUsed)
        {
            string floatingTextMessage = checkResult.TotalBonusAmount.ToString();

            if(checkResult.TotalBonusAmount > 0) // если бонус положительный, то к сообщению добавим плюс
            {
                floatingTextMessage += "+";
            }

            // создание плавающего текста для последующей анимации добавления бонуса к костям
            _floatingTextsService.CreateFloatingText(floatingTextMessage, _diceView.transform.position * Random.insideUnitCircle, _diceView.transform.position, (floatingTextView) =>
            {
                _diceView.transform.DOShakePosition(_durationDiceShakeEffect, _strengthDiceShakeEffect, _vibratoDiceShakeEffect, _randomessDiceShakeEffect);

                _diceView.VisibleEdge.ID = checkResult.RollResultWithBonus;
            });
        }
    }

    #endregion
}
