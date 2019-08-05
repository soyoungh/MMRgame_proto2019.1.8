using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine_Answer : MonoBehaviour
{
    public Spine_Touch fishing_ins_spine;
    public Spine_Touch saver_ins_spine;
    public GameObject Help;

    public string Add_animname1;
    public string Add_animname2;
    public string Add_animname3;
    public GameObject[] right_;

    bool stop_ = false;
    // Update is called once per frame
    void Update()
    {
        if(!stop_ && fishing_ins_spine.ListIndex == 4)
        {
            saver_ins_spine.anim_name.Add(Add_animname1);
            saver_ins_spine.anim_name.Add(Add_animname2);
            saver_ins_spine.anim_name.Add(Add_animname3);
            Invoke("Show_", 4f);
            stop_ = true;
        }

        if(saver_ins_spine.ListIndex == 4)
        {
            saver_ins_spine.gameObject.GetComponent<Animator>().enabled = true;
            for (int i = 0; i < right_.Length; i++)
            {
                right_[i].SetActive(true);
            }
        }
    }

    void Show_()
    {
        Help.SetActive(true);
    }
}
