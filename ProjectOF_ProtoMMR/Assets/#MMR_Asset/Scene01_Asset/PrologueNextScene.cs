using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PrologueNextScene : MonoBehaviour
{
    void Update()
    {
        if(GetComponent<VideoPlayer>().frame > 900)
        {
            GetComponent<Play_SceneManager_Button>().NextScene();
        }
    }
}
