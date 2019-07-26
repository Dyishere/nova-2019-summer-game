using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // [输入]
    private float inputMove;
    private bool inputJump;

    private BirdPlatformerMovement m_BirdPlatformerMovement;
    private void Awake()
    {
        m_BirdPlatformerMovement = GetComponent<BirdPlatformerMovement>();
    }

    private void Update()
    {
        inputMove = Input.GetAxis("Move");
        inputJump = Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        m_BirdPlatformerMovement.Move(inputMove, inputJump);
    }
}
