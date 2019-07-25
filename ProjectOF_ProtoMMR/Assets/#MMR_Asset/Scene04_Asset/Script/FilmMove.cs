using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilmMove : MonoBehaviour
{
    Vector3 startPOS;
    float EachFrameMag, MouseX;
    void DestroyAnimator()
    {
        GetComponent<Animator>().enabled = false;
    }

    public void OnDragBegan()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            startPOS = touch.position;
        }
    }
    public void OnDragMove()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 PrePOS = touch.position - touch.deltaPosition;

            Vector3 CurGap = startPOS - (Vector3)touch.position;
            Vector3 PreGap = startPOS - PrePOS;

            EachFrameMag = Mathf.Abs((PreGap.magnitude - CurGap.magnitude) * 0.1f);//적당한 소수점을 찾아야하는데 ㅗ

            Vector3 MoveGap = (PreGap - CurGap).normalized;
            MoveGap = new Vector3(MoveGap.x, 0, 0);
            MouseX = MoveGap.x;

            gameObject.transform.position += MoveGap * EachFrameMag;
        }
    }
}
