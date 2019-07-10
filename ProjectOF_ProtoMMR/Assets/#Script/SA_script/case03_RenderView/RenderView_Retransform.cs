using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RenderView_Retransform : MonoBehaviour
{
    public RectTransform DragBox;

    public void MaskResizing()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = DragBox.sizeDelta;
        MaskReplacing();
    }

    public void MaskReplacing()
    {
        RectTransform FirstChild = gameObject.transform.GetChild(0).gameObject.GetComponent<RectTransform>();
        FirstChild.localPosition = new Vector3(-DragBox.localPosition.x, -DragBox.localPosition.y, DragBox.localPosition.z);
        //gameObject.transform.parent.gameObject.GetComponent<RectTransform>().localScale = new Vector3(2, 2, 1);
    }
}
