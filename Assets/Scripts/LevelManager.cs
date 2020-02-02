using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour
{
    public bool initialGravity;
    public static LevelManager Instance { get; private set; }
    public List<Rigidbody2D> phisicalObjects;
    private Player player;
    public Player Player
    {
        get
        {
            if(player==null)
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }

            return player;
        }
    }

    public bool Gravity
    {
        get => gravity;
        set
        {
            gravity = value;
            if (gravity)
            {
                Physics2D.gravity = Vector2.down * 9.81f;
                Player.GetComponent<Rigidbody2D>().drag = Player.withinGravityDrag;
            }
            else
            {
                Physics2D.gravity = Vector2.zero;
                Player.GetComponent<Rigidbody2D>().drag = Player.withoutGravityDrag;
            }
        }
    }

    [SerializeField, ReadOnly]
    private bool gravity;

    public NewInput input;

    #region MonoBehaviour

    private void OnValidate()
  {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                if (Application.isPlaying)
                {
                    Destroy(gameObject);
                }
                else
                {
                    DestroyImmediate(gameObject);
                }
                return;
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        
        input = new NewInput();
        Gravity = initialGravity;
        Physics2D.gravity = (Gravity ? 1f : 0f) * Vector2.down * 9.81f;

        AudioManager.Instance.PlayNewMusic();
    }

    private void Start()
    {
        phisicalObjects = new List<Rigidbody2D>(FindObjectsOfType<Rigidbody2D>());
    }

    private void OnDestroy() => Instance = null;

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
        AudioManager.Instance.PlayRestartSound();
        GameManager.Instance.ReloadLevel();
    }

    #endregion

}
