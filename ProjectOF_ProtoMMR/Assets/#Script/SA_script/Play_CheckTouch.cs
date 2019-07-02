using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Play_CheckTouch : MonoBehaviour
{
    public static Touch touch;
    public Ui_CamZoom ins_zoom;
    public Ui_CamOnOff ins_onoff;
    public Play_DrawDrag ins_drawbox;

    public delegate void PlayDelegate();
    //bool용 delegate 만들어야함(for Image find right answer)0701

    public static event PlayDelegate OnTouchBegan_FromGapMove;
    public static event PlayDelegate OnTouchMoved_FromGapMove;

    public static event PlayDelegate DragBoxStart_FromDrag;
    public static event PlayDelegate DragBoxEnd_FromDrag;

    public static event PlayDelegate TouchMoved_FromAnswer;
    public static event PlayDelegate TouchEnd_FromAnswer;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            touch = Input.GetTouch(0);


            if(touch.phase == TouchPhase.Began)
            {
                OnTouchBegan_FromGapMove();// 드래그이동(MouseGapMove) Began
                if (ins_onoff.Iscam && Input.touchCount < 2)// 드래그박스(DrawDrag)
                    DragBoxStart_FromDrag();
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (ins_drawbox.isClicked_DrawingBox)//이거 체크해야지 오류안나네 시발
                    TouchMoved_FromAnswer();//FRA
                if (!ins_zoom.IsTouch2 && !ins_onoff.Iscam)//드래그이동(MouseGapMove) Moved
                    OnTouchMoved_FromGapMove();
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                if(ins_drawbox.isClicked_DrawingBox)
                    TouchEnd_FromAnswer();//FRA
                if (ins_onoff.Iscam && Input.touchCount < 2)// 드래그박스(DrawDrag)
                    DragBoxEnd_FromDrag();
            }

        }
    }
}
