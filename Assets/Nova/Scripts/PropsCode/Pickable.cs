using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    private int curPlayerNum;       //用于判别当前物品被哪个玩家接触
    private bool isPicked;          //用于判定是否已被捡起
    void Start()
    {
    }

    void Update()
    {
            Picking();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isPicked)
            return;
        //允许玩家捡拾当前物品
        if (collision.gameObject.tag == "Player")
            collision.gameObject.SendMessage("PickUpPermit",gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isPicked)
            return;
        //不允许玩家进行捡拾
        if (collision.gameObject.tag == "Player")
            collision.gameObject.SendMessage("PickUpPermit", "null");
    }

    private void BeingPicked(int curNum)        //获取被哪个玩家对该物体进行了捡起放下操作
    {
        isPicked = !isPicked;
        if (isPicked)
            curPlayerNum = curNum;
        else
            curPlayerNum = 0;
    }

    private void Picking ()
    {
        if (curPlayerNum == 0 || !isPicked)
            return;
        else
        {
            string playerPicking = "Player" + curPlayerNum + "/PickPos";
            GetComponent<Rigidbody2D>().velocity = new Vector2 (0,0);
            GetComponent<Rigidbody2D>().rotation = 0;
            transform.position = GameObject.Find(playerPicking).transform.position;
        }
    }
}
