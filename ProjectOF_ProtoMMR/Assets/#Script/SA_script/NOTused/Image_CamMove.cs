//#define DEBUG_UsingRaycastForDragMove
//#define DEBUG_KeepDistanceWithZ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this is not used
/// </summary>
public class Image_CamMove : MonoBehaviour
{
    public Ui_CamZoom ins_camzoom;
    public Ui_CamOnOff ins_uicam;
    public Image_SameDegreeCamMove ins_rotGrid;
    public GameObject cam, camDegree;
    Vector3 startPoint, endPoint;
    public bool isdrag, iszoomin = false;

    bool forDebuging = false;
#if DEBUG_UsingRaycastForDragMove
    Vector3 startPointwithZ, endPointwithZ, tempHit;
#endif
#if DEBUG_KeepDistanceWithZ
    public float distance;
    Vector3 hitpoint, zStart;
#endif


    // Update is called once per frame
    void Update()
    {

#if DEBUG_KeepDistanceWithZ
        //raycast2, distance movement
        forDebuging = true;
        // 축회전하지말고 매번 z가 raypoint닿은 포지션에서 특정거리만큼을 유지하게하기
        //https://gamedev.stackexchange.com/questions/120410/make-object-follow-another-object-while-keeping-the-same-distance-from-it
        //keep distance로 검색해서 수정하기
        if (Input.GetMouseButtonDown(0) && Input.touchCount < 2)
        {
            Vector3 tempMousePos = Input.mousePosition;
            startPoint = Camera.main.ScreenToWorldPoint(new Vector3(
                tempMousePos.x, tempMousePos.y, -Camera.main.transform.position.z));
            print(startPoint);

            zStart = cam.transform.position;
            distance = cam.transform.position.z - hitpoint.z;//-9.9
            isdrag = true;
        }
        else if (Input.GetMouseButtonUp(0))
            isdrag = false;

        if (!ins_uicam.Iscam && !ins_camzoom.IsTouch2)
        {
            if (Input.touchCount < 2 && isdrag)
            {
                Vector3 tempMousePos = Input.mousePosition;
                endPoint = Camera.main.ScreenToWorldPoint(new Vector3(
                    tempMousePos.x, tempMousePos.y, -Camera.main.transform.position.z));

                RaycastHit hit;
                if(Physics.Raycast(cam.transform.position, Vector3.forward, out hit))
                {
                    hitpoint = hit.point;
                }
                

                if (ins_rotGrid.isZoomin)
                {
                    iszoomin = true;

                    startPoint.z = distance;
                    endPoint.z = hitpoint.z - distance;
                    var tempGAP = (endPoint - startPoint).normalized;//endPoint - startPoint;
                    //var tempMOVE = Camera.main.transform.position - tempGAP;
                    //tempMOVE.z = hitpoint.z + distance;
                    camDegree.transform.localPosition -= tempGAP * 3 * Time.deltaTime; //= Camera.main.transform.position - tempGAP;//tempMOVE;//
                    print("처음클릭: " + startPoint + " / 매번클릭: " + endPoint);
                    print("tempgap : "+tempGAP);
                }
                else
                {
                    var tempGAP = endPoint - startPoint;
                    camDegree.transform.localPosition = Camera.main.transform.position - tempGAP;
                }
            }
        }
#endif

#if DEBUG_UsingRaycastForDragMove
        //Raycast movement1
        if (Input.GetMouseButtonDown(0) && Input.touchCount < 2)
        {
            Vector3 tempMousePos = Input.mousePosition;
            startPoint = Camera.main.ScreenToWorldPoint(new Vector3(tempMousePos.x, tempMousePos.y, 1));
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            isdrag = true;

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                startPointwithZ = hit.point;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isdrag = false;
        }

        if(!ins_uicam.Iscam && ins_camzoom.IsTouch2 == false)
        {
            if(Input.touchCount < 2 && isdrag)
            {
                Vector3 tempMousePos = Input.mousePosition;
                endPoint = Camera.main.ScreenToWorldPoint(new Vector3(tempMousePos.x, tempMousePos.y, 1));

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    endPointwithZ = hit.point;
                }

                if (ins_rotGrid.isZoomin)
                {
                    //확대
                    var DragGap = endPointwithZ - startPointwithZ;
                    camDegree.transform.Translate(-DragGap, Space.Self);
                    iszoomin = true;
                }
                else
                {
                    //축소
                    var DragGap = endPoint - startPoint;
                    camDegree.transform.position = cam.transform.position - DragGap;
                }
            }
        }
#else
        if (!forDebuging)
        {
            //Defaul Movement
            if (Input.GetMouseButtonDown(0) && Input.touchCount < 2)
            {
                Vector3 tempMousePos = Input.mousePosition;
                startPoint = Camera.main.ScreenToWorldPoint(new Vector3(
                    tempMousePos.x, tempMousePos.y, -Camera.main.transform.position.z));
                isdrag = true;
            }
            else if (Input.GetMouseButtonUp(0))
                isdrag = false;

            if (!ins_uicam.Iscam && !ins_camzoom.IsTouch2)
            {
                if (Input.touchCount < 2 && isdrag)
                {
                    Vector3 tempMousePos = Input.mousePosition;
                    endPoint = Camera.main.ScreenToWorldPoint(new Vector3(
                        tempMousePos.x, tempMousePos.y, -Camera.main.transform.position.z));

                    if (ins_rotGrid.isZoomin)
                    {
                        iszoomin = true;
                        var tempGAP = endPoint - startPoint;
                        camDegree.transform.Translate(-tempGAP, Space.Self);
                    }
                    else
                    {
                        var tempGAP = endPoint - startPoint;
                        camDegree.transform.localPosition = Camera.main.transform.position - tempGAP;
                    }
                }
            }
        }
#endif

        if (!isdrag && iszoomin)
        {
            if (!ins_rotGrid.isZoomin)
            {
                camDegree.transform.position = Vector3.MoveTowards(camDegree.transform.position,
                    new Vector3(camDegree.transform.position.x, camDegree.transform.position.y, -10), Time.deltaTime * 2f);

                if (camDegree.transform.localPosition.z == -10)
                    iszoomin = false;
            }
        }

    }
    private void OnDrawGizmos()
    {
#if DEBUG_UsingRaycastForDragMove
        Gizmos.color = Color.red;
        Gizmos.DrawLine(endPoint, endPointwithZ);
        Gizmos.DrawWireSphere(endPoint, 0.03f);
        //Draw last touch point(world) and ray hit point
#endif
#if DEBUG_KeepDistanceWithZ
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(hitpoint, 0.05f);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(endPoint, 0.08f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startPoint, 0.08f);

#endif
    }

    /*
    Vector3 ScreenToWorld(Vector3 MouseValue)
    {
        Vector3 WorldValue = cam.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(
            MouseValue.x, MouseValue.y, -cam.transform.position.z));
        return WorldValue;
    }
    */
}