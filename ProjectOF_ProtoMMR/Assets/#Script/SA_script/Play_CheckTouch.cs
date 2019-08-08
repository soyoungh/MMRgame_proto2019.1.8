using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Play_CheckTouch : MonoBehaviour
{
    public static Touch touch;
    public Ui_CamZoom ins_zoom;
    public Ui_CamOnOff ins_onoff;
    public Play_DrawDrag ins_drawdrag;
    public bool TutorialCheck = false;
    public static bool SpineCheck = false;

    public delegate void PlayDelegate();

    public static event PlayDelegate OnTouchBegan_FromGapMove;
    public static event PlayDelegate OnTouchMoved_FromGapMove;

    public static event PlayDelegate DragBoxStart_FromDrag;
    public static event PlayDelegate DragBoxEnd_FromDrag;

    public static event PlayDelegate TouchMoved_FromAnswer;
    public static event PlayDelegate TouchEnd_FromAnswer;

    //public static event PlayDelegate SpineStart_FromSpine;
    //public static event PlayDelegate SpineMoved_FromSpine;


    void Update()
    {
        //print("스파인터치 : " + SpineCheck);
        if (TutorialCheck)
        {
            if (Input.touchCount == 1)
            {
                touch = Input.GetTouch(0);


                if (touch.phase == TouchPhase.Began)
                {
                    //SpineStart_FromSpine();
                    OnTouchBegan_FromGapMove();// 드래그이동(MouseGapMove) Began
                    if (DragBoxEnd_FromDrag != null && ins_onoff.Iscam && Input.touchCount < 2)
                        DragBoxStart_FromDrag();// 드래그박스(DrawDrag) 시작
                                                //랜더뷰때 drawdrag가 비활성화돼서 널체크로 오류방지해야함
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    //SpineMoved_FromSpine();
                    if (TouchMoved_FromAnswer != null && ins_drawdrag.isClicked_DrawingBox)
                        TouchMoved_FromAnswer();//FRA
                    if (OnTouchMoved_FromGapMove != null && !ins_zoom.IsTouch2 && !ins_onoff.Iscam)
                    {
                        OnTouchMoved_FromGapMove();//드래그이동(MouseGapMove) Moved 이때 스파인터치 막기
                        SpineCheck = true;
                        print("스파인터치 : " + SpineCheck);
                    }
                }else if (touch.phase == TouchPhase.Ended)
                {
                    SpineCheck = false;
                    print("스파인터치 : " + SpineCheck);
                    if (TouchMoved_FromAnswer != null && ins_drawdrag.isClicked_DrawingBox)
                        TouchEnd_FromAnswer();//FRA
                    if (DragBoxEnd_FromDrag != null && ins_onoff.Iscam && Input.touchCount < 2)
                        DragBoxEnd_FromDrag();// 드래그박스(DrawDrag) 끝
                                              //랜더뷰때 drawdrag가 비활성화돼서 널체크로 오류방지해야함
                }

            }
        }
    }
}
//포인터가 가리키는게 버튼 유아이일때만 무시하게 하는법 찾기[0710]