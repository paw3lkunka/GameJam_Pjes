using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButtonsGenerator : MonoBehaviour
{
    public GameObject buttonPrefab;
    private GameObject[] buttonInstances;

    private int levelCount;

    private void Start()
    {
        levelCount = GameManager.Instance.levelScenesNames.Count;
        buttonInstances = new GameObject[levelCount];
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(GameManager.Instance.MainMenu);
        Generate();
    }

    private void Generate()
    {
        for(int i = 0; i < levelCount; ++i)
        {
            Vector3 pos = new Vector3(-600 + 300 * (i % 5), 200 - 300 * (i / 5), 0);
            buttonInstances[i] = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity, gameObject.transform);
            buttonInstances[i].transform.localPosition = pos;
            buttonInstances[i].GetComponent<LevelSelectButton>().SceneIndex = i;
            buttonInstances[i].GetComponent<LevelSelectButton>().UpdateButton();
            if(i > GameManager.Instance.LevelsCompleted)
            {
                buttonInstances[i].GetComponent<Button>().interactable = false;
            }
        }
    }
}
