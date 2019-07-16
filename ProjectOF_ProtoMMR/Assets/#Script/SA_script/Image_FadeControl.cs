using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

//시발 스켈레톤알파 애님 추가하기
// 

/// <summary>
/// ##____[UI와 2d Sprite중 결정필요]____##
/// spine 파일로 받으면 아예 ui이런거 없이 게임오브젝트로 들어감
/// obj.GetComponent<SkeletonAnimation>().skeleton.a << 스켈레톤알파호출
/// </summary>
public class Image_FadeControl : MonoBehaviour
{
    public static Image_FadeControl FadeInstance = null;

    private void Awake()
    {
        if (FadeInstance == null)
            FadeInstance = this;
        else
            Destroy(this);

        Image_FindRightAnswer.FadeOutEvent += this.StartFadeOut;
        Image_FindRightAnswer.FadeOutEvent_sprite += this.StartFadeOut_sprite;
    }
    private void OnDisable()
    {
        Image_FindRightAnswer.FadeOutEvent -= this.StartFadeOut;
        Image_FindRightAnswer.FadeOutEvent_sprite -= this.StartFadeOut_sprite;
    }

    void StartFadeOut(SkeletonAnimation BeforeSprite, float FirstWait)
    {
        StartCoroutine(FadeOut(BeforeSprite, FirstWait));
    }
    void StartFadeOut_sprite(SpriteRenderer BeforeSprite, float FirstWait)
    {
        StartCoroutine(FadeOut_sprite(BeforeSprite, FirstWait));
    }


    public IEnumerator FadeOut(SkeletonAnimation BeforeSprite, float FirstWait)
    {
        yield return new WaitForSeconds(FirstWait);
        while(BeforeSprite.skeleton.a > 0)
        {
            BeforeSprite.skeleton.a -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public IEnumerator FadeIn(SkeletonAnimation BeforeSprite, float FirstWait)
    {
        yield return new WaitForSeconds(FirstWait);
        while (BeforeSprite.skeleton.a < 1)
        {
            BeforeSprite.skeleton.a += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator FadeOut_sprite(SpriteRenderer BeforeSprite, float FirstWait)
    {
        yield return new WaitForSeconds(FirstWait);
        while (BeforeSprite.color.a > 0)
        {
            BeforeSprite.color -= new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public IEnumerator FadeIn_sprite(SpriteRenderer BeforeSprite, float FirstWait)
    {
        yield return new WaitForSeconds(FirstWait);
        while (BeforeSprite.color.a < 1)
        {
            BeforeSprite.color += new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
