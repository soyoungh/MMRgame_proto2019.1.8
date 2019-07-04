using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 정답일경우 카메라를 그 위치로 이동시킴
/// #카메라이동확대
/// </summary>
public class RenderView_RenderCam : MonoBehaviour
{
    public GameObject MainCam;
    public Play_DrawDrag ins_drawdrag;

    Vector2 DefaulSize = new Vector2(500, 500);
    

    /// <summary>
    /// 정답일경우 해당위치로 카메라이동
    /// </summary>
    public void CorrectAnswerMove()//From AllController
    {
        transform.position = ins_drawdrag.DragBoxImage.transform.position + new Vector3(0,0,-10);
        GetComponent<Camera>().fieldOfView = MainCam.GetComponent<Camera>().fieldOfView;
        //해당위치이동 및 메인캠fov와 동기화
    }
}


/* 바로 위에 이부분 넣으면 드래그 모양대로 RenderView 활성화
LoadImageMask.sizeDelta = ins_drawdrag.DragBoxImage.sizeDelta;
//마스크를 드래그범위사이즈만큼 자름
float ProportionX = DefaulSize.x / LoadImageMask.sizeDelta.x;
float ProportionY = DefaulSize.y / LoadImageMask.sizeDelta.y;
LoadImageMask.localScale = new Vector3(ProportionX, ProportionY, 1);
//마스크를 줄어든만큼 확대, 프레임에 사이즈맞추기
*/
