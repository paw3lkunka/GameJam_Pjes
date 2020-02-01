﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    public bool initialGravity;
    public static LevelManager instance;
    public List<Rigidbody2D> phisicalObjects;

    public bool Gravity
    {
        get => gravity;
        set
        {
            gravity = value;
            if (gravity)
            {
                Physics2D.gravity = Vector2.down * 9.81f;
            }
            else
            {
                Physics2D.gravity = Vector2.zero;
            }
        }
    }

    [SerializeField, ReadOnly]
    private bool gravity;

    public NewInput input;

    #region MonoBehaviour

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
        input = GameManager.Instance.GameInput;
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
        
        Gravity = initialGravity;
        Physics2D.gravity = (Gravity ? 1f : 0f) * Vector2.down * 9.81f;
    }

    private void Start()
    {
        phisicalObjects = new List<Rigidbody2D>(FindObjectsOfType<Rigidbody2D>());
    }

    private void OnDestroy() => instance = null;

    private void OnEnable()
    {
        input.Gameplay.ReloadLevel.performed += ReloadLevel;
        input.Gameplay.ReloadLevel.Enable();
    }

    private void OnDisable()
    {
        input.Gameplay.ReloadLevel.performed -= ReloadLevel;
        input.Gameplay.ReloadLevel.Disable();
    }

    #endregion
    public void Stop()
    {
        phisicalObjects.ForEach( (rBody) => 
        { 
            rBody.velocity = Vector2.zero;
            rBody.angularVelocity = 0;
        });
    }

    #region InputMethods
    
    private void ReloadLevel(InputAction.CallbackContext ctx)
    {
        GameManager.Instance.ReloadLevel();
    }

    #endregion

}
