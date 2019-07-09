﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 숨은그림을 구별하여 찾는 클래스
/// #이미지의rect생성 #이미지모서리찾기 #드래그박스모서리찾기 #기즈모그리기 #드래그최소범위구분
/// 드래그영역내에 오답이 3개 이상일경우 그중 2개지워버리기
/// </summary>
public class Image_FindRightAnswer : MonoBehaviour
{
    public delegate void RenderViewDelegate();
    public static event RenderViewDelegate RightAnswer;//RenderView_AllController
    public delegate void FadeDelegate(SpriteRenderer BeforeSprite, float FirstWait);
    public static event FadeDelegate FadeOutEvent;
    //public static event FadeDelegate FadeInEvent;

    public GameObject RenderView;
    public RectTransform DragBox, CheckRange;
    public Vector3 ImagePosition;
    SpriteRenderer HideSpriteRender;
    Rect HideSpriteRect;
    bool IsSizeFit = false;

    Vector3[] DragCornersVector = new Vector3[4];
    Vector3[] ImageCornersVector = new Vector3[4];


    public Texture temp_gizmo;
    // Start is called before the first frame update
    void Start()
    {
        HideSpriteRender = GetComponent<SpriteRenderer>();
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

    void Answer_Wrong2_Remove_(GameObject[] RemoveOBJ)
    {
        for (int i = 0; i < RemoveOBJ.Length; i++)
        {
            FadeOutEvent(RemoveOBJ[i].GetComponent<SpriteRenderer>(), 0);
        }
    }

    public void DragMoveSizeCheck()
    {
        print("최소사이즈 [" + CheckRange.sizeDelta.x / 1.5f + ", " + CheckRange.sizeDelta.y / 1.5f + "]");
        print("최대사이즈 [" + CheckRange.sizeDelta.x * 3 + ", " + CheckRange.sizeDelta.y * 3 + "]");
        if (DragBox.sizeDelta.x > CheckRange.sizeDelta.x / 1.5f && DragBox.sizeDelta.y >= CheckRange.sizeDelta.y / 1.5f)
        {
            if(DragBox.sizeDelta.x < CheckRange.sizeDelta.x * 3 && DragBox.sizeDelta.y < CheckRange.sizeDelta.y * 3)
            {
                IsSizeFit = true;
                DragBox.gameObject.GetComponent<Image>().color = Color.green * new Color(1, 1, 1, 0.5f);
            }else
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
        for (int i = 0; i < 4; i++)
        {
            if (!HideSpriteRect.Contains(DragCornersVector[i], true))//정답의 코너와 바로 비교하기때문에 따로 콜라이더가 없어도되네
                return false;
            //나중에 이부분을 배열로 이용해서 찾은 정답은 삭제하고 남은애들끼리 다 확인하는 그런게 필요함
        }
        return true;
    }

    /// <summary>
    /// 이미지의 각 코너를 찾아주는 함수(이미지범위)
    /// 드래그박스의 각 코너를 저장해줌
    /// 이미지의 범위를 각각 계산해서 rect로 재정의(후에 contain명령어를 쓰기위함)
    /// </summary>
    void ImageAndDragboxCorners()
    {
        ImagePosition = gameObject.transform.position;
        Vector3 HalfLength = HideSpriteRender.bounds.size * 0.5f;

        ImageCornersVector[0] = ImagePosition + HalfLength;
        ImageCornersVector[1] = ImagePosition - HalfLength;
        ImageCornersVector[2] = new Vector3(ImageCornersVector[0].x, ImageCornersVector[1].y, 0);
        ImageCornersVector[3] = new Vector3(ImageCornersVector[1].x, ImageCornersVector[0].y, ImageCornersVector[0].z);

        HideSpriteRect.xMin = ImageCornersVector[3].x;
        HideSpriteRect.yMin = ImageCornersVector[3].y;
        HideSpriteRect.xMax = ImageCornersVector[2].x;
        HideSpriteRect.yMax = ImageCornersVector[2].y;
        
        //이거 너무 더러운뎅, 수정할 방법 찾기
        // Image Corner Save
        
        DragBox.GetWorldCorners(DragCornersVector);
        // DragBox Corner Save
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, GetComponent<SpriteRenderer>().bounds.size);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.05f);

        if(HideSpriteRect != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(new Vector2(HideSpriteRect.center.x, HideSpriteRect.center.y), new Vector2(HideSpriteRect.size.x, HideSpriteRect.size.y));
        }
    }
}

