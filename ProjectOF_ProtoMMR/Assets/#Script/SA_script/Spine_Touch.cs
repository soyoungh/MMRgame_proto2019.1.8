using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class Spine_Touch : MonoBehaviour
{
    SkeletonAnimation anim;
    public List<string> anim_name = new List<string>(0);
    bool isonceplayed = false;
    int ListIndex = 0;

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
        if (ListIndex < anim_name.Count)
        {
            anim.AnimationName = anim_name[ListIndex];
            print("현재 애니메이션 번호 : " + ListIndex);
            isonceplayed = true;
            ListIndex++;
        }
        else
            print("애니메이션 끝!");
        //anim끝나면 null을 넣어줘서 리플레이 가능하게  수정하기
        //anim이 끝나면 부르는 함수가있는지 찾아보기
    }
}
