﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.GetComponent<Player>() )
        {
            AudioManager.Instance.PlayCongratsSound();
            GameManager.Instance.NextLevel();
        }
    }
}
