using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_SaveTutorial : MonoBehaviour
{
    int SaveTutorial = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SaveTutorial"))
        {
            PlayerPrefs.SetInt("SaveTutorial", SaveTutorial);
        }
    }

}
