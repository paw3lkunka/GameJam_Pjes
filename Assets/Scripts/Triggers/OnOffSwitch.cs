using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnOffSwitch : MonoBehaviour, ISwitch
{
    public bool State { get; private set; }

    public UnityEvent TurnOn;
    public UnityEvent TurnOff;

    public void Use()
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
