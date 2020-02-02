using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if !UNITY_EDITOR && UNITY_WSA
using System.Threading.Tasks;
#endif


public class LevelComplete : MonoBehaviour
{
    private const float MIN_TIME_TO_SHOW = 2.0f;


    /// <summary>
    /// Flag to tell whether a scene is being loaded or not
    /// </summary>
    public bool isLoading { get; private set; }
    /// <summary>
    /// Elapsed time since the new scene started loading
    /// </summary>
    private float timeElapsed;

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void NextLevel()
    {
        GameManager.Instance.NextLevel();
        Hide();
    }

}
