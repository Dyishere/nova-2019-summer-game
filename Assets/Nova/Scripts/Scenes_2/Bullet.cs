using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject MoveMaster;
    public GameObject LineMaster;

    public bool Stop = false;                   //停止子弹
    public bool enAbleToCatchPlayer = false;    //判断是否可以捕捉玩家（旋转时不捕捉）
    public float bulletSpeed = 20;              //子弹速度
    public float maxShotTime = 0.5f;            //发射出去最长的停留时间
    public int bulletRotateAngle = 2;           //子弹旋转角（旋转速度）
    public float mixBulletLength = 2f;
    public bool followPlayer = true;
    public int nowLayer = 17;


    bool nowIsReturnTime = false;               //是否停止转动
    float startTime;                            //计发射一段时间后子弹返回
    float endTime;
    GrappleMovement move;
    Line line;
    Vector3 shotVelocity = new Vector3();

    private void Start()
    {
        move = MoveMaster.GetComponent<GrappleMovement>();
        line = LineMaster.GetComponent<Line>();
    }

    void Update()
    {
        if(followPlayer == true && gameObject.layer == 18)
        {
            //gameObject.layer = 18;
            gameObject.transform.position = MoveMaster.gameObject.transform.position;
        }
        if (Stop == false)                                      //如果子弹不停止
        {
            if(followPlayer == false)
            {
                gameObject.layer = nowLayer;
                if (line.IsShotting == false)                    //如果发射是 否，那么旋转，并更新开始时间，等到发射的时候就是
                {                                                //刚好发射的时间
                    GameObject temp = MoveMaster;
                    gameObject.transform.RotateAround(temp.transform.position, new Vector3(0, 0, 1), bulletRotateAngle);
                    startTime = Time.time;
                }
                else if (nowIsReturnTime == false)               //如果发射是 是，并且还没到返回时间
                {
                    GameObject temp = MoveMaster;                //给子弹速度
                    shotVelocity = (gameObject.transform.position - temp.transform.position).normalized * bulletSpeed;
                    gameObject.GetComponent<Rigidbody2D>().velocity = shotVelocity;
                    endTime = Time.time;                         //记录时间，如果到达最大返回时间，返回子弹
                    if (endTime - startTime >= maxShotTime)
                    {
                        nowIsReturnTime = true;
                    }
                }
                else                                            //返回子弹
                {
                    GameObject temp = MoveMaster;
                    shotVelocity = (gameObject.transform.position - temp.transform.position).normalized * bulletSpeed;
                    GetComponent<Rigidbody2D>().velocity = -1 * shotVelocity;

                    //如果当距离小于最小距离，那么回收子弹并且重置变量
                    float dis = (gameObject.transform.position - temp.transform.position).sqrMagnitude;
                    if (dis <= mixBulletLength)
                    {
                        gameObject.layer = 18;
                        gameObject.transform.position = temp.transform.position;
                        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        followPlayer = true;
                        Stop = true;
                        nowIsReturnTime = false;
                        line.IsShotting = false;
                        line.startRound = false;
                    }

                }
            }
            else
            {
                gameObject.layer = 18;
                gameObject.transform.position = MoveMaster.gameObject.transform.position;
            }
        }
        else if (move.isMovingToTree == true)                      //如果子弹不能移动，并且勾到了树
        {                                                 //子弹静止
            
            GameObject temp = MoveMaster;                 //在人到达和子弹最小距离时重置变量回收子弹
            float dis = (transform.position - temp.transform.position).sqrMagnitude;
            if (dis <= mixBulletLength)
            {
                gameObject.layer = 18;
                MoveMaster.gameObject.layer = 14;
                temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                move.stopMoving = false;
                move.isMovingToTree = false;
                gameObject.transform.position = temp.transform.position;
                followPlayer = true;
                Stop = true;
                nowIsReturnTime = false;
                line.IsShotting = false;
                line.startRound = false;

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && enAbleToCatchPlayer == true && collision.name != MoveMaster.name)                     //如果捕捉到玩家，标记玩家
        {
            switch (collision.name)
            {
                case "Player1": move.grappledEnemyID = 1; break;
                case "Player2": move.grappledEnemyID = 2; break;
                case "Player3": move.grappledEnemyID = 3; break;
                case "Player4": move.grappledEnemyID = 4; break;
            }
            move.isGrapplingEnemy = true;
            nowIsReturnTime = true;
        }
        if (collision.tag == "CatchPoint" && (collision.name == "CatchPoint" || ("Bullet" + collision.name == gameObject.name))) 
        {
            GameObject tree = collision.gameObject;
            move.catchPoint = tree;//寻找目标树
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);   //子弹停止并移动到树的位置
            gameObject.transform.position = tree.transform.position;
            move.isMovingToTree = true;                                                    //让人物移动到树那里

            followPlayer = true;
            Stop = true;                                                           //子弹停止移动，转动，重置子弹的发射和捕获
            nowIsReturnTime = true;
            line.IsShotting = true;
            enAbleToCatchPlayer = true;

        }

    }
}
