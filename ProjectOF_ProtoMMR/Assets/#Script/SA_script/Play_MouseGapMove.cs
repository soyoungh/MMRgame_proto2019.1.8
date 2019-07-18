using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//리펙토링 끝나면 주석처리하기
/// <summary>
/// 화면을 드래그하여 마우스의 위치 차이를 이용하여 오브젝트를 움직이는 함수입니다.
/// #캠zoomIN이동 #캠zoomOUT이동 #이동거리제한 #z축고정 
/// </summary>

    //이동거리 밖으로 빼놓기
//delegate void DelegateMove();
public class Play_MouseGapMove : MonoBehaviour
{
    [Tooltip("SCRIPT OBJECT, check if camera is zoomed or not")]
    public Image_SameDegreeCamMove ins_GridRotation;
    [Tooltip ("GAME OBJECT, CamDegree is fake anchor of camera")]
    public GameObject CamAnchor;

    public bool iszoom = false;
    public float CamMoveSpeed;
    float CamMoveClamp_ZoomOut = 15.05f;

    Vector3 StartTouch, StartMouse;
    Vector3 StartTouch_world;
    

    private void OnEnable()
    {
        Play_CheckTouch.OnTouchBegan_FromGapMove += this.OnTouchBegan;
        Play_CheckTouch.OnTouchMoved_FromGapMove += this.OnTouchMoved;
    }
    private void OnDisable()//이게 없을경우 이전에 등록된 이벤트?를 계속찾음 
    {
        Play_CheckTouch.OnTouchBegan_FromGapMove -= this.OnTouchBegan;
        Play_CheckTouch.OnTouchMoved_FromGapMove -= this.OnTouchMoved;
    }


    public void OnTouchMoved()
    {
        if (ins_GridRotation.isZoomin)
            OnTouchMoved_ZoomOn();
        else
            OnTouchMoved_ZoomOff();

        CamAnchor.transform.localPosition = new Vector3(
            Mathf.Clamp(CamAnchor.transform.localPosition.x, -CamMoveClamp_ZoomOut, CamMoveClamp_ZoomOut),
            Mathf.Clamp(CamAnchor.transform.localPosition.y, -CamMoveClamp_ZoomOut, CamMoveClamp_ZoomOut),
            CamAnchor.transform.localPosition.z);
    }

    /// <summary>
    /// 터치가 들어갈때(한번) 실행되는 함수
    /// </summary>
    public void OnTouchBegan()
    {
        StartTouch = Play_CheckTouch.touch.position;
        StartMouse = Input.mousePosition;

        StartTouch_world = Camera.main.ScreenToWorldPoint(StartTouch
            + Vector3.forward * -Camera.main.transform.position.z);
        //start좌표를 저장하고 월드좌표로 바꿔주는 타이밍도 중요!
    }


    /// <summary>
    /// 드래그&줌인 됐을때 실행되는 함수
    /// </summary>
    public void OnTouchMoved_ZoomOn()
    {
        iszoom = true;
        Vector3 PreviousPos = Play_CheckTouch.touch.position - Play_CheckTouch.touch.deltaPosition;

        Vector3 CurrentGap = StartTouch - (Vector3)Play_CheckTouch.touch.position;
        Vector3 PreviousGap = StartTouch - PreviousPos;

        float EachFrameMag = Mathf.Abs((PreviousGap.magnitude - CurrentGap.magnitude) * 0.1f);//0.02f
        //속도(지금은 0.02f가 젤 적절한데 씬마다 다른지 확인해야함)랑 방향 수정
        Vector3 EachFrameDiff = -(PreviousGap - CurrentGap).normalized * 0.1f * EachFrameMag;
        CamAnchor.transform.Translate(EachFrameDiff);

    }

    /// <summary>
    /// 드래그&줌아웃 됐을때 실행되는 함수
    /// </summary>
    public void OnTouchMoved_ZoomOff()
    {
        //iszoom = false;
        Vector3 EndTouch_world = Camera.main.ScreenToWorldPoint((Vector3)Play_CheckTouch.touch.position
            + Vector3.forward * -Camera.main.transform.position.z);

        Vector3 TouchGap_WorldTouch = EndTouch_world - StartTouch_world;
        CamAnchor.transform.localPosition -= TouchGap_WorldTouch;
        

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        CamAnchor.transform.localPosition = Vector3.MoveTowards(CamAnchor.transform.localPosition,
            new Vector3(CamAnchor.transform.localPosition.x, CamAnchor.transform.localPosition.y, 0), 0.02f);
        //z축 0으로 고정 (엄청 크게는 상관없는데 값변하는게 신경쓰여서 추가함)0617
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    }
}

