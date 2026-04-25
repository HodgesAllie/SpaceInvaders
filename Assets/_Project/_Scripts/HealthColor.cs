using UnityEngine;

[RequireComponent(typeof(Health), typeof(SpriteRenderer))]
public class HealthColor : MonoBehaviour
{
    Health health;
    SpriteRenderer sprite;
    public bool SetOnStart = true;
    public void Start()
    {
        health = GetComponent<Health>();
        sprite = GetComponent<SpriteRenderer>();
        if (SetOnStart) SetColor();
    }
    public void SetColor()
    {
        if (health == null || sprite == null) return;
        // 8 -> 1: white, grey, blue, purple, orange, yellow, red, dark red
        switch (health)
        {
            case var _ when health.current >= 8:
                sprite.color = Color.white;
                break;
            case var _ when health.current == 7:
                sprite.color = Color.gray;
                break;
            case var _ when health.current == 6:
                sprite.color = Color.blue;
                break;
            case var _ when health.current == 5:
                sprite.color = new Color(0.5f, 0f, 0.5f); //purple
                break;
            case var _ when health.current == 4:
                sprite.color = new Color(1f, 0.65f, 0f); //orange
                break;
            case var _ when health.current == 3:
                sprite.color = Color.yellow;
                break;
            case var _ when health.current == 2:
                sprite.color = Color.red;
                break;
            case var _ when health.current == 1:
                sprite.color = new Color(0.5f, 0f, 0f); //dark red
                break;
            default:
                sprite.color = Color.black;
                break;
        }
    }
}
