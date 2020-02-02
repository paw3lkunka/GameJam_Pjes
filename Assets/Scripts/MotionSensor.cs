using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MotionSensor : Laser
{
    [ReadOnly]
    public bool active; 

    public UnityEvent OnEnter;
    public UnityEvent OnExit;


    new protected void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(head.transform.position, DirVector, float.PositiveInfinity, layerMask);
        SetLine(hit);

        RigidbodyType2D targetType;
        try
        {
            targetType = hit.collider.GetComponent<Rigidbody2D>().bodyType; 
        }
        catch
        {
            targetType = RigidbodyType2D.Static;
        }

        if ( !active && targetType == RigidbodyType2D.Dynamic  )
        {
            OnEnter.Invoke();
            active = true;
        }
        else if( active && targetType != RigidbodyType2D.Dynamic )
        {
            OnExit.Invoke();
            active = false;
        }
    }
}
