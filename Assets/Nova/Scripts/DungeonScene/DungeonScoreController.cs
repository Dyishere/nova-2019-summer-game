using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScoreController : MonoBehaviour
{
    public int eachEggValue;
    private int totalFeather = 0;
    float playerAccount = 0f;
    private GameObject foundation;
    private bool[] playerExist = new bool[4];

    private void Start()
    { 
        for (int i = 0; i < 4; i++)
            playerExist[i] = true;
    }

    public void playerDie(int PlayerNum)
    {
        playerExist[PlayerNum] = false;
    }

    public void EggMatch (bool match)
    {
        if (match)
            totalFeather += eachEggValue;
        else
            totalFeather -= eachEggValue;
    }

    private void EggScoreAccount()
    {
        Transform[] foundation = GameObject.Find("Foundation").GetComponentsInChildren<Transform>();

        foreach (var child in foundation)
        {
            if (child.GetComponent<DungeonFoundation>().isMatch)
                totalFeather += eachEggValue;
        }
    }

    private void PlayerExistAccount()
    {
        for (int i = 0; i < 4; i++)
        {
            if (playerExist[i])
                playerAccount += 1f;
        }
    }

    public void FinalAccount()      // 结束时调用此方法计算选票数
    {
        EggScoreAccount();
        PlayerExistAccount();
        for (int i = 0;i < 4; i++)
        {
            if (playerExist[i])
            {
                ScoringSystem.ChangePlayerFeather((Player)i, (int)(totalFeather / playerAccount));
            }
        }
    }
}
