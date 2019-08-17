using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleMovement : MonoBehaviour
{

    public bool isMovingToTree = false;
    public bool isGrapplingEnemy = false;
    public int grappledEnemyID = 0;
    Vector3 Pos = new Vector3();

    public float regionDamage; // 区域伤害
    public float regionGapDamage; // 浮岛之间间隔的伤害
    private Rigidbody2D m_Rigidbody2D;
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void Move(float iMoveX, float iMoveY)
    {

        Pos.x = iMoveX;
        Pos.y = iMoveY;

        gameObject.transform.position = gameObject.transform.position + (Pos.normalized) * Time.deltaTime * 3;
        // m_Rigidbody2D.velocity = Pos.normalized * Time.deltaTime * 300f; // 魔法数字

        if (isGrapplingEnemy == true)                     //拉敌人
        {
            GameObject temp = null;

            switch (grappledEnemyID)
            {
                case 1:
                    temp = GameObject.Find("Player1");
                    break;
                case 2:
                    temp = GameObject.Find("Player2");
                    break;
                case 3:
                    temp = GameObject.Find("Player3");
                    break;
                case 4:
                    temp = GameObject.Find("Player4");
                    break;
            }
            if (temp != null)
            {
                float dis = (transform.position - temp.transform.position).sqrMagnitude;
                if (dis <= 6f)
                {
                    temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    temp.transform.position = gameObject.transform.position + (temp.transform.position - gameObject.transform.position).normalized * 3;
                    isGrapplingEnemy = false;
                    grappledEnemyID = 0;
                    temp = null;
                }
                else
                {
                    temp.GetComponent<Rigidbody2D>().velocity = (gameObject.transform.position - temp.transform.position).normalized * 20f;
                }
            }
        }

        if (isMovingToTree == true)                      //把自己往树拉
        {
            GameObject temp = GameObject.FindGameObjectWithTag("Tree");
            gameObject.GetComponent<Rigidbody2D>().velocity = (temp.transform.position - gameObject.transform.position).normalized * 10f;
        }
    }
}
