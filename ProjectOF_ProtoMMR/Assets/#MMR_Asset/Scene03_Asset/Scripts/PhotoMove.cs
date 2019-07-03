using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoMove : MonoBehaviour
{
    bool isdrag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isdrag = true;
        }else if (Input.GetMouseButtonUp(0))
        {
            isdrag = false;
        }

        if (isdrag)
        {
            Vector3 ScreenPosition = Camera.main.WorldToScreenPoint(Input.mousePosition + Vector3.forward);
            ScreenPosition = ScreenPosition.normalized;
            gameObject.transform.position += new Vector3(ScreenPosition.x, 0, 0);
            //이거 속도 맞춰주기
        }
    }
}
