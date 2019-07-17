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
    public RectTransform CaptureMask;

    [Tooltip("GAME OBJECT, 캡쳐된 이미지가 들어갈 게임오브젝트")]
    public GameObject Photo;
    
    //public Vector3 DragCenter, DragCenterForRay;
    public bool canPicture = false;
    public bool canPicture_Screen = false;//0618
    public Vector3 DragCenterScreen;
    public Canvas CanvasCamera;
    //public Canvas AllUi;

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
        
        // * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~캡쳐사이즈 수정후~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
        Texture2D screenShot = new Texture2D((int)CaptureMask.sizeDelta.x,
                                              (int)CaptureMask.sizeDelta.y,
                                              TextureFormat.RGB24, false);
        //새로운 투디텍스쳐를 만든다. 사이즈는 드래그박스의 사이즈로 설정
        //사이즈 델타는 부모 오브젝트에 종속되었을때 부모에 비교해서의??사이즈값
        // * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *


        // * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *
        screenShot.ReadPixels(new Rect(Screen.width/2 - CaptureMask.sizeDelta.x/2, Screen.height/2 - CaptureMask.sizeDelta.y/2,
                                       CaptureMask.sizeDelta.x, CaptureMask.sizeDelta.y), 0, 0);
        Photo.GetComponent<RectTransform>().sizeDelta = CaptureMask.sizeDelta;
        screenShot.Apply();
        // * ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ *

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
            Photo.SetActive(true);
            ins_load.LoadA_Picture();
            ins_load.LoadA_Picture();
        }

        canPicture = false;

    }

    IEnumerator TakeA_Picture_Screen()
    {
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        yield return null;
        //AllUi.enabled = false;
        yield return new WaitForEndOfFrame();
        //Debug.Log("VF활성화 " + AllUi.enabled);

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
            //AllUi.enabled = true;
            Photo.SetActive(true);
            //Debug.Log("VF활성화 " + AllUi.enabled);//flickering
            ins_load.LoadA_Picture();
        }

        canPicture_Screen = false;
        RenderTexture.active = null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(CaptureMask.rect.position, CaptureMask.rect.size);
    }
}
