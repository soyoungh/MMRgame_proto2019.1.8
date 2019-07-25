using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_SaveScene : MonoBehaviour
{
    public int CurrentSceneNum;
    public static int ActiveSceneNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SaveSceneNum"))
        {
            PlayerPrefs.SetInt("SaveSceneNum", ActiveSceneNum);
            print("SaveSceneNum가 데이터" + ActiveSceneNum + "값으로 생성되었습니다.");
        }
        else if(PlayerPrefs.GetInt("SaveSceneNum") < CurrentSceneNum)
        {
            ActiveSceneNum = PlayerPrefs.GetInt("SaveSceneNum");
            ActiveSceneNum++;
            PlayerPrefs.SetInt("SaveSceneNum", ActiveSceneNum);
            PlayerPrefs.Save();
            print("SaveSceneNum가 데이터" + ActiveSceneNum + "값으로 저장되었습니다.");
        }
    }

    public void ResetValue()
    {
        PlayerPrefs.DeleteKey("SaveSceneNum");
        print("SaveSceneNum의 키값과 데이터를 지웠습니다.");
    }
}
