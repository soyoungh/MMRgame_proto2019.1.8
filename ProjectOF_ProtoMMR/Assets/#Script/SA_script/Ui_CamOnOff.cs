using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui_CamOnOff : MonoBehaviour
{
    public bool Iscam = false;
    public GameObject ViewFinderUI;
    public delegate void DragModeDelegate();
    public static event DragModeDelegate OnDragMode;

    public void OnOff()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (Iscam)
            {
                ViewFinderUI.SetActive(false);
                Iscam = false;
                OnDragMode();
            }
            else
            {
                ViewFinderUI.SetActive(true);
                Iscam = true;
            }
        }
    }
}
