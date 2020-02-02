using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private float doorOpeningTime = 1;

    private bool isMoving = false;
    private Vector2 targetPosition;
    private Vector2 originalPosition;
    private Vector2 currVelocity = new Vector2(); // Needed for Vector2.SmoothDamp().


    void Awake()
    {
        originalPosition = transform.position;
    }
    void Update()
    {
        if(isMoving == true)
        {
            Move();
        }
    }

    public void Open()
    {
        BeginMoving(-1);
    }

    public void Close()
    {
        BeginMoving(1);
    }


    private void BeginMoving(int distance)
    {
        isMoving = true;
        targetPosition = originalPosition;
        targetPosition.x += distance * transform.localScale.x;
        originalPosition = targetPosition;
    }
    private void Move()
    {
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref currVelocity, doorOpeningTime);
        if(currVelocity == new Vector2()) // If (speed is 0).
        {
            isMoving = false;
        }
    }
}
