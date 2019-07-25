using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_CheckScene : MonoBehaviour
{
    public List<GameObject> Unrock = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SaveSceneNum"))//Tutorial 진행중
        {

        }
        else//Tutorial 종료
        {

        }
    }
}
