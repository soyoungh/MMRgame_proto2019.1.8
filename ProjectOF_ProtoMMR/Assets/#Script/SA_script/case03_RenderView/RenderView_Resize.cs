using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderView_Resize : MonoBehaviour
{
    public RectTransform DragBox;

    public void Resizing()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = DragBox.sizeDelta;
    }
}
