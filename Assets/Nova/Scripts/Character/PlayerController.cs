using System;
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
    private bool inputDash = false;

    //[本角色的区分信息]
    private int curPlayerNum;             //用于区分当前角色的编号
    public string curController;    //用于控制器与当前角色对应

    //[DoubleClick判定用]
    private int rPressCount = 0;// 按下的右次数
    private int lPressCount = 0;// 按下的左次数
    private bool rSwitchDir; //更换右双击方向
    private bool lSwitchDir; //更换左双击方向
    private float rPressedTime; // 按下第一次右时的时间记录
    private float lPressedTime; // 按下第一次左时的时间记录

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
        InitialDoubleClick();
        m_BirdPlatformerMovement.Move(inputMove, ref inputJump, ref inputDash);
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
            else
                for (int i = 1; i < 10; i++)
                    if (Input.GetButton("J" + i + "Action"))
                        curController = "J" + i;
        }
    }       //通过生成角色后第一次按下的互动键来绑定角色与控制器

    private void CharaController(string cCurController)
    {
        inputMove = Input.GetAxis(cCurController + "Move");
        inputPick = Input.GetButtonDown(cCurController + "Pick");
        if (Input.GetButtonDown(cCurController + "Jump"))
            inputJump = true;
        inputAction = Input.GetButtonDown(cCurController + "Action");
        if (DoubleClick(Input.GetAxis(cCurController + "Move")))
            inputDash = true;
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

    private bool DoubleClick (float press)
    {
        if (press > 0)
        {
            if (rPressCount == 2)
            {
                //Debug.Log("DoubleClick!!!");
                rPressedTime = 0;
                rPressCount = 0;
                return true;
            }       //当count记录到按下两次后即可返回true，并且初始化变量


            if (press >= 0.2f && rPressCount < 2)
            {
                rSwitchDir = true;
            }       //当我们按下且输入了axis后，开启双击判定流程
            else if (rSwitchDir && rPressedTime < 0.5f)
            {
                if (rPressCount == 0)
                {
                    rPressCount = 1;
                    rSwitchDir = false;
                }
                else if (rPressCount == 1)
                {
                    rPressCount = 2;
                    rSwitchDir = false;
                }
            }       //当我们已经第一次输入axis后的处于2f的时间内进行判定
        }
        else if (press <= 0)
        {
            if (lPressCount == 2)
            {
                //Debug.Log("DoubleClick!!!");
                lPressedTime = 0;
                lPressCount = 0;
                return true;
            }       //当count记录到按下两次后即可返回true，并且初始化变量
            if (lPressCount == 1)
            {
                lPressedTime += Time.deltaTime;
            }       //当按下第一次时的时间记录

            if (press <= -0.2f && lPressCount < 2)
            {
                lSwitchDir = true;
            }       //当我们按下且输入了axis后，开启双击判定流程
            else if (lSwitchDir && lPressedTime < 0.5f)
            {
                if (lPressCount == 0)
                {
                    lPressCount = 1;
                    lSwitchDir = false;
                }
                else if (lPressCount == 1)
                {
                    lPressCount = 2;
                    lSwitchDir = false;
                }
            }       //当我们已经第一次输入axis后的处于2f的时间内进行判定
            if (lPressCount == 1 && lPressedTime >= 0.5f)
            {
                lPressedTime = 0;
                lPressCount = 0;
            }       //当我们已经第一次输入axis后超过了双击时间时的判定
        }
        return false;
    }       //判定双击，传入移动输入的axis进行判断

    private void InitialDoubleClick()
    {
        if (rPressCount == 1)
        {
            rPressedTime += Time.deltaTime;
        }       //当按下第一次时的时间记录
        if (rPressCount == 1 && rPressedTime >= 0.5f)
        {
            rPressedTime = 0;
            rPressCount = 0;
        }       //当我们已经第一次输入axis后超过了双击时间时的判定
        if (lPressCount == 1)
        {
            lPressedTime += Time.deltaTime;
        }       //当按下第一次时的时间记录
        if (lPressCount == 1 && lPressedTime >= 0.5f)
        {
            lPressedTime = 0;
            lPressCount = 0;
        }       //当我们已经第一次输入axis后超过了双击时间时的判定
    }       //双击间隔判定
}
