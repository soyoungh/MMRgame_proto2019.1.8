using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data_PrologueReset : MonoBehaviour
{
    public void ResetPrologue()
    {
        PlayerPrefs.DeleteKey("SavePrologue");
        print("SavePrologue의 키값과 데이터를 지웠습니다.");
        PlayerPrefs.DeleteKey("SaveTutorial");
        print("SaveTutorial의 키값과 데이터를 지웠습니다.");
    }
}
