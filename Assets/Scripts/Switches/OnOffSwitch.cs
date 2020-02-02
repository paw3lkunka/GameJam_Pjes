using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnOffSwitch : Switch
{
    [field:SerializeField,ReadOnly]
    public bool State { get; protected set; }

    private Color32 buttonOff = new Color32(255, 0, 0, 255);
    private Color32 buttonOn = new Color32(0, 255, 0, 255);

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
        gameObject.GetComponent<SpriteRenderer>().color = buttonOn;
        TurnOn.Invoke();
        State = true;
    }

    public void Off()
    {
        gameObject.GetComponent<SpriteRenderer>().color = buttonOff;
        TurnOff.Invoke();
        State = false;
    }
}
