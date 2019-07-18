using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

/// <summary>
/// //##____[UI와 2d Sprite중 결정필요]____##
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
    public RenderView_Retransform ins_resize;

    public GameObject RenderView;
    public SkeletonAnimation BeforeFindMMR, AfterFindMMR;//퍼블릭으로 가져오는건 임시적, 코드로 가져올수있게 수정필요
    public SpriteRenderer BeforeFindMMR_sprite, AfterFindMMR_sprite;
    //이거를 스켈레톤알파랑 이미지알파 둘다 가능하게바꾸기
    

    private void OnEnable()
    {
        Image_FindRightAnswer.RightAnswer += this.RightAnswerActive;
        Image_FindRightAnswer.PhotoActive += this.RenderPhotoActive;
    }
    private void OnDisable()
    {
        Image_FindRightAnswer.RightAnswer -= this.RightAnswerActive;
        Image_FindRightAnswer.PhotoActive -= this.RenderPhotoActive;
    }

    public void RenderPhotoActive()//정답이미지(랜더뷰) 활성화
    {
        ins_RVcam.CorrectAnswerMove();//드래그범위로 캠이동
        RenderView.SetActive(true);//렌더뷰활성화
        ins_resize.MaskResizing();
        ins_prevent.DisableDragZoom();//드래그 및 줌 방지
    }

    public void RightAnswerActive()//정답일경우 이미지 변화
    {
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~[ 수정필요 ]~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~##
        //ins_changeMMR.StartCoroutine(ins_changeMMR.ChangeMMR());//정답이미지 변환
        if (Image_FadeControl.FadeInstance != null)
        {
            if (BeforeFindMMR != null) StartCoroutine(Image_FadeControl.FadeInstance.FadeOut(BeforeFindMMR, 1f));
            else StartCoroutine(Image_FadeControl.FadeInstance.FadeOut_sprite(BeforeFindMMR_sprite, 1f));
            if (AfterFindMMR != null) StartCoroutine(Image_FadeControl.FadeInstance.FadeIn(AfterFindMMR, 1f));//정답이미지만 따로 get해와서 넣어줄수 있게 수정하기0703
            else StartCoroutine(Image_FadeControl.FadeInstance.FadeIn_sprite(AfterFindMMR_sprite, 1f));
        }
        else
            print("singleton is null");
        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~##
    }
}