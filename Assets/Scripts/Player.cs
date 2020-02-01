using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 2.0f;
    public float jumpForce = 1.0f;

    [SerializeField] private bool interactEnabled = true;
    public bool InteractEnabled { get => interactEnabled; set => interactEnabled = value; }

    [SerializeField] private bool jumpEnabled = false;
    public bool JumpEnabled { get => jumpEnabled; set => jumpEnabled = value; }

    [SerializeField] private bool moveUpEnabled = false;
    public bool MoveUpEnabled { get => moveUpEnabled; set => moveUpEnabled = value; }

    [SerializeField] private bool moveDownEnabled = false;
    public bool MoveDownEnabled { get => moveDownEnabled; set => moveDownEnabled = value; }

    [SerializeField] private bool moveRightEnabled = false;
    public bool MoveRightEnabled { get => moveRightEnabled; set => moveRightEnabled = value; }

    [SerializeField] private bool moveLeftEnabled = false;
    public bool MoveLeftEnabled { get => moveLeftEnabled; set => moveLeftEnabled = value; }

    [HideInInspector]
    public List<Switch> switchesInRange;

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

        input.Gameplay.ReloadLevel.performed += ReloadLevel;
        input.Gameplay.ReloadLevel.Enable();
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

        input.Gameplay.ReloadLevel.performed -= ReloadLevel;
        input.Gameplay.ReloadLevel.Disable();
    }

    #endregion

    #region InputMethods

    private void InteractPerformed(InputAction.CallbackContext ctx)
    {
        if(interactEnabled && switchesInRange.Count > 0)
        {
            var closestSwitch = switchesInRange[0];
            var minDistance = Vector3.Distance(transform.position, switchesInRange[0].gameObject.transform.position);
            
            for(int i = 1; i < switchesInRange.Count; i++)
            {
                var nextDistance = Vector3.Distance(transform.position, switchesInRange[i].gameObject.transform.position);

                if(minDistance > nextDistance)
                {
                    minDistance = nextDistance;
                    closestSwitch = switchesInRange[i];
                }
            }

            closestSwitch.Use();
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

    private void ReloadLevel(InputAction.CallbackContext ctx)
    {
        GameManager.Instance.ReloadLevel();
    }

    #endregion

}
