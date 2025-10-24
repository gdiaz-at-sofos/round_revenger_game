using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerHPController playerHPController;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerController.transform.position;
    }
}
