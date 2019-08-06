using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    int whoPick=0;
    
    void Update()
    {
        if (transform.GetComponent<Pickable>().isPicked)
            SubstantializeProb();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            gameObject.GetComponent<CapsuleCollider2D>().isTrigger = true;
            //Invoke("EggDestroy", 6f);     暂时不需要碰撞六秒后销毁，故先注释掉。
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(whoPick!=0)
        {
            switch(whoPick)
            {
                case 1:
                    EggScoreResult(collision.gameObject.name, 1);
                    break;
                case 2:
                    EggScoreResult(collision.gameObject.name, 2);
                    break;
                case 3:
                    EggScoreResult(collision.gameObject.name, 3);
                    break;
                case 4:
                    EggScoreResult(collision.gameObject.name, 4);
                    break;
            }            
        }
    }

    private void EggScoreResult(string nestName, int player)
    {
        if(nestName==("nest"+player))
        {
            switch(player)
            {
                case 1:
                    ScoringSystom.ChangePlayerScore(Player.p1, 40);
                    Debug.Log("Player1的分数是：" + ScoringSystom.P1Score);
                    EggDestroy();
                    break;
                case 2:
                    ScoringSystom.ChangePlayerScore(Player.p2, 40);
                    Debug.Log("Player2的分数是：" + ScoringSystom.P2Score);
                    EggDestroy();
                    break;
                case 3:
                    ScoringSystom.ChangePlayerScore(Player.p3, 40);
                    Debug.Log("Player3的分数是：" + ScoringSystom.P3Score);
                    EggDestroy();
                    break;
                case 4:
                    ScoringSystom.ChangePlayerScore(Player.p4, 40);
                    Debug.Log("Player4的分数是：" + ScoringSystom.P4Score);
                    EggDestroy();
                    break;
            }
        }
        else
        {
            switch(nestName)
            {
                case "nest1":
                    ScoringSystom.ChangePlayerScore(Player.p1, -25);
                    Debug.Log("Player1的分数是：" + ScoringSystom.P1Score);
                    EggDestroy();
                    break;
                case "nest2":
                    ScoringSystom.ChangePlayerScore(Player.p2, -25);
                    Debug.Log("Player2的分数是：" + ScoringSystom.P2Score);
                    EggDestroy();
                    break;
                case "nest3":
                    ScoringSystom.ChangePlayerScore(Player.p3, -25);
                    Debug.Log("Player3的分数是：" + ScoringSystom.P3Score);
                    EggDestroy();
                    break;
                case "nest4":
                    ScoringSystom.ChangePlayerScore(Player.p4, -25);
                    Debug.Log("Player4的分数是：" + ScoringSystom.P4Score);
                    EggDestroy();
                    break;
            }
        }
    }

    void EggDestroy()
    {
        Creator.eggs.Remove((int)(transform.position.x - 0.5f));          //先从字典里面移除，然后再删除
        Destroy(gameObject);
    }

    private void SubstantializeProb()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = ~RigidbodyConstraints2D.FreezePositionY;
        gameObject.GetComponent<CapsuleCollider2D>().isTrigger = false;
        whoPick = Pickable.curPlayerNum;
    }
}
