using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject HPBar;
    [SerializeField] private HPElement HPPrefab;
    [SerializeField] private Sprite HPFull;
    [SerializeField] private Sprite HPEmpty;

    private int _currentHP = 0;
    private int _maxHP = 0;
    private List<HPElement> _hpElements = new List<HPElement>();

    private void Start()
    {
        _currentHP = PlayerManager.Instance.GetPlayerCurrentHP();
        _maxHP = PlayerManager.Instance.GetPlayerMaxHP();
        ClearHPBar();
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

    private void ClearHPBar()
    {
        for (int i = 0; i < HPBar.transform.childCount; i++)
        {
            Destroy(HPBar.transform.GetChild(i).gameObject);
        }
    }

    private void DrawHPBar()
    {
        for (int i = 0; i < _maxHP; i++)
        {
            _hpElements.Add(Instantiate(HPPrefab, HPBar.transform));
        }
    }

    private void OnPlayerHPChanged(PlayerHPChangedEvent eventData)
    {
        _currentHP = eventData.CurrentHP;
        _maxHP = eventData.MaxHP;

        for (int i = 0; i < _hpElements.Count; i++)
        {
            _hpElements[i].GetComponent<Image>().sprite = (i < _currentHP) ? HPFull : HPEmpty;
        }
    }
}
