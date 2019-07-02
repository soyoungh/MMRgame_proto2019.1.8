using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 랜더뷰의 ui관리
/// #돌아가기 #
/// </summary>
public class RenderView_UIcontrol : MonoBehaviour
{
    public Image_FindRightAnswer ins_findRight;
    public Ui_CamOnOff ins_onoff;
    public RenderView_PreventDrag ins_prevent;
    public GameObject RenderView;
    public Play_DrawDrag ins_drag;

    /// <summary>
    /// 돌아가기
    /// #드래그이동모드 #숨은그림false #이동,확대활성화 #렌더뷰비활성화
    /// </summary>
    public void OnReturn()
    {
        ins_onoff.Iscam = false;
        ins_prevent.EnableDragZoom();
        print("Return to drag move mode.");
        RenderView.SetActive(false);
        ins_drag.isClicked_DrawingBox = false;
    }
}
