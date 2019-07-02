using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using UnityEngine;

/// <summary>
/// 저장된 이미지데이터를 불러오는 클래스
/// #ReadData #스프라이트생성
/// </summary>
public class Picture_Load : MonoBehaviour
{
    Texture2D CustomTexture;//0618
    // Start is called before the first frame update
    private void Awake()
    {
        CustomTexture = new Texture2D(
            (int)gameObject.GetComponent<RectTransform>().rect.width,
            (int)gameObject.GetComponent<RectTransform>().rect.height);
        //원래 loada_picture내용이 여기있었음
    }
    public void LoadA_Picture()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)//기기가 모바일인 경우
        {
            Texture2D LoadTextureA = OnAndroid(Path.Combine(Application.persistentDataPath, "capture.png"));
            gameObject.GetComponent<Image>().sprite =
                Sprite.Create(LoadTextureA, new Rect(0, 0, LoadTextureA.width, LoadTextureA.height), gameObject.transform.position);
        }
        else//그외의 기기
        {
            Texture2D LoadTextureP = OnPc(Path.Combine(Application.persistentDataPath, "capture.png"));
            //1. 텍스쳐 변수를 만들어서 onpc라는 함수를 실행시키며 ""안의 주소를 보냄
            //8. 반환된 텍스쳐 변수를 저장시킴
            gameObject.GetComponent<Image>().sprite =
                Sprite.Create(LoadTextureP, new Rect(0, 0, LoadTextureP.width, LoadTextureP.height), gameObject.transform.position);
            //9. 저장된 텍스쳐를 LoadTextureP.width, LoadTextureP.height의 사이즈로 스프라이로 변환시켜 게임오브젝트 이미지 컴포넌트에 집어넣음
            //(텍스쳐를 스프라이트로 변환하여 이미지로 게임뷰에 띄움)
        }
    }

    Texture2D OnAndroid(string andPath)
    {
        Texture2D texM = null;
        if (File.Exists(andPath))
        {
            byte[] andfileDATA = File.ReadAllBytes(andPath);
            texM = CustomTexture;//new Texture2D(500, 500);
            texM.LoadImage(andfileDATA);
            return texM;
        }
        return null;
    }
    
    Texture2D OnPc(string pcPath)//2. 저장된 주소를 받음
    {
        Texture2D texP;
        //3. 텍스쳐 변수를 만듦(불러올 이미지를 넣을 그릇)
        if (File.Exists(pcPath))//4. 받아온 주소에 파일이 존재한다면
        {
            byte[] pcfileDATA = File.ReadAllBytes(pcPath);
            //5. 해당 주소의 바이트 정보를 읽어들임(파일을 읽어 byte변수에 저장)
            texP = CustomTexture;// new Texture2D(500, 500);
            //6. 텍스쳐 변수의 사이즈 지정
            texP.LoadImage(pcfileDATA);
            //7. 텍스쳐 변수에 바이트 정보값을 이미지로 로드시킴(바이트에서 이미지로 저장)
            return texP;
            //8. 이미지가 저장된 텍스쳐 변수를 반환
        }
        return null;
    }
}
