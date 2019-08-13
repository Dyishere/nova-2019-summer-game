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

    private void Awake()
    {
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
        VerifyImage.SetActive(false);
        if (player == Player.p1 || player == Player.p3)
            Tips1.SetActive(true);
        else if (player == Player.p2 || player == Player.p4)
            Tips2.SetActive(true);

        // 确定角色
        ScoringSystom.PlayerShow[(int)player].Show = true;
        ScoringSystom.PlayerShow[(int)player].character = (Character)spriteIndex;
        Verifyed = true;
    }

    public void UnlockedOnClick()
    {
        LockedImage.SetActive(false);
        VerifyImage.SetActive(true);
        unlock = false;
        verifyImageShow = true;
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
    }

    private void SetSprite()
    {
        iM.sprite = playerSprites[spriteIndex];
        iR.sprite = playerSprites[_addIndex(spriteIndex)];
        iL.sprite = playerSprites[_subIndex(spriteIndex)];
    }

    private void VerifyKeyDown()
    {
        VerifyImage.SetActive(false);
        if (player == Player.p1 || player == Player.p3)
            Tips1.SetActive(true);
        else if (player == Player.p2 || player == Player.p4)
            Tips2.SetActive(true);

        // 确定角色
        ScoringSystom.PlayerShow[(int)player].Show = true;
        ScoringSystom.PlayerShow[(int)player].character = (Character)spriteIndex;
        Verifyed = true;
    }

    private void UnlockedKeyDown()
    {
        LockedImage.SetActive(false);
        VerifyImage.SetActive(true);
        unlock = false;     
    }
}
