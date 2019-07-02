using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// 드래그 범위의 이미지를 저장하는 클래스
/// #이미지캡쳐 #저장 #박스의시작점(좌측하단)구하기 #해상도고정
/// </summary>
public class Picture_Save : MonoBehaviour
{
    //test
    [Tooltip("SCRIPT OBJECT, script for execute captured image load function")]
    public Picture_Load ins_load;

    [Tooltip("SCRIPT OBJECT, script for bring VECTOR to use 드래그 좌표 구하기")]
    public Play_DrawDrag ins_drag;

    [Tooltip("UI_RECTTRANSFORM, this transform's size will be captured image's size")]
    public RectTransform DragBoxImage;

    [Tooltip("GAME OBJECT, 캡쳐된 이미지가 들어갈 게임오브젝트")]
    public GameObject Photo;
    
    //public Vector3 DragCenter, DragCenterForRay;
    public bool canPicture = false;
    public bool canPicture_Screen = false;//0618
    public Vector3 DragCenterScreen;
    public Canvas AllUi;

    //0619 정답사진 이미지 변환

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(1080, 1920, true);
        //화면 사이즈랑 레졸룸을 확실이 해주고 그거에맞게 캔버스를 수정해야함
        //canvas scaler
    }
    public void StartPicture()
    {
        canPicture = true;//여기를 아예 막으면 될거같은뎅
    }

    public void StartPicture_Screen()
    {
        canPicture_Screen = true;
    }
    private void OnPostRender()
    {
        if (canPicture)
            StartCoroutine("TakeA_Picture");
        else if (canPicture_Screen)
            StartCoroutine("TakeA_Picture_Screen");
        //매번읽는 함수인건가?
    }

    IEnumerator TakeA_Picture()
    {
        yield return new WaitForEndOfFrame();
        //미리 너무 빠르게읽어버려서 빈정보를 읽어올까봐 한프레임 대기

        Texture2D screenShot = new Texture2D((int)DragBoxImage.sizeDelta.x,
                                              (int)DragBoxImage.sizeDelta.y,
                                              TextureFormat.RGB24, false);
        //새로운 투디텍스쳐를 만든다. 사이즈는 드래그박스의 사이즈로 설정
        //사이즈 델타는 부모 오브젝트에 종속되었을때 부모에 비교해서의??사이즈값



        // * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
        // *
        // * [ 드래그 방향에 관계없이 항산 드래그 박스의 좌측하단의 좌표값 구하기 ] 
        Vector2 dragLine = new Vector2(Mathf.Abs(ins_drag.lastPoint.x - ins_drag.firstPoint.x),
                                     Mathf.Abs(ins_drag.lastPoint.y - ins_drag.firstPoint.y));

        //시작점과 끝점의 x와y의 길이값, 사실 변수명이 드래그 라인이아니라 드래그 박스의 width,height의미여야함
        Vector2 center = new Vector2((ins_drag.lastPoint.x + ins_drag.firstPoint.x) / 2,
                                     (ins_drag.lastPoint.y + ins_drag.firstPoint.y) / 2);

        DragCenterScreen = (Vector3)center + (Vector3.forward * -10);//using from viewfinder autozoom
        //DragCenterForRay = center;//Picture스크립트에서 쓰임 (주석처리0618)계속문제없다면 지우기

        //시작점과 끝점의 중간지점 저장
        Vector2 alwaysStart = center - (dragLine / 2);
        //중간 지점에서 가로길이와 세로길이의 절반을 빼서 항상 좌측하단의 좌표값을 시작점으로 함
        // *
        // * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *



        screenShot.ReadPixels(new Rect(alwaysStart.x, alwaysStart.y,
                                       screenShot.width, screenShot.height), 0, 0);

        //마우스 위치(스크린기준 좌표)와 텍스쳐투디(스크린기준 좌표)의 가로세로
        //여기에 최대사이즈를 피하고, 최소사이즈를 정하여 피하면오류 ㄴㄴ일듯(attempting RT bound out)
        // read pixel로 부터 사이즈를 구해야한다_0605
        
        screenShot.Apply();

        //print("캔버스기준 드래그박스 사이즈 : " + screenShot.width + ", " + screenShot.height);//?

        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            byte[] bytes = screenShot.EncodeToPNG();
            System.IO.File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "capture.png"), bytes);
            Photo.SetActive(true);//이미지 활성화
            ins_load.LoadA_Picture();//이미지 로드
        }
        else
        {
            byte[] bytes = screenShot.EncodeToPNG();
            System.IO.File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "capture.png"), bytes);
            Photo.SetActive(true);
            ins_load.LoadA_Picture();
            ins_load.LoadA_Picture();
        }

        canPicture = false;
        //RenderTexture.active = null;

    }

    IEnumerator TakeA_Picture_Screen()
    {
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        yield return null;
        AllUi.enabled = false;
        yield return new WaitForEndOfFrame();
        Debug.Log("VF활성화 " + AllUi.enabled);

        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);
        screenShot.Apply();

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            byte[] bytes = screenShot.EncodeToPNG();
            System.IO.File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "capture.png"), bytes);
            Photo.SetActive(true);//이미지 활성화
            ins_load.LoadA_Picture();//이미지 로드
        }
        else
        {
            byte[] bytes = screenShot.EncodeToPNG();
            System.IO.File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "capture.png"), bytes);
            AllUi.enabled = true;
            Photo.SetActive(true);
            Debug.Log("VF활성화 " + AllUi.enabled);//flickering
            ins_load.LoadA_Picture();
        }

        canPicture_Screen = false;
        RenderTexture.active = null;
    }
}
