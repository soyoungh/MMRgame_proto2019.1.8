using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//0612~0613 정리후 pull request

/// <summary>
/// 특정 범위일경우 Grid의 각도를 회전시켜주는 클래스
/// #Grid회전 #카메라Grid회전 #카메라이동
/// </summary>
public class Image_SameDegreeCamMove : MonoBehaviour
{
    [Tooltip("SCRIPT OBJECT, get drag change value(FrameChangeForGridRot)")]
    public Ui_CamZoom ins_zoom;
    [Tooltip("VECTOR, grid가 회전될 각도입니다.")]
    public Vector3 gridROT;
    [Tooltip("FLOAT, 어느정도 줌 됐을때 grid가 회전될지를 정하는값")]
    public float Zoom_RealMin;
    [Tooltip("BOOL, Play_MouseGapMove에서 줌체크로 사용됨")]
    public bool isZoomin = false;

    public GameObject Cam;
    public GameObject Grid, GridForCam;
    // Update is called once per frame
    void Update()
    {
        if(Cam.GetComponent<Camera>().fieldOfView < Zoom_RealMin)//zoom in
        {
            float SpeedClamp = Mathf.Clamp(Mathf.Abs(ins_zoom.FrameChangeForGridRot), 40, 450);
            Grid.transform.eulerAngles = 
                Vector3.MoveTowards(Grid.transform.eulerAngles, gridROT, Time.deltaTime * SpeedClamp);
            //field of view를 그대로 쓰면 30~100까지의 범위를 갖고있는 값은 줌아웃을 할경우 더 빠를수밖에 없음 
            //변화된 양을 곱해줘야합니다.0612

            gameObject.transform.eulerAngles = Grid.transform.eulerAngles;
            GridForCam.transform.eulerAngles = Grid.transform.eulerAngles;
            isZoomin = true;
        }
        else//zoom out
        {
            isZoomin = false;
            float SpeedClamp = Mathf.Clamp(Mathf.Abs(ins_zoom.FrameChangeForGridRot), 40, 450);
            Grid.transform.eulerAngles = 
                Vector3.MoveTowards(Grid.transform.eulerAngles, Vector3.zero, Time.deltaTime * SpeedClamp);

            gameObject.transform.eulerAngles = Grid.transform.eulerAngles;
            GridForCam.transform.eulerAngles = Grid.transform.eulerAngles;
        }
        Cam.transform.position = transform.position;
    }
}