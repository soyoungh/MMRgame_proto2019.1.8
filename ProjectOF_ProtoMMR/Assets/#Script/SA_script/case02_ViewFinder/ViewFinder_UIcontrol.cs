using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 뷰파인더활성화시 촬영,리턴 UI컨트롤러
/// </summary>
public class ViewFinder_UIcontrol : MonoBehaviour
{
    public ViewFinder_AutoCamZoom ins_VFzoom;
    public Picture_Save ins_save;
    public Ui_CamOnOff ins_onoff;
    public GameObject LoadImage;
    

    public void OnPhoto()
    {
        print("Take a picture.");
        ins_save.StartPicture_Screen();
    }
    public void OnReturn()
    {
        print("Return to move.");
        LoadImage.SetActive(false);
        ins_onoff.Iscam = false;
        ins_VFzoom.StartCoroutine(ins_VFzoom.CameraAutoMove_OUT());
    }

}
