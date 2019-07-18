using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 맨처음에 오답만 액티브 되어야함
/// </summary>
public class Tutorial_control : MonoBehaviour
{
    public Ui_CamZoom ins_zoom;
    public Play_CheckTouch ins_touch;
    public Button CameraMode;
    public GameObject RenderView, hint, back;
    public GameObject[] RightAnswer = new GameObject[4];

    bool iszoomed = true;
    bool ismoved = true;
    bool ismoved2 = true;
    bool isdraged = true;

    private void OnEnable()
    {
        ins_zoom.enabled = false;
        ins_touch.enabled = false;
        hint.SetActive(false);
        back.SetActive(false);
        CameraMode.enabled = false;

        for (int i = 0; i < RightAnswer.Length; i++)
        {
            RightAnswer[i].SetActive(false);
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < RightAnswer.Length; i++)
        {
            RightAnswer[i].SetActive(true);
        }
    }

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
            if (!ismoved2 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                PlayAnim();
                ismoved = true;
                ismoved2 = true;
            }

            if (!ismoved && Play_CheckTouch.touch.phase == TouchPhase.Moved) ismoved2 = false;
            if (CameraMode.gameObject.GetComponent<Ui_CamOnOff>().Iscam) PlayAnim();
            if (!isdraged) PlayAnim();// 사진찍으면 해제


        }
    }
    public void ZoomActive()//줌인아웃 가능
    {
        ins_zoom.enabled = true;
        iszoomed = false;
    }
    public void MoveActive()//드래그이동 가능
    {
        ins_touch.enabled = true;
        ismoved = false;
    }

    // * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~카메라모드~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
    public void CameraModeActive()//카메라 버튼 가능
    {
        CameraMode.enabled = true;
    }
    public void CameraModeActive_yellow()
    {
        CameraMode.gameObject.GetComponent<Image>().color = Color.yellow;
    }
    public void CameraModeActive_white()
    {
        CameraMode.gameObject.GetComponent<Image>().color = Color.white;
    }
    // * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *

    public void DragBoxDraw()//
    {
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