using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    int whoPick = 0;
    private Pickable PA;

    private void Start()
    {
        PA = GetComponent<Pickable>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag!="Player")
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

            Invoke("TrashDestroy", 12f);
        }
        else
        {
            switch (collision.gameObject.name)
            {
                case "Player1":
                    ScoringSystem.ChangePlayerScore(Player.p1, -10);
                    Debug.Log("Player1的分数是：" + ScoringSystem.P1Score);
                    TrashDestroy();
                    break;
                case "Player2":
                    ScoringSystem.ChangePlayerScore(Player.p2, -10);
                    Debug.Log("Player2的分数是：" + ScoringSystem.P2Score);
                    TrashDestroy();
                    break;
                case "Player3":
                    ScoringSystem.ChangePlayerScore(Player.p3, -10);
                    Debug.Log("Player3的分数是：" + ScoringSystem.P3Score);
                    TrashDestroy();
                    break;
                case "Player4":
                    ScoringSystem.ChangePlayerScore(Player.p4, -10);
                    Debug.Log("Player4的分数是：" + ScoringSystem.P4Score);
                    TrashDestroy();
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.name)
        {
            case "nest1":
                ScoringSystem.ChangePlayerScore(Player.p1, -10);
                Debug.Log("Player1的分数是：" + ScoringSystem.P1Score);
                TrashDestroy();
                break;
            case "nest2":
                ScoringSystem.ChangePlayerScore(Player.p2, -10);
                Debug.Log("Player2的分数是：" + ScoringSystem.P2Score);
                TrashDestroy();
                break;
            case "nest3":
                ScoringSystem.ChangePlayerScore(Player.p3, -10);
                Debug.Log("Player3的分数是：" + ScoringSystem.P3Score);
                TrashDestroy();
                break;
            case "nest4":
                ScoringSystem.ChangePlayerScore(Player.p4, -10);
                Debug.Log("Player4的分数是：" + ScoringSystem.P4Score);
                TrashDestroy();
                break;
        }
    }

    void Update()
    {
        whoPick = PA.curPlayerNum;
        if (transform.GetComponent<Pickable>().isPicked)
            SubstantializeProb();
    }

    void TrashDestroy()
    {
        Creator.trashes.Remove((int)(transform.position.x-0.5f));
        if(GetComponent<Pickable>().isPicked)
            GameObject.Find("Player" + PA.curPlayerNum + 1).SendMessage("PickUpPermit", "null");
        Destroy(gameObject);
    }

    private void SubstantializeProb()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
