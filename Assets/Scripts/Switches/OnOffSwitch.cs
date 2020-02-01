using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnOffSwitch : Switch
{
    [field:SerializeField,ReadOnly]
    public bool State { get; private set; }

    public UnityEvent TurnOn;
    public UnityEvent TurnOff;

    [ContextMenu("Use")]
    public override void Use()
    {
        if (State)
        {
            Off();
        }
        else
        {
            On();
        }
    }

    public void On()
    {
        TurnOn.Invoke();
        State = true;
    }

    public void Off()
    {
        TurnOff.Invoke();
        State = false;
    }
}
