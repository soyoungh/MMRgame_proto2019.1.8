using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이거 지워도되나여
/// <summary>
/// 찾은 그림이 적당한 위치에 있는지 확인(비교)하는 클래스
/// </summary>
public class Picture_Compare : MonoBehaviour
{
    public GameObject HideMMR;
    public Picture_Find ins_find;
    public Image_FindRightAnswer ins_findRight;

    Vector3 MinSize, MaxSize;
    Vector3 MinPos, MaxPos;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (ins_findRight.DragHideCompare())
            {
                print("찾았다" + ins_findRight.DragHideCompare());
            }
            else
            {
                print("못찾았다" + ins_findRight.DragHideCompare());
            }
        }
    }
}
