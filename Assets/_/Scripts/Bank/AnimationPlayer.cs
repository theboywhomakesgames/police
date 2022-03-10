using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    public void PlayAnimation(string stateName)
    {
        AnimatorClipInfo[] clipsInfo;
        clipsInfo = animator.GetCurrentAnimatorClipInfo(0);
        currentState = clipsInfo[0].clip.name;

        if (stateName == currentState)
            return;

        animator.CrossFade(stateName, easeDuration);
        currentState = stateName;
    }

    [SerializeField] protected Animator animator;
    [SerializeField] protected string currentState;
    [SerializeField] protected float easeDuration = 0.5f;
}
