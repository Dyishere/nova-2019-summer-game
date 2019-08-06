using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    int whoPick = 0;

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
                    ScoringSystom.ChangePlayerScore(Player.p1, -10);
                    Debug.Log("Player1的分数是：" + ScoringSystom.P1Score);
                    TrashDestroy();
                    break;
                case "Player2":
                    ScoringSystom.ChangePlayerScore(Player.p1, -10);
                    Debug.Log("Player2的分数是：" + ScoringSystom.P2Score);
                    TrashDestroy();
                    break;
                case "Player3":
                    ScoringSystom.ChangePlayerScore(Player.p1, -10);
                    Debug.Log("Player3的分数是：" + ScoringSystom.P3Score);
                    TrashDestroy();
                    break;
                case "Player4":
                    ScoringSystom.ChangePlayerScore(Player.p1, -10);
                    Debug.Log("Player4的分数是：" + ScoringSystom.P4Score);
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
                ScoringSystom.ChangePlayerScore(Player.p1, -10);
                Debug.Log("Player1的分数是：" + ScoringSystom.P1Score);
                TrashDestroy();
                break;
            case "nest2":
                ScoringSystom.ChangePlayerScore(Player.p1, -10);
                Debug.Log("Player2的分数是：" + ScoringSystom.P2Score);
                TrashDestroy();
                break;
            case "nest3":
                ScoringSystom.ChangePlayerScore(Player.p1, -10);
                Debug.Log("Player3的分数是：" + ScoringSystom.P3Score);
                TrashDestroy();
                break;
            case "nest4":
                ScoringSystom.ChangePlayerScore(Player.p1, -10);
                Debug.Log("Player4的分数是：" + ScoringSystom.P4Score);
                TrashDestroy();
                break;
        }
    }

    void Update()
    {
        whoPick = Pickable.curPlayerNum;    
    }

    void TrashDestroy()
    {
        Creator.trashes.Remove((int)(transform.position.x-0.5f));
        Destroy(gameObject);
    }
}
