using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public string mainMenuSceneName;
    public string levelSelectSceneName;
    [SerializeField]
    public List<string> levelScenesNames;
    

    public static GameManager Instance { get; private set; }

    [SerializeField]
    public GameObject loadingScreenPrefab;
    private GameObject loadingScreenInstance;

    [SerializeField]
    public GameObject eventSystem;
    private GameObject eventSystemInstance;

    [SerializeField]
    public GameObject guiPrefab;
    public GameObject GuiInstance { get; private set; }

    [SerializeField]
    private int actualSceneIndex;

    public int LevelsCompleted { get; private set; }

    #region MonoBehaviour
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        loadingScreenInstance = Instantiate(loadingScreenPrefab, Vector3.zero, Quaternion.identity);
        eventSystemInstance = Instantiate(eventSystem);

        DontDestroyOnLoad(loadingScreenInstance);
        DontDestroyOnLoad(eventSystemInstance);
        
        GuiInstance = Instantiate(guiPrefab);
        GuiInstance.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(ReloadLevel);
        DontDestroyOnLoad(GuiInstance);
        GuiInstance.SetActive(false);

        actualSceneIndex = -1;
        LevelsCompleted = 15;
    }

    private void Update()
    {
        
    }

    #endregion

    #region SceneManagement
    public void NextLevel()
    {
        GuiInstance.transform.GetComponentsInChildren<TextMeshProUGUI>()[0].gameObject.SetActive(true);
        actualSceneIndex += 1;
        GuiInstance.transform.GetComponentsInChildren<TextMeshProUGUI>()[0].gameObject.SetActive(false);
        if ( actualSceneIndex > LevelsCompleted)
        {
            LevelsCompleted++;
        }
        if (actualSceneIndex < levelScenesNames.Count)
        {
            LoadLevel(actualSceneIndex);
        }
        else if(actualSceneIndex == levelScenesNames.Count)
        {
            FlushUselessShit();
            loadingScreenInstance.GetComponent<LoadingScreen>().Show(SceneManager.LoadSceneAsync(mainMenuSceneName));
            GuiInstance.SetActive(false);
            actualSceneIndex = -1;
        }
        else
        {
            //Do nothing
        }

    }

    public void LevelSelector()
    {
        SceneManager.LoadScene(levelSelectSceneName);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void LoadLevel(int index)
    {
        if(index < levelScenesNames.Count)
        {
            actualSceneIndex = index;
            loadingScreenInstance.GetComponent<LoadingScreen>().Show(SceneManager.LoadSceneAsync(levelScenesNames[index]));
            GuiInstance.SetActive(true);
        }
    }

    public void ReloadLevel()
    {
        Debug.Log(actualSceneIndex.ToString());
        SceneManager.LoadScene(levelScenesNames[actualSceneIndex]);   
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void FlushUselessShit()
    {
        Destroy(eventSystemInstance);
        Destroy(gameObject);
        Destroy(GuiInstance);
    }
    #endregion
}
