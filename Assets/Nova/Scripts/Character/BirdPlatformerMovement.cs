using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 鸟类角色移动控制器，实现移动、多段跳、羽落效果、踩头攻击、冲刺攻击等功能。
/// 这个组件提供Move方法，请在其他组件（如玩家控制器、AI控制器）中调用该方法。
/// 请注意：默认情况下，角色美术资源应该统一向右看
/// 如果需要修改朝向,请在Unity Editor修改子物体 CharacterSprite 的 Scale.x 改为 -1
/// </summary>
public class BirdPlatformerMovement : MonoBehaviour
{

    [Header("游戏属性")]
    public float moveSpeed;
    public float jumpForce;
    public float flyingSpeed; // 羽落状态下增加的速度
    public float flyingBaseSpeed; // 羽落状态下与输入无关的初始速度
    public float flyingUpForce;

    [Space(10)]

    [Header("高级属性")]
    //与游戏内容无关的变量
    private const int maxJumpTimes = 2; // 可以连跳的次数
    [SerializeField] private float jumpBreakTime = .3f; // 两次跳跃之间的间隔时间
    [SerializeField] private GameObject CharacterSprite; // 将角色精灵分离设置成子物体，这样在转身的时候能保持父级物体的Scale始终为正值
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float groundCheckRadius = .3f;
    [SerializeField] private LayerMask m_WhatIsGround;

    [Space(10)]

    // [状态变量]
    private bool isGrounded = false;
    private int jumpCounter = 0; // 记录跳跃的次数（用于连跳）
    private float jumpTimer = 0f; // 记录跳跃后经过的时间
    private bool isFlying = false;
    private bool isFacingRight = true;

    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = CharacterSprite.GetComponent<Animator>();
    }

    private void Start()
    {
        //自动初始化 isFacingRight 的值
        isFacingRight = CharacterSprite.transform.localScale.x > 0;
    }

    public void Move(float iMove, bool iJump)
    {
        // 计算以下两个变量，然后应用变化
        Vector2 moveDir = new Vector2(iMove * moveSpeed, 0);
        bool jump = false;

        // 跳跃
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, groundCheckRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (m_Rigidbody2D.velocity.y < 0f)
                {
                    jumpCounter = 0;
                }
            }
        }

        jumpTimer += Time.deltaTime;
        if (iJump)
        {
            if (isGrounded)
            {
                jump = true;
            }
            else
            {
                if (jumpCounter < maxJumpTimes && jumpTimer > jumpBreakTime)
                {
                    jump = true;
                }
            }
            iJump = false;
        }

        if (jump)
        {
            m_Rigidbody2D.AddForce(Vector2.up * jumpForce);
            ++jumpCounter;
            m_Animator.SetTrigger("jump");
            jumpTimer = 0f;
        }

        // 羽落效果
        if (isGrounded || jumpCounter == 2)
        {
            isFlying = false;
        }
        else if (jumpCounter == 1 && m_Rigidbody2D.velocity.y < -0f)
        {
            isFlying = true;
        }
        m_Animator.SetBool("isFlying", isFlying);

        if (isFlying)
        {
            float facingDir = isFacingRight ? 1f : -1f;
            moveDir += new Vector2(iMove * flyingSpeed + facingDir * flyingBaseSpeed, 0);
            m_Rigidbody2D.AddForce(flyingUpForce * Vector2.up);
        }

        // 翻转
        if ((iMove > 0f && !isFacingRight) || (iMove < 0f && isFacingRight))
        {
            Vector2 mScale = new Vector2(CharacterSprite.transform.localScale.x * (-1f), CharacterSprite.transform.localScale.y);
            CharacterSprite.transform.localScale = mScale;
            isFacingRight = !isFacingRight;
        }

        // 移动
        m_Rigidbody2D.velocity = moveDir * 100f * Time.deltaTime + m_Rigidbody2D.velocity.y * Vector2.up;
        // 如果需要水平后座力可以考虑使用注释中的代码（有bug）
        // Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        // m_Rigidbody2D.MovePosition(m_Rigidbody2D.position + moveDir * Time.deltaTime);
        m_Animator.SetFloat("move", Mathf.Abs(iMove));
    }
}
