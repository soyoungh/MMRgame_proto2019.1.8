using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 드래그 박스 중심에서 레이를 쏴서 숨은그림 찾는 클래스
/// </summary>
public class Picture_Find : MonoBehaviour
{
    [Tooltip("GAME OBJECT, check whether it is actived or not")]
    public GameObject DragBox;
    [Tooltip("SCRIPT OBJECT, bring vector3 for get clicked point position")]
    public Play_DrawDrag ins_drag;
    [Tooltip("BOOL, check if raycast hit or not")]
    public bool isHit = false;

    Ray ray;
    RaycastHit hit;
    Vector3 RayPosition, RayPoint;

    //이거 쓸모없는 스크립튼지 확인하고 지우기
    // Update is called once per frame
    void Update()
    {
        if (DragBox.activeSelf)//
        {
            //캠ui버튼 활성화, 터치카운트1, 마우스 다운
            RayPosition = new Vector3((Input.mousePosition.x + ins_drag.firstPoint.x) / 2,
                                     (Input.mousePosition.y + ins_drag.firstPoint.y) / 2);
            ray = Camera.main.ScreenPointToRay(RayPosition);//?

            if(Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                isHit = true;
                RayPoint = hit.point;
            }
            else
            {
                isHit = false;
            }
            
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.blue);
        }
    }

    private void OnDrawGizmos()//not used
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(RayPoint, 0.1f);
    }
}

/*
 * 드래그 센터가 이미지가 찍히고 나서 저장되는 값이라 
 * 실시간으로 값이 들어오지 않아서 바로 확인하기 어려움
 * 차라리 ins_save에서 가져오지말고 이 스크립트에서 
 * 바로 계산해서 사용하는게 나을수도 있음
 * 
 * 똑같네
 */
