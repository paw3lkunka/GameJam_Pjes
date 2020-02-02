using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject left; // up if vertical
    public GameObject right;
    [SerializeField]
    private float doorOpeningTime = 1;

    private Vector2 neutralPosL;
    private Vector2 neutralPosR;

    private void Awake()
    {
        neutralPosL = left.transform.position;
        neutralPosR = right.transform.position;
    }


    public void Open()
    {
        StopAllCoroutines();
        gameObject.SetActive(true);
        StartCoroutine(Move(true, true));
        StartCoroutine(Move(false, true));
    }

    public void Close()
    {
        StopAllCoroutines();
        StartCoroutine(Move(true, false));
        StartCoroutine(Move(false, false));
    }

    private IEnumerator Move(bool left, bool open)
    {
        GameObject wing = left ? this.left : this.right;
        Vector2 currVelocity = new Vector2();
        Vector2 offsett = (left ? new Vector2(-.5f, 0) : new Vector2(.5f, 0));
        Vector2 target = (left ? neutralPosL : neutralPosR) + (open ? offsett : Vector2.zero);
        while (Vector2.Distance(wing.transform.position, target) > 0.001f)
        {
            wing.transform.position = Vector2.SmoothDamp(wing.transform.position, target, ref currVelocity, doorOpeningTime);
            yield return new WaitForEndOfFrame();
        }
    }
}