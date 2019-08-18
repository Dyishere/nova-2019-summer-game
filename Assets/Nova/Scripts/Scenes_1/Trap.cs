using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void Start()
    {  
        Invoke("TrapDestroy", 12f);
    }

    void TrapDestroy()
    {

        TrapCreator.traps.Remove((int)(transform.position.x+0.5f));
        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.name)
        {
            case "Player1":
                ScoringSystem.ChangePlayerScore(Player.p1, -10);
                Debug.Log("Player1的分数是：" + ScoringSystem.P1Score);
                TrapDestroy();
                break;
            case "Player2":
                ScoringSystem.ChangePlayerScore(Player.p1, -10);
                Debug.Log("Player2的分数是：" + ScoringSystem.P2Score);
                TrapDestroy();
                break;
            case "Player3":
                ScoringSystem.ChangePlayerScore(Player.p1, -10);
                Debug.Log("Player3的分数是：" + ScoringSystem.P3Score);
                TrapDestroy();
                break;
            case "Player4":
                ScoringSystem.ChangePlayerScore(Player.p1, -10);
                Debug.Log("Player4的分数是：" + ScoringSystem.P4Score);
                TrapDestroy();
                break;
        }
    }
}
