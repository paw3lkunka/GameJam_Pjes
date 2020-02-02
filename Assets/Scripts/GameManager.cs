using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private GameObject guiInstance;

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
        
        guiInstance = Instantiate(guiPrefab);
        guiInstance.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(ReloadLevel);
        DontDestroyOnLoad(guiInstance);
        guiInstance.SetActive(false);

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
        actualSceneIndex += 1;
        Debug.Log(actualSceneIndex.ToString());

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
            guiInstance.SetActive(false);
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

    public void LoadLevel(int index)
    {
        if(index < levelScenesNames.Count)
        {
            actualSceneIndex = index;
            loadingScreenInstance.GetComponent<LoadingScreen>().Show(SceneManager.LoadSceneAsync(levelScenesNames[index]));
            guiInstance.SetActive(true);
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
        Destroy(guiInstance);
    }
    #endregion
}
