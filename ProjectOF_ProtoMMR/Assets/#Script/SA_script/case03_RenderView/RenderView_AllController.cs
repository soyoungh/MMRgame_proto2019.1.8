using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 정답을 찾을경우, render view관련 컨트롤
/// # 랜더뷰활성화
/// # 드래그 및 줌 방지
/// # 이미지변환
/// # 랜더 카메라 이동및 확대
/// </summary>
public class RenderView_AllController : MonoBehaviour
{
    public RenderView_RenderCam ins_RVcam;
    public RenderView_ChangeMMR ins_changeMMR;
    public RenderView_PreventDrag ins_prevent;
    public RenderView_Resize ins_resize;

    public GameObject RenderView;
    public SpriteRenderer BeforeFindMMR, AfterFindMMR;//퍼블릭으로 가져오는건 임시적, 코드로 가져올수있게 수정필요

    private void OnEnable()
    {
        Image_FindRightAnswer.RightAnswer += this.RenderViewControl;//From FRA
    }
    
    public void RenderViewControl()
    {
        ins_RVcam.CorrectAnswerMove();//드래그범위로 캠이동
        RenderView.SetActive(true);//렌더뷰활성화
        ins_resize.Resizing();
        
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[ 수정필요 ]~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~##
        //ins_changeMMR.StartCoroutine(ins_changeMMR.ChangeMMR());//정답이미지 변환
        if (Image_FadeControl.FadeInstance != null)
        {
            StartCoroutine(Image_FadeControl.FadeInstance.FadeOut(BeforeFindMMR, 1f));
            StartCoroutine(Image_FadeControl.FadeInstance.FadeIn(AfterFindMMR, 1f));//정답이미지만 따로 get해와서 넣어줄수 있게 수정하기0703
        }
        else
            print("singleton is null");
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~##
        
        ins_prevent.DisableDragZoom();//드래그 및 줌 방지
    }
}