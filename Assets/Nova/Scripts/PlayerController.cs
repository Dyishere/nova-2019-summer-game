using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("游戏属性")]
    public float moveSpeed;
    public float jumpForce;
    [Space(10)]

    [Header("高级属性")]
    [SerializeField] private int maxJumpTimes = 2; // 可以连跳的次数
    [SerializeField] private float jumpBreakTime = .3f; // 两次跳跃之间的间隔时间
    [SerializeField] private GameObject CharacterSprite;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float groundCheckRadius = .3f;
    [SerializeField] private LayerMask m_WhatIsGround;

    [Space(10)]

    // [输入]
    private float inputMove;
    private bool inputJump;

    // [状态变量]
    private bool isGrounded = false;
    private int jumpCounter = 0; // 记录跳跃的次数（用于连跳）
    private float jumpTimer = 0f; // 记录跳跃后经过的时间
    private bool isFacingRight = true;
    // 默认情况下，角色美术资源应该统一向右看
    // 如果需要修改朝向,请在Unity Editor修改子物体 CharacterSprite 的 Scale.x 改为 -1
    // 这个组件会自动初始化 isFacingRight（在Start()方法）

    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = CharacterSprite.GetComponent<Animator>();
    }

    private void Start()
    {
        isFacingRight = CharacterSprite.transform.localScale.x > 0;
    }
    private void Update()
    {
        inputMove = Input.GetAxis("Move");
        inputJump = Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        // 移动
        Vector2 moveDir = new Vector2(inputMove * moveSpeed, 0);
        m_Rigidbody2D.velocity = moveDir * 100f * Time.deltaTime + m_Rigidbody2D.velocity.y * Vector2.up;
        // 如果需要水平后座力请使用注释中的代码（有bug）
        // Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        // m_Rigidbody2D.MovePosition(m_Rigidbody2D.position + moveDir * Time.deltaTime);


        // 跳跃
        bool jump = false;
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, groundCheckRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                jumpCounter = 0;
            }
        }

        jumpTimer += Time.deltaTime;
        if (inputJump)
        {
            if (isGrounded)
            {
                jump = true;
            }
            else
            {
                if (jumpCounter < maxJumpTimes - 1 && jumpTimer > jumpBreakTime)
                {
                    jump = true;
                }
            }
            inputJump = false;
        }

        if (jump)
        {
            m_Rigidbody2D.AddForce(Vector2.up * jumpForce);
            ++jumpCounter;
            jumpTimer = 0f;
        }

        // 翻转
        if ((inputMove > 0f && !isFacingRight) || (inputMove < 0f && isFacingRight))
        {
            Vector2 mScale = new Vector2(CharacterSprite.transform.localScale.x * (-1f), CharacterSprite.transform.localScale.y);
            CharacterSprite.transform.localScale = mScale;
            isFacingRight = !isFacingRight;
        }

        m_Animator.SetFloat("move", Mathf.Abs(inputMove));
    }
}
