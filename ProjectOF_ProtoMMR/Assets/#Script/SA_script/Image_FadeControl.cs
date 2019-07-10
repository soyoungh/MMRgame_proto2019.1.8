using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    void StartFadeOut(Image BeforeSprite, float FirstWait)
    {
        StartCoroutine(FadeOut(BeforeSprite, FirstWait));
    }


    public IEnumerator FadeOut(Image BeforeSprite, float FirstWait)
    {
        yield return new WaitForSeconds(FirstWait);
        while(BeforeSprite.color.a > 0)
        {
            BeforeSprite.color -= new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public IEnumerator FadeIn(Image BeforeSprite, float FirstWait)
    {
        yield return new WaitForSeconds(FirstWait);
        while (BeforeSprite.color.a < 1)
        {
            BeforeSprite.color += new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
