using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button creditsButton;
    public Button exitButton;


    // Start is called before the first frame update
    void Awake()
    {
        startButton.onClick.AddListener(GameManager.Instance.LevelSelector);
        exitButton.onClick.AddListener(GameManager.Instance.ExitGame);
        creditsButton.onClick.AddListener(GameManager.Instance.TheEnd);

    }

}
