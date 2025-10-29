using UnityEngine;

public class ProgressUIController : MonoBehaviour
{
    [SerializeField] private GameObject triangleBossProgressUI;
    
    void Start()
    {
       triangleBossProgressUI.SetActive(PlayerPrefs.GetInt("TRIANGLE_LEVEL_PASSED", 0) == 1);
    }
}
