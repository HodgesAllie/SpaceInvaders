using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class AlienSpawner : MonoBehaviour
{
    public float delta;
    public float speed;
    public GameObject SpawnPoint;
    public int2 HealthRange = new(1, 3);
    public GameObject prefab;
    public bool SpawnOnStart = true;
    public bool startRight = true;

    private float timer = 0f;
    private ObjectPool<GameObject> alienPool;

    void Start()
    {
        alienPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(prefab),
            actionOnGet: (bullet) => bullet.SetActive(true),
            actionOnRelease: (bullet) => bullet.SetActive(false),
            actionOnDestroy: (bullet) => Destroy(bullet),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 1000
        );
        if (SpawnOnStart)
            Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= delta)
        {
            timer = 0;
            Spawn();
        }
        timer += Time.deltaTime;
    }

    void Spawn()
    {
        GameObject alien = alienPool.Get();
        var mover = alien.GetComponent<AlienMove>();
        mover.speed = speed;
        alien.transform.position = SpawnPoint.transform.position;
        mover.mag = (sbyte)(startRight ? 1 : -1);
        Health health = alien.GetComponent<Health>();
        health.current = UnityEngine.Random.Range(HealthRange.x, HealthRange.y + 1);
        health.max = health.current;
        void func(Collision2D collision)
        {
            Player p = collision.gameObject.GetComponent<Player>();
            if (p != null)
            {
                alienPool.Release(alien);
                p.GetComponent<Health>()?.Damage(1);
                mover.OnCollision.RemoveListener(func);
            }
        };
        mover.OnCollision.AddListener(func);
    }
}
