using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ActiveButton : MonoBehaviour
{
    public List<GameObject> ActiveOBJ;
    bool isActivated = false;
    
    public void Activation()
    {
        if (!isActivated)
        {
            int i = 0;
            while (i < ActiveOBJ.Count)
            {
                ActiveOBJ[i].SetActive(true);
                i++;
            }
            isActivated = true;
        }
        else
        {
            int i = 0;
            while (i < ActiveOBJ.Count)
            {
                ActiveOBJ[i].SetActive(false);
                i++;
            }
            isActivated = false;
        }
    }
}
