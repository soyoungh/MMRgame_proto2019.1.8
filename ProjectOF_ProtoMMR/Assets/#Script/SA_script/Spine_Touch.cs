using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

/// <summary>
/// idle상태가있다면 isgotDEFAULT에 체크(isitLOOP는 무시)
/// idle상태가없다면 루프여부를 isitLOOP에 체크
/// </summary>

[System.Serializable]//editor에서 접근가능하게
public class Spine_Touch : MonoBehaviour
{
    SkeletonAnimation anim;
    public SkeletonAnimation OtherAnim;
    public List<string> anim_name = new List<string>(0);

    public bool isLOOP = false;
    bool isIDLE = false;
    public bool PlayME = false;
    public bool PlayOTHER = false;
    
    bool isitDragging = false;
    int ListIndex = 0;

    private void OnEnable()
    {
        Play_CheckTouch.SpineStart_FromSpine += this.OnTouchEntering;
        Play_CheckTouch.SpineMoved_FromSpine += this.OnDragging;
    }
    private void OnDisable()
    {
        Play_CheckTouch.SpineStart_FromSpine -= this.OnTouchEntering;
        Play_CheckTouch.SpineMoved_FromSpine -= this.OnDragging;
    }

    private void Start()
    {
        if (PlayME)
        {
            anim = GetComponent<SkeletonAnimation>();
            if (anim.AnimationName != null)
            {
                isIDLE = true;
                ListIndex = 1;
            }
        }else if (PlayOTHER)
        {
            anim = OtherAnim;
            if (anim.AnimationName != null)
            {
                isIDLE = true;
                ListIndex = 1;
            }
        }
    }
    void OnDragging()
    {
        isitDragging = true;
    }
    void OnTouchEntering()
    {
        isitDragging = false;
    }

    private void OnMouseUp()
    {if (isitDragging) return;

        OnPlayME();
        
    }

    public void OnPlayME()
    {
        if (isIDLE)
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
        }
        else
        {
            if (ListIndex < anim_name.Count)
            {
                anim.AnimationName = anim_name[ListIndex];
                ListIndex++;
                if (isLOOP && ListIndex == anim_name.Count)
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
        if (isLOOP)
        {
            anim.loop = true;
            anim.AnimationName = anim_name[0];
            ListIndex = 1;
        }
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

