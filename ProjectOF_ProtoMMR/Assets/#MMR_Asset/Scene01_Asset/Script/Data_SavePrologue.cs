using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_SavePrologue : MonoBehaviour
{
    int SavePrologue = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SavePrologue"))
        {
            PlayerPrefs.SetInt("SavePrologue", SavePrologue); //프롤로그씬에 넣어서 해당씬을 봤음을 저장
        }
        else
        {
            SavePrologue = PlayerPrefs.GetInt("SavePrologue");
            SavePrologue++;
            PlayerPrefs.SetInt("SavePrologue", SavePrologue);
            PlayerPrefs.Save();
            print("" + SavePrologue);
        }
    }
}
