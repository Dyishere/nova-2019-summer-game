using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBirdPlatformerControlelr : MonoBehaviour
{
    // [输入]
    private float inputMove;
    private bool inputJump;
    private bool inputDash = false;


    private BirdPlatformerMovement m_BirdPlatformerMovement;
    private void Awake()
    {
        m_BirdPlatformerMovement = GetComponent<BirdPlatformerMovement>();

    }
    private void Update()
    {
        inputMove = Input.GetAxis("Horizontal");
        inputJump = Input.GetKeyDown(KeyCode.W);
        inputDash = Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.F);
    }

    private void FixedUpdate()
    {
        m_BirdPlatformerMovement.Move(inputMove, ref inputJump, ref inputDash);
    }
}
