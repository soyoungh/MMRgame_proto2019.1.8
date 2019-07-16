using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_Anim_Touch : MonoBehaviour
{
    Animator anim;
    public string animName;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0;
    }
    private void OnMouseUp()
    {
        anim.speed = 1;
        anim.Play(animName);
    }
}
