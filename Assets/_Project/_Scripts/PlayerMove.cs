using System;
using UnityEngine;
using FlyingWormConsole3;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] InputSystem_Object inputObj;
    [SerializeField] Vector2 range = new Vector2(-8f, 8f);

    private float mag = 0f;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputSystem_Actions b = inputObj.actions;
        InputSystem_Actions.PlayerActions c = inputObj.actions.Player;
        InputAction a = inputObj.actions.Player.Move;
        inputObj.actions.Player.Move.performed += Move_input;
        inputObj.actions.Player.Move.canceled += Move_input;
        inputObj.actions.Player.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Move_input(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        mag = Mathf.Clamp(obj.ReadValue<Vector2>().x, -1, 1);
    }

    private void FixedUpdate()
    {
        Vector2 position = rb.position + Vector2.right * mag * Time.fixedDeltaTime * speed;
        position = new Vector2(Mathf.Clamp(position.x, range.x, range.y), position.y);
        rb.MovePosition(position);
        //ConsoleProDebug.Watch("Player Move", mag.ToString());
    }
}
