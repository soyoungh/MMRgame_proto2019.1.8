using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이동,확대 방지
/// 이게 촬영모드에서 전부 사용돼서 필요없다면 지우거나 촬영모드용으로 수정해서 사용하거나 하기
/// </summary>
public class RenderView_PreventDrag : MonoBehaviour
{
    public Play_DrawDrag DragControl;
    public Ui_CamZoom ZoomControl;

    public void DisableDragZoom()
    {
        DragControl.enabled = false;
        ZoomControl.enabled = false;
    }

    public void EnableDragZoom()
    {
        DragControl.enabled = true;
        ZoomControl.enabled = true;
    }
}
