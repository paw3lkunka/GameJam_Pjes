using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private void OnValidate()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                DestroyImmediate(gameObject);
                return;
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        Gravity = Physics2D.gravity != Vector2.zero;
    }
    private void OnDestroy() => instance = null;

    public List<Rigidbody2D> phisicalObjects;

    [SerializeField,ReadOnly]
    private bool gravity;


    // Methodes for use in switches events

    public bool Gravity
    {
        get => gravity;
        set
        {
            gravity = value;
            if ( gravity )
            {
                Physics2D.gravity = Vector2.down;
            }
            else
            {
                Physics2D.gravity = Vector2.zero;
            }
        }
    }


    public void Stop()
    {
        phisicalObjects.ForEach( (rBody) => 
        { 
            rBody.velocity = Vector2.zero;
            rBody.angularVelocity = 0;
        });
    }


}
