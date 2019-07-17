using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_GivePicture : MonoBehaviour
{
    public Picture_Save ins_save;
    public void TackA_Picture()
    {
        ins_save.StartPicture_Screen();
        Invoke("InvokeNextScene", 1.5f);
    }
    public void InvokeNextScene()
    {
        GetComponent<Play_SceneManager_Button>().NextScene();
    }
}
