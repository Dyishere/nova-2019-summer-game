﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public LineRenderer LR;
    public GameObject bullet;
    public GameObject BulletMaster;
    public bool IsShotting = false;
    public bool startRound = false;

    Bullet bullet_;
    private void Start()
    {
        bullet_ = BulletMaster.GetComponent<Bullet>();
        LR.SetColors(Color.black, Color.black);
        LR.SetWidth(0.1f, 0.1f);
    }

    public void Grapple(ref bool iShoot, ref bool iAim)
    {
        LR.SetPosition(0, gameObject.transform.localPosition);
        LR.SetPosition(1, bullet.transform.localPosition);

        if (iShoot && startRound == true)         //如果按下J并且已经开始旋转，那么发射子弹
        {
            IsShotting = true;
            bullet_.enAbleToCatchPlayer = true;
        }
        //如果按下K并且没有旋转，没有发射，那么旋转
        if (iAim && IsShotting == false && startRound == false)
        {
            if (bullet_.Stop == true)
            {
                bullet_.Stop = false;
            }
            if(bullet_.followPlayer == true)
            {
                bullet_.followPlayer = false;
            }

            bullet.transform.position = new Vector3(transform.position.x + 0.75f, transform.position.y, 0);

            startRound = true;
            IsShotting = false;
            bullet_.enAbleToCatchPlayer = false;
        }
        iShoot = false;
        iAim = false;
    }
}
