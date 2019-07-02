using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카메라 줌인아웃하는 클래스
/// #줌인아웃 #터치인풋2개체크 #드래그변화량(축회전,줌속도)
/// </summary>
public class Ui_CamZoom : MonoBehaviour
{
    [SerializeField]
    float minFOV_real, minFOV, maxFOV_real, maxFOV;
    public Camera cam;

    [Tooltip("BOOL, 줌이 끝날때까지 drag move가(touchcount == 1) 실행하는것을 방지")]
    public bool IsTouch2 = false;
    [Tooltip("FLOAT, grid 축회전이랑 줌속도 맞춰줄 드래그 변화량")]
    public float FrameChangeForGridRot;

    public Play_DrawDrag ins_drawdrag;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2 && !ins_drawdrag.isClicked_DrawingBox)
            ZoomStart();
        else if (Input.touchCount == 0)
            ZoomEnd();
    }

    /// <summary>
    /// 두손가락이 들어와서 드래그할때,
    /// 두손가락으로 드래그하는 양으로 줌한다
    /// </summary>
    void ZoomStart()
    {
        IsTouch2 = true;
        Touch touch0 = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);
        
        Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
        Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

        float prevMag = (touch0PrevPos - touch1PrevPos).magnitude;
        float currMag = (touch0.position - touch1.position).magnitude;

        float everyFrameChange = prevMag - currMag;
        FrameChangeForGridRot = everyFrameChange;//0612
        if (cam.orthographic)
        {
            cam.orthographicSize += everyFrameChange * 0.1f;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minFOV, maxFOV);
        }
        else
        {
            cam.fieldOfView += everyFrameChange * 0.1f;
            cam.fieldOfView = Mathf.Max(cam.fieldOfView, minFOV);
            cam.fieldOfView = Mathf.Min(cam.fieldOfView, maxFOV);
        }
    }

    /// <summary>
    /// 터치 인풋이 모두 빠졌을때,
    /// 드래그가 끝나고 화면이 지정범위를 벗어날경우 화면을 댐핑함
    /// </summary>
    void ZoomEnd()
    {
        IsTouch2 = false;
        if (cam.orthographic)
        {
            if (cam.orthographicSize < minFOV_real)
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, minFOV_real, 0.1f);
            else if (cam.orthographicSize > minFOV_real)
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, maxFOV_real, 0.1f);
        }
        else
        {
            if (cam.fieldOfView < minFOV_real)
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, minFOV_real, 0.1f);
            else if (cam.fieldOfView > maxFOV_real)
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, maxFOV_real, 0.1f);
        }
    }
}
