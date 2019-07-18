using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

/// <summary>
/// idle상태가있다면 isgotDEFAULT에 체크(isitLOOP는 무시)
/// idle상태가없다면 루프여부를 isitLOOP에 체크
/// </summary>
public class Spine_Touch : MonoBehaviour
{
    SkeletonAnimation anim;
    public List<string> anim_name = new List<string>(0);
    public bool isitLOOP = false;
    bool isgotDEFAULT = false;
    //퍼블릭불로 true인 애들만 리플레이 가능하게 수정하기
    int ListIndex = 0;

    private void Start()
    {
        anim = GetComponent<SkeletonAnimation>();
        if (anim.AnimationName != null)
        {
            isgotDEFAULT = true;
            ListIndex = 1;
        }
    }
    
    private void OnMouseUp()
    {
        if (isgotDEFAULT)
        {//0번은 디폴트 애니메이션
            if (ListIndex < anim_name.Count)
            {
                anim.loop = false;
                anim.AnimationName = anim_name[ListIndex];
                ListIndex++;
                if (ListIndex == anim_name.Count)
                {
                    StartCoroutine(WaitAnimEnd_HaveDefault());
                }
            }
        }else
        {
            if (ListIndex < anim_name.Count)
            {
                anim.AnimationName = anim_name[ListIndex];
                ListIndex++;
                if (isitLOOP && ListIndex == anim_name.Count)
                {
                    StartCoroutine(WaitAnimEnd_HaveNothing());
                }
            }
        }
    }

    IEnumerator WaitAnimEnd_HaveDefault()
    {
        while (!anim.state.GetCurrent(0).IsComplete)
        {
            yield return new WaitForEndOfFrame();
        }
        anim.AnimationName = anim_name[0];
        anim.loop = true;
        ListIndex = 1;
    }

    IEnumerator WaitAnimEnd_HaveNothing()
    {
        while (!anim.state.GetCurrent(0).IsComplete)
        {
            yield return new WaitForEndOfFrame();
        }
        anim.AnimationName = null;
        ListIndex = 0;
        print("애님 리셋");
    }
}

