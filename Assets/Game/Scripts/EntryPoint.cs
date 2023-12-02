using UnityEngine;

/// <summary>
/// Используется для того чтобы задать порядок инициализации монобехов.
/// </summary>
public class EntryPoint : MonoBehaviour
{
    [Header("Services")]
    [SerializeField]
    private DicesService _dicesService;
    [SerializeField]
    private PlayerStatsBonusesService _playerStatsBonusesService;
    [SerializeField]
    private FloatingTextsService _floatingTextsService;  


    [Header("Panels")]
    [SerializeField]
    private PlayerStatCheckPanel _playerStatCheckPanel;
    
    private void Awake()
    {
        _dicesService.Initialize();

         _playerStatsBonusesService.Initialize();

        _floatingTextsService.Initialize();

        _playerStatCheckPanel.Initialize(GenerateCheck());
    }

    private PlayerStatCheck GenerateCheck()
    {
        Dice d20 = _dicesService.GetDiceByType(DiceType.D20);

        PlayerStatCheck result = new PlayerStatCheck(
            Random.Range(10, d20.EdgesAmount),
            RandomHelper.GetRandomEnumValue<PlayerStat>(),
            d20);

        return result;
    }
}
