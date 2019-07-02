using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이동,확대 방지
/// </summary>
public class RenderView_PreventDrag : MonoBehaviour
{
    public GameObject DragControl;
    public GameObject ZoomControl;

    public void DisableDragZoom()
    {
        print("??");
        DragControl.SetActive(false);
        ZoomControl.SetActive(false);
    }

    public void EnableDragZoom()
    {
        DragControl.SetActive(true);
        ZoomControl.SetActive(true);
    }
}
