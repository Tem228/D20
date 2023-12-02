using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Панель для выбора бонусов
/// </summary>
public class PlayerStatsBonusesSelectorPanel : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    [Min(1)]
    private int _maxSelectedBonusesAmount;

    [Header("Texts")]
    [SerializeField]
    private TMP_Text _totalBonusAmountText;

    [Header("View")]
    [SerializeField]
    private PlayerStatBonusView _viewPrefab;
    [SerializeField]
    private RectTransform _viewsContent;

    [Header("Services")]
    [SerializeField]
    private PlayerStatsBonusesService _playerStatsBonusesService;

    private int _totalBonusAmount;

    private List<PlayerStatBonusView> _views;

    private List<PlayerStatBonusView> _selectedViews;

    public void Initialize(PlayerStat playerStat)
    {
        _totalBonusAmount = 0;

        _views = new List<PlayerStatBonusView>();

        _selectedViews = new List<PlayerStatBonusView>();

        CreateViews(_playerStatsBonusesService.GetBonusesByStat(playerStat));

        DisplayTotalBonusAmount();
    }

    public PlayerStatBonus[] GetSelectedBonuses()
    {
        List<PlayerStatBonus> result = new List<PlayerStatBonus>();

        for (int i = 0; i < _selectedViews.Count; i++)
        {
            result.Add(_selectedViews[i].Info);
        }

        return result.ToArray();
    }

    private void DisplayTotalBonusAmount()
    {
        _totalBonusAmountText.text = $"Total bonus: {_totalBonusAmount}";
    }

    private void CreateView(PlayerStatBonus info)
    {
        PlayerStatBonusView result = Instantiate(_viewPrefab, _viewsContent);

        result.Initialize(info, OnViewSelected, OnViewUnSelected);

        _views.Add(result);
    }

    private void CreateViews(PlayerStatBonus[] infos)
    {
        for (int i = 0; i < infos.Length; i++)
        {
            CreateView(infos[i]);
        }
    }

    private void DestroyBonuses()
    {
        for(int i = 0; i < _views.Count; i++)
        {
            Destroy(_views[i].gameObject);
        }

        _views.Clear();
    }

    #region EventsHandlers

    private void OnViewSelected(PlayerStatBonusView view)
    {
        if(_selectedViews.Count >= _maxSelectedBonusesAmount) // проверка для ограничения количества выбранных бонусов
        {
            return;
        }

        _totalBonusAmount += view.Info.BonusAmount;

        view.MarkAsSelected();

        _selectedViews.Add(view);

        DisplayTotalBonusAmount();
    }

    private void OnViewUnSelected(PlayerStatBonusView view)
    {
        _totalBonusAmount -= view.Info.BonusAmount;

        view.MarkAsUnSelected();

        _selectedViews.Remove(view);

        DisplayTotalBonusAmount();
    }

    #endregion
}
