using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public Vector2 direction = Vector2.up;
    public float speed = 10f;
    public GameObject owner;
    public int damage = 1;
    public Color color;
    public float timeToLive = 5;
    static ObjectPool<GameObject> bullets;
    private Rigidbody2D rb;
    [RuntimeInitializeOnLoadMethod]
    static void Init()
    {
        GameObject prefab = Resources.Load<GameObject>("Bullet");
        Debug.Log(prefab);
        bullets = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(prefab),
            actionOnGet: (bullet) => bullet.SetActive(true),
            actionOnRelease: (bullet) => bullet.SetActive(false),
            actionOnDestroy: (bullet) => Destroy(bullet),
            collectionCheck: false,
            defaultCapacity: 10,
            maxSize: 100
        );
    }
    public static Bullet Spawn(Vector2 position, Vector2 direction, float speed, GameObject owner, int damage, float timeToLive, Color color)
    {
        var go = bullets.Get();
        Bullet bullet = go.GetComponent<Bullet>();
        go.transform.position = position;
        bullet.direction = direction.normalized;
        Debug.DrawRay(position, direction.normalized * 2, Color.red, 2f);
        bullet.speed = speed;
        bullet.owner = owner;
        bullet.damage = damage;
        bullet.color = color;
        go.GetComponent<SpriteRenderer>().color = color;
        go.transform.up = direction;
        bullet.timeToLive = timeToLive;
        return bullet;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        timeToLive -= Time.deltaTime;
        if (timeToLive <= 0)
        {
            bullets.Release(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == owner) return;
        Debug.Log($"Hit: {collision.gameObject}");
        collision.gameObject.GetComponent<Health>()?.Damage(damage);
        bullets.Release(this.gameObject);
    }
}
