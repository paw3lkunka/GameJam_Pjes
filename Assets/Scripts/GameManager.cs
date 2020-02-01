﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string mainMenuSceneName;
    [SerializeField]
    public List<string> sceneNames;
    

    public static GameManager Instance { get; private set; }

    [SerializeField]
    public GameObject loadingScreenPrefab;
    private GameObject loadingScreenInstance;

    [SerializeField]
    public GameObject eventSystem;
    private GameObject eventSystemInstance;

    private int actualSceneIndex;

    #region MonoBehaviour
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this);
            return;
        }
        
    }

    private void Start()
    {
        loadingScreenInstance = Instantiate(loadingScreenPrefab, Vector3.zero, Quaternion.identity);
        eventSystemInstance = Instantiate(eventSystem);
        DontDestroyOnLoad(loadingScreenInstance);
        DontDestroyOnLoad(eventSystem);
        actualSceneIndex = -1;
    }

    private void Update()
    {
        
    }

    #endregion

    #region SceneManagement
    public void NextLevel()
    {
        actualSceneIndex += 1;
        Debug.Log(actualSceneIndex.ToString());
        if(actualSceneIndex < sceneNames.Count)
        {
            loadingScreenInstance.GetComponent<LoadingScreen>().Show( SceneManager.LoadSceneAsync(sceneNames[actualSceneIndex]) );
        }
        else if(actualSceneIndex == sceneNames.Count)
        {
            FlushUselessShit();
            loadingScreenInstance.GetComponent<LoadingScreen>().Show(SceneManager.LoadSceneAsync(mainMenuSceneName));
            actualSceneIndex = -1;
        }
        else
        {
            //Do nothing
        }

    }

    public void ReloadLevel()
    {
        SceneManager.LoadSceneAsync(sceneNames[actualSceneIndex]);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void FlushUselessShit()
    {
        Destroy(eventSystemInstance);
        Destroy(gameObject);
    }
    #endregion

}
