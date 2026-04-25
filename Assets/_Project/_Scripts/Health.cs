using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int current = 1;
    public int max = 1;
    public UnityEvent OnDeath = new();
    public UnityEvent OnDamaged = new();
    public void Damage(int amount)
    {
        current -= amount;
        OnDamaged?.Invoke();
        if (current <= 0)
            OnDeath?.Invoke();
    }
}
