using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour
{
    public Player player;
    public GameObject LockedImage;
    public GameObject VerifyImage;
    public GameObject Tips1;
    public GameObject Tips2;
    public bool Verifyed = false;

    // 切换角色选择图片
    public List<Sprite> playerSprites;
    public GameObject ImageM;
    public GameObject ImageL;
    public GameObject ImageR;

    // 切换图片辅助变量
    private Image iM;
    private Image iL;
    private Image iR;
    private int spriteIndex;
    private bool keyDown = true;

    // 
    private bool verifyImageShow = false;
    private bool unlock = true;

    //[此栏操控的游戏角色信息]
    public int curCharacterNum;
    public int curPanelNum;

    //[此栏操控的玩家控制信息]
    public string curPlayerController;

    private void Awake()
    {
        CheckCurPanelNum();
        iM = ImageM.GetComponent<Image>();
        iL = ImageL.GetComponent<Image>();
        iR = ImageR.GetComponent<Image>();
    }

    private void Start()
    {
        // 初始化UI状态
        InitUI();

        // 初始化角色图片
        spriteIndex = 0;
        SetSprite();
    }

    void Update()
    {
        SetSprite();
        CheckCurController();

        if (curPlayerController != "null" && !ScoringSystom.PlayerJoin[curPanelNum])     //当前控制器已绑定后或上一玩家还未绑定时不接受绑定
        {
            if (Input.GetAxis(curPlayerController + "Horizontal") < -0.5f && keyDown)
            {
                SubIndex();
                keyDown = false;
            }
            else if (Input.GetAxis(curPlayerController + "Horizontal") > 0.5f && keyDown)
            {
                AddIndex();
                keyDown = false;
            }

            if (Mathf.Abs(Input.GetAxis(curPlayerController + "Horizontal")) < 0.5f && !keyDown)
                keyDown = true;

            if (Input.GetButtonDown(curPlayerController + "Pick") && unlock)
            {
                UnlockedKeyDown();
            }
            if (Input.GetButtonUp(curPlayerController + "Action") && verifyImageShow)
            {
                VerifyKeyDown();
            }
            if (Input.GetButtonDown(curPlayerController + "Action") && !Verifyed)
                verifyImageShow = true;

        } 
        /*
        // 以下需区分四人的独立输入
        if (Input.GetKey(KeyCode.A) && keyDown) 
        {
            SubIndex();
            keyDown = false;
        }
        else if (Input.GetKey(KeyCode.D) && keyDown)
        {
            AddIndex();
            keyDown = false;
        }
        if (Input.GetKeyUp(KeyCode.A) && !keyDown)
            keyDown = true;
        if (Input.GetKeyUp(KeyCode.D) && !keyDown)
            keyDown = true;

        if (Input.GetKey(KeyCode.V) && unlock)
        {
            UnlockedKeyDown();
        }
        if (Input.GetKey(KeyCode.V) && verifyImageShow)
        {
            VerifyKeyDown();
        }
        if (Input.GetKeyUp(KeyCode.V) && !verifyImageShow)
            verifyImageShow = true;
        */
    }
    
    public void SubIndex()
    {
        spriteIndex = _subIndex(spriteIndex);
    }
    public void AddIndex()
    {
        spriteIndex = _addIndex(spriteIndex);
    }



    // 图片切换辅助方法
    private int _subIndex(int i)
    {
        i--;
        if (i < 0)
            i = 3;

        return i;
    }

    private int _addIndex(int i)
    {
        i++;
        if (i > 3)
            i = 0;
        return i;
    }


    // 按钮方法
    public void VerifyOnClick()
    {
        if (ScoringSystom.CheckCharaJoin((Character)spriteIndex))
            return;     //当该角色已被其他玩家选择时返回
        VerifyImage.SetActive(false);
        if (player == Player.p1 || player == Player.p3)
            Tips1.SetActive(true);
        else if (player == Player.p2 || player == Player.p4)
            Tips2.SetActive(true);

        // 确定角色
        ScoringSystom.PlayerShow[(int)player].Show = true;
        ScoringSystom.PlayerShow[(int)player].character = (Character)spriteIndex;
        Verifyed = true;

        //[输入相关]
        ScoringSystom.PlayerInpuController[curPanelNum - 1].iCharaNum = (Character)spriteIndex;
        ScoringSystom.PlayerJoin[curPanelNum] = true;
        CharaCreator();
    }

    public void UnlockedOnClick()
    {
        LockedImage.SetActive(false);
        VerifyImage.SetActive(true);
        unlock = false;
        verifyImageShow = false;
    }

    // 
    private void InitUI()
    {
        verifyImageShow = false;
        unlock = true;
        LockedImage.SetActive(true);
        VerifyImage.SetActive(false);
        Tips1.SetActive(false);
        Tips2.SetActive(false);
        ScoringSystom.Init();
    }

    private void SetSprite()
    {
        iM.sprite = playerSprites[spriteIndex];
        iR.sprite = playerSprites[_addIndex(spriteIndex)];
        iL.sprite = playerSprites[_subIndex(spriteIndex)];
    }

    private void VerifyKeyDown()
    {
        if (ScoringSystom.CheckCharaJoin((Character)spriteIndex))
            return;
        VerifyImage.SetActive(false);
        if (player == Player.p1 || player == Player.p3)
            Tips1.SetActive(true);
        else if (player == Player.p2 || player == Player.p4)
            Tips2.SetActive(true);

        // 确定角色
        ScoringSystom.PlayerShow[(int)player].Show = true;
        ScoringSystom.PlayerShow[(int)player].character = (Character)spriteIndex;
        Verifyed = true;

        //[输入相关]
        ScoringSystom.PlayerInpuController[curPanelNum - 1].iCharaNum = (Character)spriteIndex;
        ScoringSystom.PlayerJoin[curPanelNum] = true;
        CharaCreator();
    }

    private void UnlockedKeyDown()
    {
        LockedImage.SetActive(false);
        VerifyImage.SetActive(true);
        unlock = false;
    }

    //[输入相关]
    private void CheckCurPanelNum()
    {
        foreach (char c in gameObject.name)
            if (Convert.ToInt32(c) >= 48 && Convert.ToInt32(c) <= 57)
                curPanelNum = Convert.ToInt32(c) - 48;
        curPlayerController = "null";
    }

    private void CheckCurController()
    {
        if (curPlayerController != "null" || !ScoringSystom.PlayerJoin[curPanelNum - 1])     //当前控制器已绑定后或上一玩家还未绑定时不接受绑定
            return;
        else
        {
            if (Input.GetButtonDown("KPick"))
            {
                if (ScoringSystom.CheckControllerJoin("K"))
                    return;
                if (ScoringSystom.PlayerInpuController[curPanelNum - 1].iCharaNum == Character.b0)
                    UnlockedOnClick();
                curPlayerController = "K";
                ScoringSystom.PlayerInpuController[curPanelNum-1].iController = curPlayerController;
            }
            else
                for (int i = 1; i < 10; i++)
                    if (Input.GetButton("J" + i + "Pick"))
                    {
                        if (ScoringSystom.CheckControllerJoin("J" + i))
                            return;
                        if (ScoringSystom.PlayerInpuController[curPanelNum - 1].iCharaNum == Character.b0)
                            UnlockedOnClick();
                        curPlayerController = "J" + i;
                        ScoringSystom.PlayerInpuController[curPanelNum-1].iController = curPlayerController;
                    }

        }
    }

    private void CharaCreator()
    {
        string charaChosed = "Player" + ((int)ScoringSystom.PlayerInpuController[curPanelNum-1].iCharaNum+1);
        //Debug.Log(charaChosed);
        GameObject[] all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        for (int i = 0; i < all.Length; i++)        //寻找被禁用的player
        {
            var item = all[i];
            if (item.scene.isLoaded && item.name == charaChosed)
            {
                item.SetActive(true);
            }
        }
    }
}
