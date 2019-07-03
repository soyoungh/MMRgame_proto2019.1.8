using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Play_SceneManager_Progress : MonoBehaviour
{
    AsyncOperation asyncOper;
    bool PlayReady = false;
    public RectTransform ProgressBar;
    public static int NextSceneNum;

    private void Start()
    {
        StartCoroutine(StartLoad());
    }

    public void Ready()
    {
        PlayReady = true;
    }

    IEnumerator StartLoad()
    {
        asyncOper = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncOper.allowSceneActivation = false;

        while (!asyncOper.isDone)
        {
            yield return null;
            if (asyncOper.progress >= 0.9f)
            {
                if (ProgressBar != null)
                {
                    ProgressBar.localScale = Vector3.one;
                }
                if (PlayReady == true)
                {
                    asyncOper.allowSceneActivation = true;
                }
            }
            else if (ProgressBar != null)
            {
                 ProgressBar.localScale = new Vector3(asyncOper.progress, 1, 1);
            }
        }
    }
}
