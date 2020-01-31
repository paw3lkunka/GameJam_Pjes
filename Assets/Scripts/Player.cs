using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool jumpEnabled = false;
    public bool interactEnabled = true;
    public bool moveUpEnabled = false;
    public bool moveDownEnabled = false;
    public bool moveRightEnabled = false;
    public bool moveLeftEnabled = false;

    private NewInput input;
    private Vector2 movement;

    void Awake()
    {
        input = new NewInput();
    }

    void Update()
    {
        
    }

    void OnEnable() // Required for NewInput system.
    {
        input.Gameplay.MoveUp.performed += ctx =>
        {
            movement = ctx.ReadValue<Vector2>();
        };
    }

    private void MoveUp()
    {

    }
}
