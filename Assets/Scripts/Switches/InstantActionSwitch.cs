using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstantActionSwitch : Switch
{
    public UnityEvent OnPush;
    public void Use()
    {
        OnPush.Invoke();
    }
}
