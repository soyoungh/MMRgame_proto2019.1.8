using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_SaveScene : MonoBehaviour
{
    public static int ActiveSceneNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SaveSceneNum"))
        {
            PlayerPrefs.SetInt("SaveSceneNum", ActiveSceneNum);
        }
    }
    
}
