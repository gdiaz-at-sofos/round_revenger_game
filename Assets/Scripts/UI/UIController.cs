using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject HPBar;
    [SerializeField] private HPElement HPPrefab;
    [SerializeField] private Sprite HPFull;
    [SerializeField] private Sprite HPEmpty;

    private int _currentHP = 0;
    private int _maxHP = 0;

    private void Start()
    {
        _currentHP = PlayerManager.Instance.GetPlayerCurrentHP();
        _maxHP = PlayerManager.Instance.GetPlayerMaxHP();
        DrawHPBar();
    }

    private void OnEnable()
    {
        EventBus<PlayerHPChangedEvent>.Subscribe(GameEvent.PlayerHPChanged, OnPlayerHPChanged);
    }

    private void OnDisable()
    {
        EventBus<PlayerHPChangedEvent>.Unsubscribe(GameEvent.PlayerHPChanged, OnPlayerHPChanged);
    }

    private void DrawHPBar()
    {
        for (int i = 0; i < _maxHP; i++)
        {
            Instantiate(HPPrefab, HPBar.transform);
        }
    }

    private void OnPlayerHPChanged(PlayerHPChangedEvent eventData)
    {
        _currentHP = eventData.CurrentHP;
        _maxHP = eventData.MaxHP;

        for (int i = 0; i < _maxHP; i++)
        {
            if (i < _currentHP)
            {
                HPBar.transform.GetChild(i).GetComponent<Image>().sprite = HPFull;
            }
            else
            {
                HPBar.transform.GetChild(i).GetComponent<Image>().sprite = HPEmpty;
            }
        }
    }
}
