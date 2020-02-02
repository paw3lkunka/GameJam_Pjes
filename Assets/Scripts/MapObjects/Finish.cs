using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField]
    public GameObject levelCompletePrefab;
    private GameObject levelCompleteInstance;

    private void Start()
    {
        levelCompleteInstance = Instantiate(levelCompletePrefab);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.GetComponent<Player>() )
        {
            levelCompleteInstance.GetComponent<LevelComplete>().Show();
        }
    }
}
