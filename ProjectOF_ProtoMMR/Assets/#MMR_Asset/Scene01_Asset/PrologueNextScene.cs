using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PrologueNextScene : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<VideoPlayer>().isPrepared)
        {
            if (!GetComponent<VideoPlayer>().isPlaying) GetComponent<Play_SceneManager_Button>().NextScene();
        }
    }
}