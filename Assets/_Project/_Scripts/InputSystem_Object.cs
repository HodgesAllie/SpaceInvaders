using UnityEngine;

[CreateAssetMenu(fileName = "InputSystem_Object", menuName = "Scriptable Objects/InputSystem_Object")]
public class InputSystem_Object : ScriptableObject
{
    public InputSystem_Actions actions;
    private void OnEnable()
    {
        if (actions == null)
        {
            actions = new InputSystem_Actions();
        }
    }
}
