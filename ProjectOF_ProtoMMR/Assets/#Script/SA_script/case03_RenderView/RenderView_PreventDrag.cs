using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이동,확대 방지
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
