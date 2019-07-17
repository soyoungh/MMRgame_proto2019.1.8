using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_control : MonoBehaviour
{
    public Ui_CamZoom ins_zoom;
    public Play_CheckTouch ins_touch;
    public Button CameraMode;
    public GameObject RenderView;

    bool iszoomed = true;
    bool ismoved = true;
    bool ismoved2 = true;
    bool isdraged = true;
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            if (ins_zoom.IsTouch2 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (!iszoomed)
                {
                    PlayAnim();
                    iszoomed = true;
                }
            }
            if (!ismoved && Play_CheckTouch.touch.phase == TouchPhase.Moved) ismoved2 = false;
            if (!ismoved2 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                PlayAnim();
                ismoved = true;
                ismoved2 = true;
            }
            if (!isdraged && RenderView.activeSelf) PlayAnim();
        }
    }
    public void ZoomActive()
    {
        ins_zoom.enabled = true;
        iszoomed = false;
    }
    public void MoveActive()
    {
        ins_touch.enabled = true;
        ismoved = false;
    }
    public void CameraModeActive()
    {
        CameraMode.enabled = true;
        isdraged = false;
    }
    public void PauseAnim()
    {
        GetComponent<Animator>().speed = 0;
    }
    public void PlayAnim()
    {
        GetComponent<Animator>().speed = 1;
    }
    
}