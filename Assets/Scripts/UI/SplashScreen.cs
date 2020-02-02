using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SplashScreen : MonoBehaviour
{
    public VideoPlayer player;
    private void Awake()
    {
        player.Play();
    }

    private void FixedUpdate()
    {
        if (!player.isPlaying)
        {
            GameManager.Instance.MainMenu();
        }
    }
}
