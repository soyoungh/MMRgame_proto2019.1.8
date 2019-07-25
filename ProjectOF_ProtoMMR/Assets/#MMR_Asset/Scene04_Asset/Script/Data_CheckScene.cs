using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Data_CheckScene : MonoBehaviour
{
    public Sprite Image_Unrock;
    public List<GameObject> Unrock = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SaveSceneNum"))//Tutorial 진행중
        {
            Unrock[0].GetComponent<Image>().sprite = Image_Unrock;
        }
        else//Tutorial 종료
        {
            for (int i = 0; i < PlayerPrefs.GetInt("SaveSceneNum") + 1; i++)
            {
                Unrock[i].GetComponent<Image>().sprite = Image_Unrock;
            }
        }
    }
}
