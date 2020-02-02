using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheEnd : MonoBehaviour
{
    public Button menuButton;


    // Start is called before the first frame update
    void Awake()
    {
        menuButton.onClick.AddListener(GameManager.Instance.LevelSelector);

    }
}
