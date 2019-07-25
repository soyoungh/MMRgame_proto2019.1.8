using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_CheckPrologue : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.HasKey("SavePrologue"))
        {
            gameObject.GetComponent<Play_SceneManager_Progress>().NextSceneNum = 2;
            print(PlayerPrefs.GetInt("SavePrologue"));
        }
    }
}
