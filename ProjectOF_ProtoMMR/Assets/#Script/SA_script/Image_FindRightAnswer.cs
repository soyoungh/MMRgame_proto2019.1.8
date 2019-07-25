#define CASE_DELET_ALL_WRONG
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
    public static event RenderViewDelegate RightAnswer;
    public static event RenderViewDelegate PhotoActive;
    //RenderView_AllController에서 처리할 델리게이트, 이벤트

    public delegate void FadeDelegate(SkeletonAnimation BeforeSprite, float FirstWait);
    public static event FadeDelegate FadeOutEvent;
    public delegate void FadeDelegate_sprite(SpriteRenderer BeforeSprite, float FirstWait);
    public static event FadeDelegate_sprite FadeOutEvent_sprite;
    //페이드 부분 관련 델리게이트, 이벤트

    public delegate void RollDelegate();
    public static event RollDelegate RollCount;
    //Ui_CamRoll 부분 관련 델리게이트, 이벤트

    public GameObject RenderView, Give;
    public RectTransform DragBox, CheckRange;
    public RectTransform[] ImageContain = new RectTransform[4];
    public float SizeRange_min, SizeRange_max;

    bool IsSizeFit = false;
    public int Num_RightAnswer = 0;
    
    private void OnEnable()
    {
        Play_CheckTouch.TouchMoved_FromAnswer += this.DragMoveSizeCheck;
        Play_CheckTouch.TouchEnd_FromAnswer += this.DragEndFigureOut;
    }
    private void OnDisable()
    {
        Play_CheckTouch.TouchMoved_FromAnswer -= this.DragMoveSizeCheck;
        Play_CheckTouch.TouchEnd_FromAnswer -= this.DragEndFigureOut;
    }
    public void DragMoveSizeCheck()
    {
        if (DragBox.sizeDelta.x > CheckRange.sizeDelta.x / SizeRange_min && DragBox.sizeDelta.y >= CheckRange.sizeDelta.y / SizeRange_min)
        {
            if (DragBox.sizeDelta.x < CheckRange.sizeDelta.x * SizeRange_max && DragBox.sizeDelta.y < CheckRange.sizeDelta.y * SizeRange_max)
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
        if (IsSizeFit )//이거랑 정답영역인지 
        {
            if(RenderView.activeSelf == false)//정답 찾고 렌더뷰가 켜져도 이 부분이 실행되는걸 방지
            {
                RollCount();
                DragBox.gameObject.SetActive(false);
                PhotoActive();
                if (DragHideCompare())
                {
                    RightAnswer();
                    Give.SetActive(true);
                    print("Right!");
                }
                else
                {
                    Answer_Wrong0_GetList();
                    print("Wrong!");
                }
                IsSizeFit = false;
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
        for (int i = 0; i < ImageContain.Length; i++)
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(DragBox, ImageContain[i].position))
                return false;
        }
        return true;
    }

    public void Answer_Wrong0_GetList()
    {
        Vector3[] DragCornersVector = new Vector3[4];
        DragBox.GetWorldCorners(DragCornersVector);
        Collider2D[] Overlaped = Physics2D.OverlapAreaAll(DragCornersVector[0], DragCornersVector[2], 1 << 13);
        GameObject[] OverlapObject = new GameObject[Overlaped.Length];
        int i = 0;
        while (i < Overlaped.Length)
        {
            OverlapObject[i] = Overlaped[i].gameObject;
            print("겹침" + OverlapObject[i].name);
            i++;
        }
#if CASE_DELET_ALL_WRONG
        Answer_Wrong2_Remove_(OverlapObject);
#else
        if (OverlapObject.Length >= 3)
            Answer_Wrong1_SetRemove(OverlapObject.Length, OverlapObject);
#endif
    }

#if CASE_DELET_ALL_WRONG
#else
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
#endif

    void Answer_Wrong2_Remove_(GameObject[] RemoveOBJ)//알파애님
    {
        for (int i = 0; i < RemoveOBJ.Length; i++)//스켈레톤 혹은 스프라이트 이미지
        {
            if (RemoveOBJ[i].GetComponent<SkeletonAnimation>() != null)
                FadeOutEvent(RemoveOBJ[i].GetComponent<SkeletonAnimation>(), 0);
            else
                FadeOutEvent_sprite(RemoveOBJ[i].GetComponent<SpriteRenderer>(), 0);

            RemoveOBJ[i].GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}