using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCharaController : MonoBehaviour
{
    public float movingSpeed;

    //[本角色的区分信息]
    private int curCharaNum;        //用于区分当前角色的编号(0,1,2,3)
    private int curPlayerNum;       //用于区分当前玩家的编号(0,1,2,3)
    public string curController;    //用于控制器与当前角色对应

    //[安全区相关]
    public bool areaHurtSwitch = false;
    public bool inSafeArea = false;
    public bool areaHurting;

    //[捡拾相关]
    private string touchingProp = "null";        //玩家碰触的物品名字
    private bool isPicking = false;              //是否正在拿着东西

    // [输入]
    private float inputHorizontal, inputVertical;
    private bool inputPick;


    //本脚本挂载在角色上，其中Moving脚本为暂时的测试脚本，需要角色控制器脚本替换。
    //判断角色是否在危险时间位于危险区域，会传出一个areaHurting的布尔值，true为正在受到危险区域伤害
    private void Start()
    {
        CheckCurrentChara();         //按gameObject名字获取当前角色编号
    }

    private void Update()
    {
        if (areaHurting)
        {
            GameObject.Find("Main Camera").GetComponent<DungeonScoreController>().playerDie(curPlayerNum);
            areaHurting = false;
            gameObject.SetActive(false);
        }
        //触发一次互动键来首次储存该角色的控制,例如键盘的互动键为e，触发一次后便可用键盘移动此角色。
        //ControllerJudgement();

        //以传入的标签分别进行控制器的输入读取
        if (curController != "null")
            CharaController(curController);

        //测试用方法：当按下C时生成下一个角色，然后需要按下下一个角色的Action键绑定移动
        //if (Input.GetKeyDown(KeyCode.C))
        //    NextPlayerCreator();
    }

    private void FixedUpdate()
    {
        Moving(inputHorizontal,inputVertical);
        PickUp(inputPick, curCharaNum);
        AreaHurtJudgement();
    }

    private void CheckCurrentCharaTest()
    {
        foreach (char c in gameObject.name)
            if (Convert.ToInt32(c) >= 48 && Convert.ToInt32(c) <= 57)
                curCharaNum = Convert.ToInt32(c) - 48 - 1;
        curController = "K";
        curPlayerNum = 1;
    }       //获取当前玩家编号

    private void CheckCurrentChara()
    {
        foreach (char c in gameObject.name)
            if (Convert.ToInt32(c) >= 48 && Convert.ToInt32(c) <= 57)
                curCharaNum = Convert.ToInt32(c) - 48 - 1;
        int i = ScoringSystom.FindPlayerByChara((Character)curCharaNum);
        if (i == 5)
        {
            curController = "null";
            curPlayerNum = 5;
            gameObject.SetActive(false);
        }
        else
        {
            curController = ScoringSystom.PlayerInpuController[i].iController;
            curPlayerNum = (int)ScoringSystom.PlayerInpuController[i].iPlayerNum;
        }
    }       //获取当前玩家编号


    private void Moving(float iHorizontal,float iVertical)
    {
        transform.Translate(new Vector3(iHorizontal * movingSpeed, iVertical * movingSpeed, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SafeArea")
                inSafeArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "SafeArea")
            inSafeArea = false;
    }

    private void AreaHurtJudgement()
    {
        if (areaHurtSwitch && !inSafeArea)
            areaHurting = true;
        else
            areaHurting = false;
    }

    private void AreaHurtSwitch()
    {
        areaHurtSwitch = !areaHurtSwitch;
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
        inputHorizontal = Input.GetAxis(cCurController + "Horizontal");
        inputVertical = Input.GetAxis(cCurController + "Vertical");
        inputPick = Input.GetButtonDown(cCurController + "Pick");
    }       //已绑定控制器后分别读取输入

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

    private void PickUp(bool iPick, int curNum)
    {

        if (touchingProp == "null" || !iPick)       //如果未碰触物体或没有按下捡拾指令
            return;
        else
        {
            GameObject.Find("Egg/" + touchingProp).SendMessage("BeingPicked", curNum);     //触发碰触物体上的捡拾脚本Pickable
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
