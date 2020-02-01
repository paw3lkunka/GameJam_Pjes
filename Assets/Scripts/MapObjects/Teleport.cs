using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Teleport pairedTeleport;
    private bool detectingEnabled = true;
    private Vector2 pairedTeleportPosition;

    void Awake()
    {
        pairedTeleportPosition = pairedTeleport.transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(detectingEnabled)
        {
            TeleportObject(col.gameObject);
        }
    }

    private void TeleportObject(GameObject teleportedObject)
    {
        StartCoroutine(PairedTeleportTimeout());
        teleportedObject.transform.position = pairedTeleportPosition;
    }

    private IEnumerator PairedTeleportTimeout() // Used to prevent teleporting back.
    {
        pairedTeleport.detectingEnabled = false;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        pairedTeleport.detectingEnabled = true;
    }
}
