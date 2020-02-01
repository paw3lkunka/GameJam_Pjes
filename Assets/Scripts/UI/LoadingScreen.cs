using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    private const float MIN_TIME_TO_SHOW = 1.0f;

    /// <summary>
    /// Reference to current loading operation running in the background
    /// </summary>
    private AsyncOperation currentLoadingOperation;

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
    private void Update()
    {
        if (isLoading)
        {
            if(currentLoadingOperation.isDone)
            {
                Hide();
            }
            else
            {
                timeElapsed += Time.deltaTime;
                if (timeElapsed >= MIN_TIME_TO_SHOW)
                {
                    currentLoadingOperation.allowSceneActivation = true;
                }
            }
        }
    }

    public void Show(AsyncOperation loadingOperation)
    {
        gameObject.SetActive(true);
        currentLoadingOperation = loadingOperation;
        currentLoadingOperation.allowSceneActivation = false;
        timeElapsed = 0.0f;
        isLoading = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        currentLoadingOperation = null;
        isLoading = false;
    }
}
