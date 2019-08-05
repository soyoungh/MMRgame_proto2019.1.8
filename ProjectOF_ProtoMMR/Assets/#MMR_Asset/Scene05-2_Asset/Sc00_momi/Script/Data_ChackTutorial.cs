using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_ChackTutorial : MonoBehaviour
{
    public GameObject Tuto;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("SaveTutorial"))
        {
            Tuto.GetComponent<Tutorial_control>().enabled = false;
            Tuto.SetActive(false);
        }
    }
}
