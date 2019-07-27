﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // [输入]
    private float inputMove;
    private bool inputJump;
    private bool inputAction;
    private bool inputPick;

    //[本角色的区分信息]
    private int curPlayerNum;             //用于区分当前角色的编号
    public string curController;    //用于控制器与当前角色对应
    private BirdPlatformerMovement m_BirdPlatformerMovement;
    private void Awake()
    {
        m_BirdPlatformerMovement = GetComponent<BirdPlatformerMovement>();
    }

    private void Start()
    {
        curController = "null";     //初始化
        CurrentPlayerNum();         //按gameObject名字获取当前角色编号
    }

    private void Update()
    {
        //触发一次互动键来首次储存该角色的控制,例如键盘的互动键为e，触发一次后便可用键盘移动此角色。
        ControllerJudgement();

        //以传入的标签分别进行控制器的输入读取
        if (curController != "null")
            CharaController(curController);

        //测试用方法：当按下C时生成下一个角色，然后需要按下下一个角色的Action键绑定移动
        if (Input.GetKeyDown(KeyCode.C))
            NextPlayerCreator();
    }

    private void FixedUpdate()
    {
        m_BirdPlatformerMovement.Move(inputMove, ref inputJump);
        m_BirdPlatformerMovement.PickUp(inputPick, curPlayerNum);
    }

    private void ControllerJudgement()
    {
        if (curController != "null")
            return;
        else
        {
            if (Input.GetButtonDown("KAction"))
                curController = "K";
            else if (Input.GetButton("J1Action"))
                curController = "J1";
            else if (Input.GetButton("J2Action"))
                curController = "J2";
            else if (Input.GetButton("J3Action"))
                curController = "J3";
        }
    }       //通过生成角色后第一次按下的互动键来绑定角色与控制器

    private void CharaController(string cCurController)
    {
        switch (cCurController)
        {
            case "K":
                inputMove = Input.GetAxis("KMove");
                inputPick = Input.GetButtonDown("KPick");
                if (Input.GetButtonDown("KJump"))
                    inputJump = true;
                inputAction = Input.GetButtonDown("KAction");
                break;
            case "J1":
                inputMove = Input.GetAxis("J1Move");
                inputPick = Input.GetButtonDown("J1Pick");
                if (Input.GetButtonDown("J1Jump"))
                    inputJump = true;
                inputAction = Input.GetButtonDown("J1Action");
                break;
            case "J2":
                inputMove = Input.GetAxis("J2Move");
                inputPick = Input.GetButtonDown("J2Pick");
                if (Input.GetButtonDown("J2Jump"))
                    inputJump = true;
                inputAction = Input.GetButtonDown("J2Action");
                break;
            case "J3":
                inputMove = Input.GetAxis("J3Move");
                inputPick = Input.GetButtonDown("J3Pick");
                if (Input.GetButtonDown("J3Jump"))
                    inputJump = true;
                inputAction = Input.GetButtonDown("J3Action");
                break;
        }
    }       //已绑定控制器后分别读取输入

    private void CurrentPlayerNum()
    {
        if (curPlayerNum > 0)
            return;
        else
            foreach (char c in gameObject.name)
                if (Convert.ToInt32(c) >= 48 && Convert.ToInt32(c) <= 57)
                    curPlayerNum = Convert.ToInt32(c) - 48;
    }       //获取当前玩家编号

    private void NextPlayerCreator()
    {
        int nextNum = curPlayerNum + 1;
        string nextPlayer = "Player" + nextNum;
        if (nextNum > 4)
        {
            Debug.Log("已达到最多角色数");
        }
        else
        {
            GameObject[] all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
            for (int i = 0; i < all.Length; i++)        //寻找被禁用的player
            {
                var item = all[i];
                if (item.scene.isLoaded && item.name == nextPlayer)
                {
                    item.SetActive(true);
                }
            }
        }
    }


}
