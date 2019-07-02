using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_CameraMove : MonoBehaviour
{
    //notused

    [Tooltip("SCRIPT OBJECT, count touch input number")]
    public Ui_CamZoom ins_camzoom;

    [Tooltip("SCRIPT OBJECT, check if cam is clicked or not")]
    public Ui_CamOnOff ins_uicam;
    
    public GameObject CamDegree, grid;//수정중

    //[Tooltip("")]
    public Image_SameDegreeCamMove ins_rotGrid;
    bool isdrag, iszoomin = false;

    Vector3 startPoint, endPoint;
    Vector3 DeletStart, DeletEnd;

    /// <summary>
    /// this is not used, 지워야하나
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.touchCount < 2)
        {
            Vector3 tempMousePos = Input.mousePosition;
            DeletStart = tempMousePos;
            startPoint = Camera.main.ScreenToWorldPoint(new Vector3(tempMousePos.x, tempMousePos.y, -Camera.main.transform.position.z));//0
            //startPoint.z = 0;
            //startPoint.z = Camera.main.transform.localPosition.z;
            isdrag = true;
            print("//");
            //print("Start mousePOS : " + Input.mousePosition);
        }

        if (!ins_uicam.Iscam && ins_camzoom.IsTouch2 == false)
        {
            if(Input.touchCount < 2 && isdrag)
            {
                Vector3 tempMousePos = Input.mousePosition;
                DeletEnd = tempMousePos;
                endPoint = Camera.main.ScreenToWorldPoint(new Vector3(tempMousePos.x, tempMousePos.y, -Camera.main.transform.position.z));//1
                //endPoint.z = 0;
                //endPoint.z = Camera.main.transform.localPosition.z;
                //z축은 움직이지 않을거라 고정

                //print("End mousePOS : " + Input.mousePosition);

                //print("Start point : " + startPoint + " // End point : " + endPoint);
                //print("스크린의 픽셀 : " + Camera.main.scaledPixelWidth + ", " + Camera.main.scaledPixelHeight);
                if (ins_rotGrid.isZoomin)//확대했을때
                {
                    var temp = (endPoint - startPoint);//temp.z = 0;//z축때문인게 ㅈㄴ확실
                    CamDegree.transform.Translate(-temp, Space.Self);
                    /*
                    */

                    /*
                    Revise_0520
                    var Screen_temp = DeletEnd - DeletStart;
                    var World_temp = Camera.main.ScreenToWorldPoint(new Vector3(Screen_temp.x, Screen_temp.y, -CamDegree.transform.position.z));
                    CamDegree.transform.Translate(-World_temp, Space.Self);
                    print("s:" + Screen_temp + ", w:" + World_temp);
                    :
                    스크린에서 월드로 값바뀌는게 미리바꾸면 월드에서는 한포인트만을 받기 때문에
                    (월드축에서는 손가락의 월드위치가 바뀌지않고 카메라가 움직이는거)
                    end - start가 값이 잘 안바뀌는게 문제라고 생각했음
                    그래서 아예 스크린값에서 end-start를 하고 그걸 월드로 바꿨는데 1도안됨ㅡㅡㅗ
                    */

                    iszoomin = true;
                    //print("(cam zoom in)end-start is : " + temp);//2
                    //이 temp의 값자체가 위로 올라가면 적게나옴
                    //아 이거 오류 z축달라져서 그런거같은데?
                    //https://forum.unity.com/threads/solved-moving-object-to-point-object-moves-slower-when-point-is-close-than-farther-away.269802/
                    //확대후 로컬축 기준으로 이동
                    //이부분 scene settingAll에서 오류날거니까 이따 수정하던지 새로스크립트를 만들던지 하셈
                }
                else//확대하지않을때
                {
                    CamDegree.transform.localPosition = Camera.main.transform.position - (endPoint - startPoint);
                    print("(cam zoom out)end-start is : " + (endPoint - startPoint));
                }
            }
        }

        if (!isdrag && iszoomin)//after zoom in
        {
            if (!ins_rotGrid.isZoomin)//then zoon out
            {
                //print("zoom out");
                CamDegree.transform.localPosition = Vector3.MoveTowards(CamDegree.transform.position,
                    new Vector3(CamDegree.transform.position.x, CamDegree.transform.position.y, -10), Time.deltaTime * 2f);
                if (CamDegree.transform.localPosition.z == -10)
                {
                    //print("z축고정");
                    iszoomin = false;
                }
            }
            //iszoomout = true;
        }
        //print("줌인했는지: " + iszoomin);
    }
    Vector3 transformToLocal(Vector3 worldValue)
    {
        Vector3 value = CamDegree.transform.InverseTransformDirection(worldValue);
        return value;
    }
}