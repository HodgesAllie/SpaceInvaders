using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class AlienMove : MonoBehaviour
{
    public float speed = 4;
    public Vector2 bounds = new Vector2(-8f, 8f);
    public sbyte mag = 1;
    public float drop = 1;
    private Rigidbody2D rb;
    public UnityEvent<Collision2D> OnCollision = new();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 delta = Vector2.zero;
        if (mag == 1 && rb.position.x >= bounds.y)
        {
            mag = -1;
            delta += Vector2.down * drop;
        }
        else if (mag == -1 && rb.position.x <= bounds.x)
        {
            mag = 1;
            delta += Vector2.down * drop;
        }
        delta += Vector2.right * mag * Time.fixedDeltaTime * speed;

        rb.MovePosition(rb.position + delta);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollision.Invoke(collision);
    }
}
