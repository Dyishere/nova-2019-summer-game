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
    public float dashTime;

    [Space(10)]

    //与游戏内容无关的变量

    [Header("高级属性")]
    [SerializeField] private float jumpBreakTime = .3f; // 两次跳跃之间的间隔时间
    [SerializeField] private GameObject CharacterSprite = null; // 将角色精灵分离设置成子物体，这样在转身的时候能保持父级物体的Scale始终为正值(预防潜在bug?)
    [SerializeField] private Transform GroundCheck = null;
    [SerializeField] private float groundCheckRadius = .3f;
    [SerializeField] private LayerMask m_WhatIsGround = 0;
    [SerializeField] private float hurtVelocityY = 0.2f; // 被踩头的时候，对方的掉落速度最小值
    [SerializeField] [Range(0f, 1f)] private float headGetHurtRange = 0.9f; // 属于可以被踩的头的范围是多少（头部边缘踩到后不属于有效攻击）
    [SerializeField] private float recoveringTime = 1f; // 被攻击后需要的恢复时间，期间为眩晕状态，无法操作

    [Space(10)]
    // [状态变量]
    [Header("角色状态")]
    public bool isDashing = false;
    private const int maxJumpTimes = 2; // 可以连跳的次数
    private bool isFacingRight = true;
    private bool isGrounded = false;
    private int jumpCounter = 0; // 记录跳跃的次数（用于连跳）
    private float jumpTimer = 0f; // 记录跳跃瞬间后经过的时间
    private bool isFlying = false;
    private bool dashed = false; // 在这次滑翔中是否已经冲刺过
    private float dashTimer = 0f;
    private bool isHurt = false; // 被攻击后的眩晕状态, 无法操作, 短时间内不会被继续受伤害
    private string touchingProp = "null";        //玩家碰触的物品名字
    private bool isPicking = false;              //是否正在拿着东西

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

    /// <summary>
    /// 由GetButtonDown方法计算的变量（以下以iJump为例），应传入引用。
    /// GetButtonDown使iJump的值为true，但应该由Move方法来使其值重新变为false
    /// 这样可以避免Update方法和FixUpdate方法调用错位的问题，并保证跳跃代码只执行一次
    /// 错位问题举例：Update函数每帧调用，FixedUpdate函数0.2s调用一次。小概率情况下，在某帧中按下跳跃键，但该帧没有调用FixedUpdate，这时候跳跃代码不会执行。到下一帧时，iJump变回false。到下一次执行FixedUpdate函数代码时，iJump值为false。于是这次按键没有成功跳跃。
    /// </summary>
    /// <param name="iMove"></param>
    /// <param name="iJump"></param>
    public void Move(float iMove, ref bool iJump, ref bool iDash)
    {
        //bool iDash = Input.GetKeyDown(KeyCode.J); // 测试用的代码,之后会替换掉

        // 眩晕状态或者冲刺阶段，禁用控制
        if (isHurt || isDashing)
        {
            iMove = 0f;
            iJump = false;
        }
        // 计算以下两个变量，然后应用变化
        Vector2 moveDir = new Vector2(iMove * moveSpeed, 0); // 移动方向
        bool jump = false; // 是否跳跃

        // 跳跃
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, groundCheckRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                dashed = false;
                if (m_Rigidbody2D.velocity.y < 0f) //防止起跳瞬间执行
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

        // 羽落效果(滑翔)
        if (isGrounded || jumpCounter == 2)
        {
            isFlying = false;
        }
        else if (jumpCounter == 1 && m_Rigidbody2D.velocity.y < -0f)
        {
            isFlying = true;
        }
        m_Animator.SetBool("isFlying", isFlying);

        float facingDir = isFacingRight ? 1f : -1f;
        if (isFlying)
        {
            moveDir += new Vector2(iMove * flyingSpeed + facingDir * flyingBaseSpeed, 0);

            m_Rigidbody2D.AddForce(flyingUpForce * Vector2.up);

            if (iDash && !dashed && !isDashing)
            {
                m_Rigidbody2D.AddForce(facingDir * Vector2.right * dashForce);
                m_Animator.SetBool("dash", true);
                isDashing = true;
                dashed = true;
                iDash = false;
                dashTimer = 0f;
            }
        }

        dashTimer += Time.deltaTime;
        if (dashTimer > dashTime || !isFlying)
        {
            isDashing = false;
            m_Animator.SetBool("dash", false);
        }

        // 翻转
        if ((iMove > 0f && !isFacingRight) || (iMove < 0f && isFacingRight))
        {
            Vector2 mScale = new Vector2(CharacterSprite.transform.localScale.x * (-1f), CharacterSprite.transform.localScale.y);
            CharacterSprite.transform.localScale = mScale;
            isFacingRight = !isFacingRight;
            transform.Find("PickPos").transform.localPosition = new Vector2(transform.Find("PickPos").transform.localPosition.x * -1, 1);
        }

        // 移动(冲刺状态下不能直接修改velocity)
        if (!isDashing)
        {
            m_Rigidbody2D.velocity = moveDir * 100f * Time.deltaTime + m_Rigidbody2D.velocity.y * Vector2.up;
        }
        m_Animator.SetFloat("move", Mathf.Abs(iMove));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 被踩头
        PlayerController m_player = other.gameObject.GetComponent<PlayerController>();
        if (m_player && other.rigidbody.velocity.y < -1f * hurtVelocityY)
        {
            CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
            float headRadius = capsule.size.x;
            float playerHeight = capsule.size.y;
            bool isHit = false;
            foreach (ContactPoint2D m_contact in other.contacts)
            {
                // 踩到圆形头部的上半部分 算作有效攻击
                Vector2 contactPos = m_contact.point;
                // 接触点到头中心的距离差（transform.position在脚底）
                float dx = contactPos.x - transform.position.x;
                float dy = contactPos.y - (transform.position.y + playerHeight - headRadius);

                if (dy > 0f && (Mathf.Abs(dx) < headRadius * headGetHurtRange))
                {
                    isHit = true;
                    break;
                }
            }
            if (isHit)
            {
                GetHurt();
            }
        }
    }

    private void GetHurt()
    {
        if (isHurt) // 防止多次受伤
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

    public void PickUp(bool iPick, int curPlayerNum)        //PlayerController调用，传入当前的玩家编号
    {

        if (touchingProp == "null" || !iPick)       //如果未碰触物体或没有按下捡拾指令
            return;
        else
        {
            GameObject.Find(touchingProp).SendMessage("BeingPicked", curPlayerNum);     //触发碰触物体上的捡拾脚本Pickable
            isPicking = !isPicking;
            if (!isPicking)     //放下物品时初始化
                touchingProp = "null";
        }
    }

    private void PickUpPermit(string message)
    {
        if (!isPicking)
            touchingProp = message;
    }       //监视是否碰触可捡拾物品并获取物品名字
}
