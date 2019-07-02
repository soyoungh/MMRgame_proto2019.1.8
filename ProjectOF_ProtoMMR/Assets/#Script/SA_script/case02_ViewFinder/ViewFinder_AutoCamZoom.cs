using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 드래그가 놓여지면 해당부분의 중심위치 기준으로 지정한거리만큼 카메라가 zoom in됩니다.
/// </summary>
public class ViewFinder_AutoCamZoom : MonoBehaviour
{
    public Camera MainCam;
    public Play_DrawDrag ins_drawdrag;
    public GameObject ViewFinder;
    public GameObject DrawDrag;
    public GameObject CamAnchor;
    public GameObject CamOnOff;

    int DragPointFOV_to = 35;
    float DragPointFOV_from;
    public bool isAutoZooming = false;
    public Vector3 ViewFinderCamPos;
    //bool isZoomedMove = false; //이거 확대후 움직이는거 컨트롤

    // Update is called once per frame
    void Update()
    {
        //뷰파인더 실행하기 여기서해야할까
    }

    /// <summary>
    /// 뷰파인더 실행, ZOOM IN
    /// 정해진시간내에 줌인이랑 이동되는거얌
    /// (Play_DrawDrag)에서 실행시킴 / 수정해야할까?
    /// </summary>
    public IEnumerator CameraAutoMove_IN()
    {
        if (!isAutoZooming)//이거 또실행되는거 막아야댕댕이 & 드래그도 마가야댕 false해버리까
        {
            isAutoZooming = true;
            DrawDrag.SetActive(false);
            CamOnOff.SetActive(false);
            ViewFinder.SetActive(true);

            float ElapsedTime = 0;
            DragPointFOV_from = MainCam.fieldOfView;

            Vector3 DragPointPosition_from = CamAnchor.transform.localPosition;
            Vector3 DragPointPosition_to = ins_drawdrag.DragBoxImage.transform.position;
            //print("기존위치" + DragPointPosition_from + " / 목표위치" + DragPointPosition_to);


            while (ElapsedTime < 0.3f)
            {
                float temp = ElapsedTime / 0.3f;

                MainCam.fieldOfView =
                    Mathf.Lerp(DragPointFOV_from, DragPointFOV_to, temp);
                CamAnchor.transform.localPosition =
                    Vector3.Lerp(DragPointPosition_from, DragPointPosition_to, temp);

                ElapsedTime += Time.deltaTime;
                //print("러프실수 : " + temp + " | 경과시간 : " + ElapsedTime);

                yield return new WaitForEndOfFrame();
            }
            ViewFinderCamPos = CamAnchor.transform.localPosition;
            MainCam.fieldOfView = DragPointFOV_to;
        }
    }

    /// <summary>
    /// 뷰파인더 해제, ZOOM OUT
    /// (ViewFinder_UIcontrol)에서 실행시킴
    /// </summary>
    public IEnumerator CameraAutoMove_OUT()
    {
        isAutoZooming = false;
        DrawDrag.SetActive(true);
        CamOnOff.SetActive(true);
        ViewFinder.SetActive(false);

        float ElapsedTime = 0;

        while (ElapsedTime < 0.3f)
        {
            float temp = ElapsedTime / 0.3f;

            MainCam.fieldOfView =
                Mathf.Lerp(DragPointFOV_to, DragPointFOV_from, temp);

            ElapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }
        MainCam.fieldOfView = DragPointFOV_from;
    }
}
