using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSocket : MonoBehaviour
{
    [SerializeField]
    private AnimationClip[] animations;

    private Animator animator;
    private AnimatorOverrideController animatorOverrideController;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;

        animatorOverrideController["Idle"] = animations[0];
        animatorOverrideController["Walk"] = animations[1];
        animatorOverrideController["Run"] = animations[2];
        animatorOverrideController["Jump"] = animations[3];
        animatorOverrideController["Fall"] = animations[4];
        animatorOverrideController["Attack_Bow"] = animations[5];
        animatorOverrideController["HighJump"] = animations[6];
        animatorOverrideController["Block"] = animations[7];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayMovementAnimation(string state, int statee)
    {
        animator.SetInteger(state, statee);
    }
    public void PlayAttackAnimation(string clipName)
    {
        animator.SetTrigger(clipName);
    }
}
