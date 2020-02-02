using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Teleport pairedTeleport;
    private bool detectingEnabled = true;
    private Vector2 pairedTeleportPosition;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip buzzClip;
    [SerializeField]
    private AudioClip transitionClip;

    void Awake()
    {
        pairedTeleportPosition = pairedTeleport.transform.position;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = buzzClip;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        audioSource.volume = 0.3f;
        audioSource.Play();
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
        PlayTransitionSound();
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

    private void PlayTransitionSound()
    {
        audioSource.PlayOneShot(transitionClip, 1);
    }
}
