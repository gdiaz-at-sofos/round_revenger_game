using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HUDController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject HPBar;
    [SerializeField] private HPElement HPPrefab;
    [SerializeField] private Sprite HPFull;
    [SerializeField] private Sprite HPEmpty;

    private List<HPElement> _hpElements = new List<HPElement>();

    // NOTE: This is bs
    private bool _isInitialized = false;

    private void Awake()
    {
        for (int i = 0; i < HPBar.transform.childCount; i++)
        {
            Destroy(HPBar.transform.GetChild(i).gameObject);
        }
    }

    private void OnEnable()
    {
        EventBus<PlayerHPChangedEvent>.Subscribe(GameEvent.PlayerHPChanged, OnPlayerHPChanged);
    }

    private void OnDisable()
    {
        EventBus<PlayerHPChangedEvent>.Unsubscribe(GameEvent.PlayerHPChanged, OnPlayerHPChanged);
    }

    private void OnPlayerHPChanged(PlayerHPChangedEvent eventData)
    {
        if (!_isInitialized)
        {
            int maxHP = eventData.MaxHP;
            for (int i = 0; i < maxHP; i++)
            {
                _hpElements.Add(Instantiate(HPPrefab, HPBar.transform));
            }
            _isInitialized = true;
        }

        int currentHP = eventData.CurrentHP;
        for (int i = 0; i < _hpElements.Count; i++)
        {
            _hpElements[i].GetComponent<Image>().sprite = (i < currentHP) ? HPFull : HPEmpty;
        }
    }
}
