using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Spine_OneTouch : MonoBehaviour
{
    SkeletonAnimation anim;
    public string anim_name;
    bool isonceplayed = false;

    private void Start()
    {
        anim = GetComponent<SkeletonAnimation>();
    }

    private void Update()
    {
        if(isonceplayed && anim.state.GetCurrent(0).IsComplete)
        {
            anim.AnimationName = null;
            isonceplayed = false;

        }
    }
    private void OnMouseUp()
    {
        anim.AnimationName = anim_name;
        isonceplayed = true;
        //anim끝나면 null을 넣어줘서 리플레이 가능하게  수정하기
        //anim이 끝나면 부르는 함수가있는지 찾아보기
    }
}
