using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 랜더뷰에서 이동 모드로 돌아가기
/// 
/// #iscam을false로 
/// #이동,줌활성화 
/// #랜더뷰이미지비활성화 
/// #드래그중확인false로
/// </summary>
public class RenderView_UIcontrol : MonoBehaviour
{
    public Ui_CamOnOff ins_onoff;
    public RenderView_PreventDrag ins_prevent;
    public Play_DrawDrag ins_drag;
    public GameObject RenderView, ViewFinder;
    public RectTransform LoadImageAnchor, LoadImage;

    /// <summary>
    /// 돌아가기
    /// #드래그이동모드 #숨은그림false #이동,확대활성화 #렌더뷰비활성화
    /// </summary>
    public void OnReturn()//From Button
    {
        ViewFinder.SetActive(false);
        ins_onoff.Iscam = false;
        ins_prevent.EnableDragZoom();
        RenderView.SetActive(false);
        ins_drag.isClicked_DrawingBox = false;
        LoadImageAnchor.localScale = Vector3.one;
        LoadImage.position = Vector3.zero;
        
    }
}