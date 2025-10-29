using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusicSource;

    void Start()
    {
        backgroundMusicSource.Play();
    }
}
