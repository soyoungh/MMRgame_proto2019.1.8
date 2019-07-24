using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_CamRoll : MonoBehaviour
{
    Text Text_CamRoll;
    int Num_CamRoll = 0;
    int Num_CamRollTotal = 5;
    // Start is called before the first frame update
    private void OnEnable()
    {
        Image_FindRightAnswer.RollCount += this.RollCounting;
    }
    private void OnDisable()
    {
        Image_FindRightAnswer.RollCount -= this.RollCounting;
    }

    private void Start()
    {
        Text_CamRoll = gameObject.GetComponent<Text>();
        Text_CamRoll.text = "X " + Num_CamRollTotal.ToString();
    }
    public void RollCounting()
    {
        Num_CamRoll++;
        int Num_CamRollLeft = Num_CamRollTotal - Num_CamRoll;
        Text_CamRoll.text = "X " + Num_CamRollLeft.ToString();
    }
}
