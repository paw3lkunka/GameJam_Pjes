using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSelectButton : MonoBehaviour
{
    public int SceneIndex { get; set; }


    public void UpdateButton()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (SceneIndex + 1).ToString();
    }

    public void LoadMyLevel()
    {
        GameManager.Instance.LoadLevel(SceneIndex);
    }
}
