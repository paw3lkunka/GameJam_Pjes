using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    
    [SerializeField] private float jumpForce = 1.0f;

    [SerializeField] private int jumpLimit = 1;

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

    [SerializeField] private LayerMask playerLayerMask;

    [HideInInspector]
    public List<Switch> switchesInRange;

    private NewInput input;
    private Vector2 simpleMove;

    private int currJumpLimit;

    private new Rigidbody2D rigidbody;
    private Collider2D collider2d;

    [Header("Sounds")]

    private AudioSource audioSource;

    [SerializeField]
    private List<AudioClip> interactionClips;
    [SerializeField]
    private List<AudioClip> jumpClips;

    #region MonoBehaviour

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<Collider2D>();
        currJumpLimit = jumpLimit;
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (simpleMove.sqrMagnitude > 0)
        {
            if(LevelManager.Instance.Gravity)
            {
                var rbVel = rigidbody.velocity;
                rbVel.x = simpleMove.x * speed;
                rigidbody.velocity = rbVel;
            }
            else
            {
                rigidbody.velocity = simpleMove * speed;
            }
        }
    }

    void OnEnable()     // Required for NewInput system.
    {
        input = LevelManager.Instance.input;

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

        input = null;
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

            PlayInteractionSound();
            closestSwitch.Use();
        }
    }

    private void JumpPerformed(InputAction.CallbackContext ctx)
    {
        if (IsGrounded())
        {
            currJumpLimit = jumpLimit;
        }

        if (jumpEnabled && currJumpLimit > 0)
        {
            PlayJumpSound();
            rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            currJumpLimit--;
        }
    }

    private void MovePerformed(InputAction.CallbackContext ctx)
    {
        simpleMove = ctx.ReadValue<Vector2>();

        if(!moveUpEnabled && simpleMove.y > 0)
        {
            simpleMove.y = 0;
            simpleMove.x /= 0.7f;
        }

        if(!moveDownEnabled && simpleMove.y < 0)
        {
            simpleMove.y = 0;
            simpleMove.x /= 0.7f;
        }

        if(!moveLeftEnabled && simpleMove.x < 0)
        {
            simpleMove.x = 0;
            simpleMove.y /= 0.7f;
        }

        if(!moveRightEnabled && simpleMove.x > 0)
        {
            simpleMove.x = 0;
            simpleMove.y /= 0.7f;
        }
    }
    
    private void MoveCanceled(InputAction.CallbackContext ctx)
    {
        simpleMove = new Vector2();
    }
    #endregion

    private bool IsGrounded()
    {
        var raycastHit2d = Physics2D.CapsuleCast(collider2d.bounds.center, collider2d.bounds.size, CapsuleDirection2D.Horizontal, 0.0f, Vector2.down, 0.1f, ~playerLayerMask);
        return raycastHit2d.collider != null;
    }

    private void PlayInteractionSound()
    {
        if(interactionClips.Count != 0)
        {
            var clipToPlay = interactionClips[Random.Range(0, interactionClips.Count - 1)];
            audioSource.PlayOneShot(clipToPlay);
        }
    }

    private void PlayJumpSound()
    {
        if(jumpClips.Count != 0)
        {
            var clipToPlay = jumpClips[Random.Range(0, jumpClips.Count - 1)];
            audioSource.PlayOneShot(clipToPlay);
        }
    }
}
