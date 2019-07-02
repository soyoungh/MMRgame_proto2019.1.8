using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 정답을 찾을경우 원래이미지로 바뀌는 클래스
/// #이미지변환 #랜더뷰활성화
/// </summary>
public class RenderView_ChangeMMR : MonoBehaviour
{
    public GameObject RenderViewAnchor;
    SpriteRenderer BeforeFindMMR, AfterFindMMR;

    void Start()
    {
        BeforeFindMMR = GetComponent<SpriteRenderer>();
        SpriteRenderer[] Temp_AfterFindMMR = GetComponentsInChildren<SpriteRenderer>();
        AfterFindMMR = Temp_AfterFindMMR[1];

        //나중에 기획보고 수정해야하는 사항//규칙없음
    }

    public IEnumerator ChangeMMR()
    {
        RenderViewAnchor.SetActive(true);
        yield return new WaitForSeconds(1f);
        while (AfterFindMMR.color.a < 1)
        {
            BeforeFindMMR.color -= new Color(0, 0, 0, 0.1f);
            AfterFindMMR.color += new Color(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        print("Changed!");
    }
}
