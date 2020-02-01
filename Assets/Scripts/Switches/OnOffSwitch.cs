using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnOffSwitch : Switch
{
    public bool State { get; private set; }

    public UnityEvent TurnOn;
    public UnityEvent TurnOff;

    public override void Use()
    {
        if (State)
        {
            TurnOff.Invoke();
            State = false;
        }
        else
        {
            TurnOn.Invoke();
            State = true;
        }
    }
}
