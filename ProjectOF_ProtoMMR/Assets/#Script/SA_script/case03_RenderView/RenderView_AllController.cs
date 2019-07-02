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
    public Image_FindRightAnswer ins_findRight;

    private void OnEnable()
    {
        Image_FindRightAnswer.RightAnswer += this.RenderViewControl;
    }
    
    public void RenderViewControl()
    {
        print("is it play once?");
        ins_RVcam.CorrectAnswerMove();//드래그범위로 캠이동
        ins_changeMMR.StartCoroutine(ins_changeMMR.ChangeMMR());//정답이미지 변환, 렌더뷰활성화
        ins_prevent.DisableDragZoom();//드래그 및 줌 방지
    }
}


