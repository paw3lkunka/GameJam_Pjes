using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 2.0f;

    public bool jumpEnabled = false;
    public bool interactEnabled = true;
    public bool moveUpEnabled = false;
    public bool moveDownEnabled = false;
    public bool moveRightEnabled = false;
    public bool moveLeftEnabled = false;

    private NewInput input;
    private Vector2 simpleMove;

    void Awake()
    {
        input = new NewInput();
    }

    void Update()
    {
        if(simpleMove.sqrMagnitude > 0)
        {
            if(simpleMove.magnitude > 1)
            {
                simpleMove *= 0.7f;
            }
            
            var nextPos = transform.position + new Vector3(simpleMove.x, simpleMove.y) * speed;
            Vector3 velocity = new Vector3();
            transform.position = Vector3.SmoothDamp(transform.position, nextPos, ref velocity, 0.12f);
        }
    }

    void OnEnable() // Required for NewInput system.
    {
        input.Gameplay.MoveUp.performed += MoveUpPerformed;
        input.Gameplay.MoveUp.canceled += MoveUpCanceled;
        input.Gameplay.MoveUp.Enable();

        input.Gameplay.MoveDown.performed += MoveDownPerformed;
        input.Gameplay.MoveDown.canceled += MoveDownCanceled;
        input.Gameplay.MoveDown.Enable();

        input.Gameplay.MoveRight.performed += MoveRightPerformed;
        input.Gameplay.MoveRight.canceled += MoveRightCanceled;
        input.Gameplay.MoveRight.Enable();

        input.Gameplay.MoveLeft.performed += MoveLeftPerformed;
        input.Gameplay.MoveLeft.canceled += MoveLeftCanceled;
        input.Gameplay.MoveLeft.Enable();
    }

    void OnDisable()
    {
        input.Gameplay.MoveUp.performed -= MoveUpPerformed;
        input.Gameplay.MoveUp.canceled -= MoveUpCanceled;
        input.Gameplay.MoveUp.Disable();

        input.Gameplay.MoveDown.performed -= MoveDownPerformed;
        input.Gameplay.MoveDown.canceled -= MoveDownCanceled;
        input.Gameplay.MoveDown.Disable();

        input.Gameplay.MoveRight.performed -= MoveRightPerformed;
        input.Gameplay.MoveRight.canceled -= MoveRightCanceled;
        input.Gameplay.MoveRight.Disable();

        input.Gameplay.MoveLeft.performed -= MoveLeftPerformed;
        input.Gameplay.MoveLeft.canceled -= MoveLeftCanceled;
        input.Gameplay.MoveLeft.Disable();
    }

    #region InputMethods

    private void MoveUpPerformed(InputAction.CallbackContext ctx)
    {
        if(moveUpEnabled)
        {
            simpleMove.y += 1;
        }
    }

    private void MoveUpCanceled(InputAction.CallbackContext ctx)
    {
        simpleMove.y -= 1;
    }

    private void MoveDownPerformed(InputAction.CallbackContext ctx)
    {
        if(moveDownEnabled)
        {
            simpleMove.y += -1;
        }
    }

    private void MoveDownCanceled(InputAction.CallbackContext ctx)
    {
        simpleMove.y -= -1;
    }

    private void MoveRightPerformed(InputAction.CallbackContext ctx)
    {
        if(moveRightEnabled)
        {
            simpleMove.x += 1;
        }
    }

    private void MoveRightCanceled(InputAction.CallbackContext ctx)
    {
        simpleMove.x -= 1;
    }

    private void MoveLeftPerformed(InputAction.CallbackContext ctx)
    {
        if(moveLeftEnabled)
        {
            simpleMove.x += -1;
        }
    }

    private void MoveLeftCanceled(InputAction.CallbackContext ctx)
    {
        simpleMove.x -= -1;
    }

    #endregion

}
