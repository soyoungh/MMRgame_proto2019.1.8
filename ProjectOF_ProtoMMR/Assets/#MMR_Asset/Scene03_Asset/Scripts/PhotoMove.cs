using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.EventSystems;


public class PhotoMove : MonoBehaviour
{
    //429 to -429
    //[Tooltip("팬 틸트 줌 할때의 그 팬입니다.")]
    GameObject PanOBJ;
    Vector3 startPOS;
    bool isdrag = false;
    float EachFrameMag, MouseX;

    // Start is called before the first frame update
    void Start()
    {
        PanOBJ = this.gameObject;
    }

    /*
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isdrag = true;
            OnDragBegan();
        }else if (Input.GetMouseButtonUp(0))
        {
            isdrag = false;
        }

        if (isdrag)
        {
            OnDragMoved();
        }
    }
    
    private void OnMouseEnter()
    {
        startPOS = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        endPOS = Input.mousePosition;
        Vector3 TouchGap = endPOS - startPOS;
        TouchGap = new Vector3(TouchGap.x, 0, 0);
    }
    */
    public void OnDragBegan()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            startPOS = touch.position;
        }
    }

    public void OnDragMoved()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 PrePOS = touch.position - touch.deltaPosition;

            Vector3 CurGap = startPOS - (Vector3)touch.position;
            Vector3 PreGap = startPOS - PrePOS;

            EachFrameMag = Mathf.Abs((PreGap.magnitude - CurGap.magnitude) * 0.5f);

            Vector3 MoveGap = (PreGap - CurGap).normalized;
            MoveGap = new Vector3(MoveGap.x, 0, 0);
            MouseX = MoveGap.x;

            PanOBJ.transform.localPosition += MoveGap * EachFrameMag;
            //여기 이동에 clamp 걸어둬야함
        }
    }

    public void OnDragEnded()
    {
        print("속도 " + EachFrameMag);
        //print("마우스 " + MouseX);
        if(EachFrameMag > 45)
        {
            if(MouseX < 0)
            {
                while (PanOBJ.transform.localPosition.x > -429)
                {
                    PanOBJ.transform.localPosition = Vector3.MoveTowards(PanOBJ.transform.localPosition,
                        new Vector3(-429, PanOBJ.transform.localPosition.y, 0), 3f);
                    if (PanOBJ.transform.localPosition.x <= -429)
                        break;
                }
            }
            else
            {
                while (PanOBJ.transform.localPosition.x < 429)
                {
                    PanOBJ.transform.localPosition = Vector3.MoveTowards(PanOBJ.transform.localPosition,
                        new Vector3(429, PanOBJ.transform.localPosition.y, 0), 3f);
                    if (PanOBJ.transform.localPosition.x >= 429)
                        break;
                }
            }
        }
    }
}
