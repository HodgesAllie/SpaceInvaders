using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// WIP, todo move to Library
/// </summary>
public abstract class InputSystem_Abstract<T> : ScriptableObject where T : InputSystem_Abstract<T>, new()
{
    public T Actions { get; private set; }
    public void OnEnable()
    {
        if (Actions == null)
        {
            Actions = new T();
        }
    }

    [Flags]
    public enum On
    {
        None = 0,
        Performed = 1 >> 0,
        Cancled = 1 >> 1,
        Started = 1 >> 2,
        Standard = Performed | Cancled,
        All = Performed | Cancled | Started
    }
    public struct Collection
    {
        Dictionary<InputAction, List<ActionData>> actions;
        struct ActionData
        {
            public On on;
            public Action<InputAction.CallbackContext> callback;
            bool enabled;
        }
        public void Add(InputAction action, Action<InputAction.CallbackContext> callback, On on = On.Standard, bool? enabled = null)
        {
            if (on.HasFlag(On.Performed))
                action.performed += callback;
            if (on.HasFlag(On.Cancled))
                action.canceled += callback;
            if (on.HasFlag(On.Started))
                action.started += callback;
            if (enabled == true) action.Enable();
            else if (enabled == false) action.Disable();

            List<ActionData> datas = new();
            datas.Add(new ActionData() { on = on, callback = callback });
            actions.Add(action, datas);
        }
        public void Remove(InputAction action, Action<InputAction.CallbackContext> callback, On on = On.All)
        {
            if (on.HasFlag(On.Performed))
                action.performed -= callback;
            if (on.HasFlag(On.Cancled))
                action.canceled -= callback;
            if (on.HasFlag(On.Started))
                action.started -= callback;
        }
        public void Remove(InputAction action, On on = On.All)
        {
            if (actions.TryGetValue(action, out var datas))
            {
                foreach (var data in datas)
                {
                    Remove(action, data.callback, on);
                }
            }
        }
        public void Enable()
        {
            throw new NotImplementedException();
        }
        public void Disable()
        {
            throw new NotImplementedException();
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }
    }
}
