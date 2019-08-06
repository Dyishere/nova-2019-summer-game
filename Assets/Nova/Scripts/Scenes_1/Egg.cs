using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    int whoPick;
    // Start is called before the first frame update
    void Start()
    {

    }


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
        if (collision.gameObject.name == "nest1" && whoPick==1)
        {
            ScoringSystom.ChangePlayerScore(Player.p1, 5);
            Debug.Log("Player1的分数是：" + ScoringSystom.P1Score);
            EggDestroy();            
        }
        if (collision.gameObject.name == "nest2" && whoPick == 1)
        {
            ScoringSystom.ChangePlayerScore(Player.p2, -5);
            Debug.Log("Player2的分数是：" + ScoringSystom.P2Score);
            EggDestroy();
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
