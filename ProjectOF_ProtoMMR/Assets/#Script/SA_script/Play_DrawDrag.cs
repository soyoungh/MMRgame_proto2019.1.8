using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 드래그 박스를 그리는 클래스
/// #캔버스기준 #박스그리기 #마우스좌표변환
/// </summary>

delegate void DelegateDrawDrag();

public class Play_DrawDrag : MonoBehaviour
{
    //[Tooltip("SCRIPT OBJECT, 캡쳐이미지 저장함수 실행용")]
    //public Picture_Save ins_save;
    [Tooltip("UI_RECTTRANSFORM, 클릭앤 드래그할때 드래그박스 그림")]
    public RectTransform DragBoxImage;
    [Tooltip("CANVAS, 여기에 넣는 캔버스의 Local스페이스를 기준으로 recttransform(좌표위치)을 만듦")]
    public Canvas myCanvas;
    [Tooltip("VECTOR, this is for script(Picture_Save) and 드래그 방향에 상관없는 좌표값을 구하기위해 사용됨")]
    public Vector2 firstPoint, lastPoint;


    public GameObject DragRangeWarning;
    public bool isClicked_DrawingBox = false;//using from camzoom
    Vector3 MovingCenter; // 원래는 public이었음 문제있음 바꾸셈0703
    Vector3 startPos, endPos;
    Vector2 startCanvas, endCanvas;

    private void OnEnable()
    {
        Play_CheckTouch.DragBoxStart_FromDrag += this.DragBoxStart;
        Play_CheckTouch.DragBoxEnd_FromDrag += this.DragBoxEnd;
        Ui_CamOnOff.OnDragMode += this.ReturnToDragMode;
    }
    private void OnDisable()
    {
        Play_CheckTouch.DragBoxStart_FromDrag -= this.DragBoxStart;
        Play_CheckTouch.DragBoxEnd_FromDrag -= this.DragBoxEnd;
        Ui_CamOnOff.OnDragMode -= this.ReturnToDragMode;
    }

    // Update is called once per frame
    void Update()
    {
        MovingCenter = Camera.main.WorldToScreenPoint(myCanvas.transform.position);//카메라의 중심
        if (isClicked_DrawingBox)
            DragBoxOnDraging();
        //isclicked로 안하고 델리게이트에서 처리하면 드래그 박스 깜빡이는 현상 있을유
        //얘는 본인 스크립트에서 실행하는거니까 그냥냅두쟈
    }


    void ReturnToDragMode()//cam버튼 눌러서 이동모드 전환시 실행
    {
        isClicked_DrawingBox = false;
    }

    public void DragBoxStart()
    {if (!enabled) return;

        DragBoxImage.gameObject.SetActive(true);

        Vector3 TempMouse;
        TempMouse.x = Mathf.Clamp(Play_CheckTouch.touch.position.x, MovingCenter.x - 530, MovingCenter.x + 540);//캔버스내에서 벗어나지 못하게막음
        TempMouse.y = Mathf.Clamp(Play_CheckTouch.touch.position.y, MovingCenter.y - 959, MovingCenter.y + 960);
        TempMouse.z = 0;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                                    myCanvas.transform as RectTransform, TempMouse,
                                    myCanvas.worldCamera, out startCanvas);
        
        startPos = startCanvas;
        firstPoint = Play_CheckTouch.touch.position;
        isClicked_DrawingBox = true;
    }

    public void DragBoxEnd()
    {if (!enabled) return;
    
        if (isClicked_DrawingBox)//버튼클릭과 동시에 드래그실행됨을 방지
        {
            DragBoxImage.gameObject.SetActive(false);
            lastPoint = Play_CheckTouch.touch.position;

            isClicked_DrawingBox = false;
        }
    }

    public void DragBoxOnDraging()
    {if (!enabled) return;

        Vector3 TempMouse;
        TempMouse.x = Mathf.Clamp(Play_CheckTouch.touch.position.x, MovingCenter.x - 530, MovingCenter.x + 540);
        TempMouse.y = Mathf.Clamp(Play_CheckTouch.touch.position.y, MovingCenter.y - 959, MovingCenter.y + 960);
        TempMouse.z = 0;
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                                    myCanvas.transform as RectTransform, TempMouse,
                                    myCanvas.worldCamera, out endCanvas);
        endPos = endCanvas;
        DragBoxImage.position = myCanvas.transform.TransformPoint((startPos + endPos) / 2);
        //드래그 박스의 위치는 월드기준으로 드래그 첫점과 끝점사이의 중점(드래그 중심)

        float sizeX = Mathf.Abs(startPos.x - endPos.x);
        float sizeY = Mathf.Abs(startPos.y - endPos.y);
        DragBoxImage.sizeDelta = new Vector2(sizeX, sizeY);
        //드래그 박스의 사이즈x,y는 드래그 된 영역만큼의 사이즈

    }
}