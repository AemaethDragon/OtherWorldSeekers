using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakuraAnimation : MonoBehaviour
{
    public AnimationClip anim;
    public Animator animator;
    float waitTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        waitTime = anim.length + 30f;
        InvokeRepeating("PlayAnimation", 30f, waitTime);
    }

    void PlayAnimation()
    {

        animator.Play("WindMovement");

    }
}
