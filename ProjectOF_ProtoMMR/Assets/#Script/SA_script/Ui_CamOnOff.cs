using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_CamOnOff : MonoBehaviour
{
    public Image_SameDegreeCamMove ins_Degree;
    public Ui_CamZoom ins_camzoom;
    public bool Iscam = false;
    public GameObject ViewFinderUI;
    public delegate void DragModeDelegate();
    public static event DragModeDelegate OnDragMode;

    public void OnOff()
    {
        if (Input.GetMouseButtonUp(0) && ins_Degree.isZoomin)
        {
            if (Iscam)//캠 > 드래그
            {
                ViewFinderUI.SetActive(false);
                Iscam = false;
                OnDragMode();
                ins_camzoom.enabled = true;
            }
            else//드래그 > 캠
            {
                ViewFinderUI.SetActive(true);
                Iscam = true;
                ins_camzoom.enabled = false;
            }
        }
    }
}
//카메라 모드시에는 줌인아웃 비활성화