using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Switch : MonoBehaviour
{
    public abstract void Use();

    void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponent<Player>()?.switchesInRange.Add(this);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        col.gameObject.GetComponent<Player>()?.switchesInRange.Remove(this);
    }
}
