using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Common
{
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float jumpForce;
    [SerializeField]
    protected float highJumpForce;
    [SerializeField]
    protected float dropForce;

    protected float horizontal = 0f;
    protected float vertical = 0f;
    protected bool isAttacking = false;
    protected float attackTime = 0f;
    protected bool isHighJumping = false;
    protected bool isBlocking = false;

    protected Rigidbody2D rigidbody2D;
    protected Animator animator;

    [SerializeField]
    protected GameObject existSpellPoint;

    protected virtual void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (!isAttacking) UpdateDirection(horizontal);
        StopMovingWhileAttackingAndHighJumpingAndBlocking();
    }
    protected void UpdateDirection(float horizontal)
    {
        float direction = Mathf.Abs(transform.localScale.x);
        if (horizontal > 0)
        {
            transform.localScale = new Vector2(direction, transform.localScale.y);
        }
        else if (horizontal < 0f)
        {
            transform.localScale = new Vector2(-direction, transform.localScale.y);
        }
    }
    protected float GetClipTime(string stateName)
    {
        float length = 0f;
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name.Equals(stateName))
            {
                length = clip.length;
                break;
            }
        }
        return length;
    }
    protected void StopMovingWhileAttackingAndHighJumpingAndBlocking()
    {
        if ((isAttacking && (rigidbody2D.velocity.x != 0f && rigidbody2D.velocity.y == 0f))
            || (isHighJumping && (rigidbody2D.velocity.x != 0f && rigidbody2D.velocity.y == 0f))
            || (isBlocking && (rigidbody2D.velocity.x != 0f && rigidbody2D.velocity.y == 0f)))
        {
            rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
        }        
    }
    protected float GetRawAxisInput(string axisName)
    {
        float direction = 0f;
        if (axisName.Equals("Horizontal"))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction = -1f;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction = 1f;
            }
        }
        if (axisName.Equals("Vertical"))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                direction = 1f;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                direction = -1f;
            }
        }
        return direction;
    }

}
