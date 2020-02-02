using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.InputSystem;

public class SplashScreen : MonoBehaviour
{
    public VideoPlayer player;
    private NewInput input;

    private void Awake()
    {
        player.Play();
    }

    private void OnEnable()
    {
        input = new NewInput();

        input.Gameplay.Jump.performed += SkipSplashScreen;
        input.Gameplay.Jump.Enable();
    }

    private void OnDisable()
    {
        input.Gameplay.Jump.performed -= SkipSplashScreen;
        input.Gameplay.Jump.Disable();
    }

    private void FixedUpdate()
    {
        if (!player.isPlaying)
        {
            GameManager.Instance.MainMenu();
        }
    }

    private void SkipSplashScreen(InputAction.CallbackContext ctx)
    {
        player.Stop();
    }
}
