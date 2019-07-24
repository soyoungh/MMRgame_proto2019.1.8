using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 루프로 쓰려면 애니메이터에서 빈 애니메이션 추가 및 연결
/// </summary>
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
