using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] InputSystem_Object inputObj;
    public float speed = 10f;
    public float timeToLive = 2f;
    public float damage = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputObj.actions.Player.Attack.performed += Attack_performed;
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        var bullet = Bullet.Spawn(transform.position, Vector2.up, speed, gameObject, 1, timeToLive, Color.white);
        Debug.Log($"Fire: {bullet}");
    }
}
