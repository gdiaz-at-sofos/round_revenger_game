using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private ShootingParameters shootingParams;
    [SerializeField] private Bullet bulletPrefab;

    private readonly Queue<Bullet> pool = new Queue<Bullet>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Prewarm();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Prewarm()
    {
        for (int i = 0; i < shootingParams.prewarmCount; i++)
        {
            Bullet bullet = Create();
            bullet.gameObject.SetActive(false);
            pool.Enqueue(bullet);
        }
    }

    private Bullet Create()
    {
        Bullet bullet = Instantiate(bulletPrefab, transform);
        return bullet;
    }

    public Bullet Get()
    {
        Bullet bullet = null;

        if (pool.Count > 0)
        {
            bullet = pool.Dequeue();
            bullet.gameObject.SetActive(true);
        }
        else if (transform.childCount < shootingParams.maxPoolSize)
        {
            bullet = Create();
            bullet.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Max pool size reached");
        }

        return bullet;
    }

    public void Return(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        pool.Enqueue(bullet);
    }
}
