using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// only 튜토리얼 제어
/// </summary>
public class Tutorial_control : MonoBehaviour
{
    public Ui_CamZoom ins_zoom;
    public Play_CheckTouch ins_touch;
    public Image_FindRightAnswer ins_FRA;
    public Button CameraMode;
    public GameObject RenderView, hint, back, end;
    public GameObject[] RightAnswer = new GameObject[4];

    public RectTransform DragBox, CheckRange;
    public float SizeRange_min, SizeRange_max;

    bool iszoomed = true;
    bool ismoved = true;
    bool ismoved2 = true;
    bool isCamOn = true;
    bool isdrag = true;

    private void OnEnable()
    {
        ins_zoom.enabled = false;
        ins_touch.TutorialCheck = false;
        ins_FRA.enabled = false;
        hint.SetActive(false);
        back.SetActive(false);
        CameraMode.enabled = false;

        for (int i = 0; i < RightAnswer.Length; i++)
        {
            RightAnswer[i].SetActive(false);
        }
        print("0. 줌인아웃, 드래그이동, ui(hint, back, 카메라, 정답포인트 비활성화)");
    }
    private void OnDisable()
    {
        ins_zoom.enabled = true;
        ins_FRA.enabled = true;
        ins_touch.TutorialCheck = true;
        hint.SetActive(true);
        back.SetActive(true);
        CameraMode.enabled = true;

        for (int i = 0; i < RightAnswer.Length; i++)
        {
            RightAnswer[i].SetActive(true);
        }
        print("5. 줌인아웃, 드래그이동, ui(hint, back, 카메라, 정답포인트 활성화)");
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            if (ins_zoom != null && ins_zoom.IsTouch2 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (!iszoomed)
                {
                    PlayAnim();
                    iszoomed = true;
                    print("# 줌 anim play");
                }
            }
            if (!ismoved2 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                PlayAnim();
                ismoved = true;
                ismoved2 = true;
                ins_FRA.enabled = false;
                print("# 이동 anim play");
            }
            if (!isCamOn && CameraMode.gameObject.GetComponent<Ui_CamOnOff>().Iscam)
            {
                PlayAnim();
                isCamOn = true;
                CameraMode.gameObject.GetComponent<Animator>().enabled = false;
                print("# 카메라버튼 anim play");
            }



            if (!ismoved && Play_CheckTouch.touch.phase == TouchPhase.Moved) ismoved2 = false;
            if (RenderView.activeSelf == true) PlayAnim();// 카메라모드시 플레이

            if (!isdrag)//여기수정[0719]
            {
                DragMoveSizeCheck();
                if (Input.GetTouch(0).phase == TouchPhase.Ended && DragMoveSizeCheck())
                {
                    PlayAnim();// 사진을 찍을시 플레이
                    print("드래그캡쳐(사진찍음)");
                }
            }
            if (end.activeSelf == true && Play_CheckTouch.touch.phase == TouchPhase.Ended)
            {
                PlayAnim();
                end.SetActive(false);
                print("# 모든 튜토리얼종료, 바가 사라지며 게임시작");
            }
        }
    }
    public void ZoomActive()//줌인아웃 가능
    {
        ins_zoom.enabled = true;
        iszoomed = false;
        print("1. 줌인아웃 활성화");
    }
    public void MoveActive()//드래그이동 가능
    {
        ins_touch.TutorialCheck = true;
        ismoved = false;
        print("2. 드래그 이동 활성화");
    }
    public void CameraModeAnim()
    {
        CameraMode.gameObject.GetComponent<Animator>().enabled = true;
    }
    public void CameraModeActive()//카메라 버튼 가능
    {
        CameraMode.enabled = true;
        isCamOn = false;
        print("3. 카메라버튼 활성화");
    }

    public void DragBoxColoring()
    {
        isdrag = false;
    }

    public void EndActive()
    {
        end.SetActive(true);
        print("4. 튜토리얼 종료 텍스트 활성화");
    }
    public void DisableTutorialAnimator()
    {
        GetComponent<Animator>().enabled = false;
        this.gameObject.SetActive(false);
        this.enabled = false;
    }
    
    public void PauseAnim()
    {
        GetComponent<Animator>().speed = 0;
    }
    public void PlayAnim()
    {
        GetComponent<Animator>().speed = 1;
    }

    public bool DragMoveSizeCheck()
    {
        if (DragBox.sizeDelta.x > CheckRange.sizeDelta.x / SizeRange_min && DragBox.sizeDelta.y >= CheckRange.sizeDelta.y / SizeRange_min)
        {
            if (DragBox.sizeDelta.x < CheckRange.sizeDelta.x * SizeRange_max && DragBox.sizeDelta.y < CheckRange.sizeDelta.y * SizeRange_max)
            {
                DragBox.gameObject.GetComponent<Image>().color = Color.green * new Color(1, 1, 1, 0.5f);
                return true;
            }
            else
            {
                DragBox.gameObject.GetComponent<Image>().color = Color.grey * new Color(1, 1, 1, 0.5f);
                return false;
            }
        }
        else
        {
            DragBox.gameObject.GetComponent<Image>().color = Color.grey * new Color(1, 1, 1, 0.5f);
            return false;
        }
    }
}