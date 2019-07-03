using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play_SceneManager_Button : MonoBehaviour
{
    public int SceneNumber;

    public void NextScene()
    {
        SceneManager.LoadScene(SceneNumber);
    }
}
