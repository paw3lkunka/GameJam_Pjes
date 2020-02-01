using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 2.0f;
    public float jumpForce = 1.0f;

    public bool interactEnabled = true;

    public bool jumpEnabled = false;

    public bool moveUpEnabled = false;
    public bool moveDownEnabled = false;
    public bool moveRightEnabled = false;
    public bool moveLeftEnabled = false;
    [HideInInspector]
    public List<GameObject> switchesInRange;

    private NewInput input;
    private Vector2 simpleMove;

    private new Rigidbody2D rigidbody;

    #region MonoBehaviourMethods

    void Awake()
    {
        input = new NewInput();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(simpleMove.sqrMagnitude > 0)
        {
            var nextPos = transform.position + new Vector3(simpleMove.x, simpleMove.y) * speed;
            Vector3 velocity = new Vector3();
            transform.position = Vector3.SmoothDamp(transform.position, nextPos, ref velocity, 0.12f);
        }
    }

    void OnEnable()     // Required for NewInput system.
    {
        input.Gameplay.Interact.performed += InteractPerformed;
        input.Gameplay.Interact.Enable();

        input.Gameplay.Jump.performed += JumpPerformed;
        input.Gameplay.Jump.Enable();

        input.Gameplay.Move.performed += MovePerformed;
        input.Gameplay.Move.canceled += MoveCanceled;
        input.Gameplay.Move.Enable();
    }

    void OnDisable()    // Required for NewInput system.
    {
        input.Gameplay.Interact.performed -= InteractPerformed;
        input.Gameplay.Interact.Disable();

        input.Gameplay.Jump.performed -= JumpPerformed;
        input.Gameplay.Jump.Disable();

        input.Gameplay.Move.performed -= MovePerformed;
        input.Gameplay.Move.canceled -= MoveCanceled;
        input.Gameplay.Move.Disable();
    }

    #endregion

    #region InputMethods

    private void InteractPerformed(InputAction.CallbackContext ctx)
    {
        if(interactEnabled)
        {
            var closestSwitch = switchesInRange[0];
            var minDistance = Vector3.Distance(transform.position, switchesInRange[0].transform.position);
            
            for(int i = 1; i < switchesInRange.Count; i++)
            {
                var nextDistance = Vector3.Distance(transform.position, switchesInRange[i].transform.position);

                if(minDistance > nextDistance)
                {
                    minDistance = nextDistance;
                    closestSwitch = switchesInRange[i];
                }
            }

            closestSwitch.GetComponent<Switch>().Use();
        }
    }

    private void JumpPerformed(InputAction.CallbackContext ctx)
    {
        if(jumpEnabled)
        {
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void MovePerformed(InputAction.CallbackContext ctx)
    {
        simpleMove = ctx.ReadValue<Vector2>();

        if(!moveUpEnabled && simpleMove.y > 0)
        {
            simpleMove.y = 0;
        }

        if(!moveDownEnabled && simpleMove.y < 0)
        {
            simpleMove.y = 0;
        }

        if(!moveLeftEnabled && simpleMove.x < 0)
        {
            simpleMove.x = 0;
        }

        if(!moveRightEnabled && simpleMove.x > 0)
        {
            simpleMove.x = 0;
        }
    }
    
    private void MoveCanceled(InputAction.CallbackContext ctx)
    {
        simpleMove = new Vector2();
    }

    #endregion

}
