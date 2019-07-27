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
    public float flyingUpForce; // 空气对鸟翼的支持力（浮力？）
    public float dashForce;

    [Space(10)]

    //与游戏内容无关的变量

    [Header("高级属性")]
    [SerializeField] private float jumpBreakTime = .3f; // 两次跳跃之间的间隔时间
    [SerializeField] private GameObject CharacterSprite; // 将角色精灵分离设置成子物体，这样在转身的时候能保持父级物体的Scale始终为正值(预防潜在bug?)
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float groundCheckRadius = .3f;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private float hurtVelocityY = 0.2f; // 被踩头的时候，对方的掉落速度最小值
    [SerializeField] [Range(0f, 1f)] private float headGetHurtRange = 0.9f; // 属于可以被踩的头的范围是多少（头部边缘踩到后不属于有效攻击）
    [SerializeField] private float recoveringTime = 1f; // 被攻击后需要的恢复时间，期间为眩晕状态，无法操作

    [Space(10)]

    // [状态变量]
    private const int maxJumpTimes = 2; // 可以连跳的次数
    private bool isFacingRight = true;
    private bool isGrounded = false;
    private int jumpCounter = 0; // 记录跳跃的次数（用于连跳）
    private float jumpTimer = 0f; // 记录跳跃后经过的时间
    private bool isFlying = false;
    private bool dashed = false; // 在这次滑翔中是否已经冲刺过
    private bool isHurt = false; // 被攻击状态/眩晕状态, 无法操作, 短时间内不会被继续伤害

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
        bool iDash = Input.GetKeyDown(KeyCode.J); // 测试用的代码之后会替换掉

        // 眩晕状态，无法操作
        if (isHurt)
        {
            return;
        }


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
                dashed = false;
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

            if (!dashed && iDash)
            {
                // 冲刺状态变化 如“金身模式”，全身发光？？？（未实现）
                // 加力出现历史遗留BUG！！移动方式为直接修改velocity所以加力之后只会瞬移！
                // m_Rigidbody2D.AddForce(facingDir * Vector2.right * dashForce);
                m_Animator.SetTrigger("dash"); // 状态机还有问题
                dashed = true;
            }
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 潜在BUG，Contcts数组可能不止一个元素，有可能有碰撞没有被捕捉到
        // 被踩头
        PlayerController m_player = other.gameObject.GetComponent<PlayerController>();
        if (m_player && other.rigidbody.velocity.y < -1f * hurtVelocityY)
        {
            CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
            float headRadius = capsule.size.x;
            float playerHeight = capsule.size.y;

            // 踩到圆形头部的上半部分 算作有效攻击
            float dx = other.GetContact(0).point.x - transform.position.x;

            // 接触点到头中心的距离差（transform.position在脚底）
            float dy = other.GetContact(0).point.y - (transform.position.y + playerHeight - headRadius);

            if (dy > 0f && (Mathf.Abs(dx) < headRadius * headGetHurtRange))
            {
                // 这部分可能会连续触发
                GetHurt();
            }
            Debug.Log("Contcts数组元素个数：" + other.contacts.Length.ToString());
        }
    }

    private void GetHurt()
    {
        if (isHurt)
            return;

        isHurt = true;

        // 眩晕部分代码
        m_Animator.SetBool("hurt", true);

        Invoke("Recover", recoveringTime);
    }

    private void Recover()
    {
        isHurt = false;
        m_Animator.SetBool("hurt", false);
    }
}
