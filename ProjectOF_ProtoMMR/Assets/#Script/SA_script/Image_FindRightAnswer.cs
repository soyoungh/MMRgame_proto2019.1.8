using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

/// <summary>
/// 숨은그림을 구별하여 찾는 클래스
/// #이미지의rect생성 #이미지모서리찾기 #드래그박스모서리찾기 #기즈모그리기 #드래그최소범위구분
/// 드래그영역내에 오답이 3개 이상일경우 그중 2개지워버리기
/// </summary>
public class Image_FindRightAnswer : MonoBehaviour
{
    public delegate void RenderViewDelegate();
    public static event RenderViewDelegate RightAnswer;//RenderView_AllController

    public delegate void FadeDelegate(SkeletonAnimation BeforeSprite, float FirstWait);
    public static event FadeDelegate FadeOutEvent;
    public delegate void FadeDelegate_sprite(SpriteRenderer BeforeSprite, float FirstWait);
    public static event FadeDelegate_sprite FadeOutEvent_sprite;
    //페이드 부분 관련 델리게이트, 이벤트


    public SpriteRenderer FindRange;
    public GameObject RenderView;
    public RectTransform DragBox, CheckRange;
    //public Vector3 ImagePosition;
    public float SizeRange_min, SizeRange_max;

    public GameObject HideSpriteRender;
    Rect HideSpriteRect;
    Rect DragRect;
    bool IsSizeFit = false;

    Vector3[] DragCornersVector = new Vector3[4];
    Vector3[] ImageCornersVector = new Vector3[4];
    Vector3 world_DragBoxPos;//드래그박스의 센터

    // Start is called before the first frame update
    void Start()
    {
    }


    private void OnEnable()
    {
        Play_CheckTouch.TouchMoved_FromAnswer += this.ImageAndDragboxCorners;
        Play_CheckTouch.TouchMoved_FromAnswer += this.DragMoveSizeCheck;
        Play_CheckTouch.TouchEnd_FromAnswer += this.DragEndFigureOut;
    }

    public void Answer_Wrong0_GetList()
    {
        Collider2D[] Overlaped = Physics2D.OverlapAreaAll(DragCornersVector[0], DragCornersVector[2]);
        GameObject[] OverlapObject = new GameObject[Overlaped.Length];
        int i = 0;
        while(i < Overlaped.Length)
        {
            OverlapObject[i] = Overlaped[i].gameObject;
            i++;
        }

        if (OverlapObject.Length >= 3)
            Answer_Wrong1_SetRemove(OverlapObject.Length, OverlapObject);
    }

    void Answer_Wrong1_SetRemove(int maxNum, GameObject[] overedOBJ)
    {
        int[] ranINT = new int[2];
        GameObject[] ranOBJ = new GameObject[2];

        ranINT[0] = Random.Range(0, maxNum);
        ranINT[1] = Random.Range(0, maxNum);

        if(ranINT[0] == ranINT[1])
        {
            for (int i = 0; i < ranINT.Length; i++)
            {
                ranINT[1] = Random.Range(0, maxNum);
                if (ranINT[0] != ranINT[1])
                    break;
            }
        }

        ranOBJ[0] = overedOBJ[ranINT[0]];
        ranOBJ[1] = overedOBJ[ranINT[1]];
        Answer_Wrong2_Remove_(ranOBJ);
    }

    void Answer_Wrong2_Remove_(GameObject[] RemoveOBJ)//알파애님
    {
        for (int i = 0; i < RemoveOBJ.Length; i++)//스켈레톤 혹은 스프라이트 이미지
        {
            if (RemoveOBJ[i].GetComponent<SkeletonAnimation>() != null)
                FadeOutEvent(RemoveOBJ[i].GetComponent<SkeletonAnimation>(), 0);
            else
                FadeOutEvent_sprite(RemoveOBJ[i].GetComponent<SpriteRenderer>(), 0);
        }
    }

    public void DragMoveSizeCheck()
    {
        print("최소사이즈 [" + CheckRange.sizeDelta.x / SizeRange_min + ", " + CheckRange.sizeDelta.y / SizeRange_min + "]");
        print("최대사이즈 [" + CheckRange.sizeDelta.x * SizeRange_max + ", " + CheckRange.sizeDelta.y * SizeRange_max + "]");
        if (DragBox.sizeDelta.x > CheckRange.sizeDelta.x / SizeRange_min && DragBox.sizeDelta.y >= CheckRange.sizeDelta.y / SizeRange_min)
        {
            if(DragBox.sizeDelta.x < CheckRange.sizeDelta.x * SizeRange_max && DragBox.sizeDelta.y < CheckRange.sizeDelta.y * SizeRange_max)
            {
                IsSizeFit = true;
                DragBox.gameObject.GetComponent<Image>().color = Color.green * new Color(1, 1, 1, 0.5f);
            }
            else
            {
                IsSizeFit = false;
                DragBox.gameObject.GetComponent<Image>().color = Color.grey * new Color(1, 1, 1, 0.5f); ;
            }
        }
        else
        {
            IsSizeFit = false;
            DragBox.gameObject.GetComponent<Image>().color = Color.grey * new Color(1, 1, 1, 0.5f); ;
        }
    }

    public void DragEndFigureOut()
    {
        if (IsSizeFit)//이거랑 정답영역인지 
        {
            if(RenderView.activeSelf == false)//정답 찾고 렌더뷰가 켜져도 이 부분이 실행되는걸 방지
            {
                if (DragHideCompare())
                {
                    RightAnswer();
                    print("Right!");
                    DragBox.gameObject.SetActive(false);
                }
                else
                {
                    Answer_Wrong0_GetList();
                    print("Wrong!");
                }
            }
        }
    }


    /// <summary>
    /// 드래그와 찾은이미지 비교
    /// 손이 떨어졌을때 드래그박스가 이미지범위에서 벗어났는지를 확인
    /// </summary>
    /// <returns>bool값을(찾았는지 아닌지의 여부) 리턴</returns>
    public bool DragHideCompare()
    {
        //정답 이미지의 모든 코너가 박스안에 들어왔을때로 수정하기
        for (int i = 0; i < ImageCornersVector.Length; i++)
        {
            if (DragRect.Contains(ImageCornersVector[i]))
                return false;
            else
                return true;
        }
        return true;
    }

    /// <summary>
    /// 이미지의 각 코너를 찾아주는 함수(이미지범위)
    /// </summary>
    void ImageAndDragboxCorners()
    {
        //이미지 코너 구하는거 추가해서 드래그박스가 이미지를 포함하는지 확인해야합니다[0712]
        DragBox.GetWorldCorners(DragCornersVector);
        //world_DragBoxPos = new Vector3(DragBox.position.x, DragBox.position.y, FindRange.GetComponent<SpriteRenderer>().bounds.center.z);
        Vector3 center = FindRange.GetComponent<SpriteRenderer>().bounds.center;
        Vector3 size_2 = FindRange.GetComponent<SpriteRenderer>().bounds.size / 2;

        ImageCornersVector[0] = center + size_2;
        ImageCornersVector[1] = center - size_2;
        ImageCornersVector[2] = new Vector3(ImageCornersVector[0].x, ImageCornersVector[1].y, 0);
        ImageCornersVector[3] = new Vector3(ImageCornersVector[1].x, ImageCornersVector[0].y, ImageCornersVector[0].z);

        DragRect = new Rect(FindRange.GetComponent<SpriteRenderer>().bounds.min, FindRange.GetComponent<SpriteRenderer>().bounds.size);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;//정답이미지
        Gizmos.DrawWireCube(FindRange.GetComponent<SpriteRenderer>().bounds.center, FindRange.GetComponent<SpriteRenderer>().bounds.size);//이거 각코너 구해서 해보기
        Gizmos.color = Color.black;//정답이미지
        Gizmos.DrawWireCube(DragRect.center, DragRect.size);//이거 각코너 구해서 해보기

        //Gizmos.color = Color.black;//월드_드래그박스센터
        //Gizmos.DrawWireSphere(world_DragBoxPos, 0.05f);
        Gizmos.color = Color.grey;//스크린_드래그박스센터
        Gizmos.DrawWireSphere(DragBox.position, 0.05f);

        for (int i = 0; i < DragCornersVector.Length; i++)//월드 드래그박스 각 모서리
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(DragCornersVector[i], 0.1f);
            Gizmos.color = Color.black - new Color(0,0,0, i * 0.2f) ;
            Gizmos.DrawWireSphere(ImageCornersVector[i], 0.1f);
        }
    }

    private void OnGUI()
    {
        //DragBox.rect.position = DragBox.position;
        GUI.Box(new Rect(0,0,100,100), "개시발");
    }
}

