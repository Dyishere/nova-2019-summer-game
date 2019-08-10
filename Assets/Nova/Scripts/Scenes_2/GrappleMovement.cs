using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleMovement : MonoBehaviour
{
    public bool IsTree = false;
    public bool IsPlayer = false;
    public int catchPlayer = 0;
    Vector3 Pos = new Vector3();

    public Line m_Line;

    private void FixedUpdate()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    private void Move(float iMoveX, float iMoveY)
    {
        //移动改这里

        Pos.x = iMoveX;
        Pos.y = iMoveY;

        gameObject.transform.position = gameObject.transform.position + (Pos.normalized) * Time.deltaTime * 3;

        //移动改这里


        if (IsPlayer == true)                     //拉敌人
        {
            GameObject temp = null;

            switch (catchPlayer)
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
                    IsPlayer = false;
                    catchPlayer = 0;
                    temp = null;
                }
                else
                {
                    temp.GetComponent<Rigidbody2D>().velocity = (gameObject.transform.position - temp.transform.position).normalized * 20f;
                }
            }
        }

        if (IsTree == true)                      //把自己往树拉
        {
            GameObject temp = GameObject.FindGameObjectWithTag("Tree");
            gameObject.GetComponent<Rigidbody2D>().velocity = (temp.transform.position - gameObject.transform.position).normalized * 10f;
        }
    }
}
