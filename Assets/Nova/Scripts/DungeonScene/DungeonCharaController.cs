using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonCharaController : MonoBehaviour
{
    public float movingSpeed;
    private bool areaHurtSwitch = false;
    private bool inSafeArea = false;
    private bool areaHurting;

    //本脚本挂载在角色上，其中Moving脚本为暂时的测试脚本，需要角色控制器脚本替换。
    //判断角色是否在危险时间位于危险区域，会传出一个areaHurting的布尔值，true为正在受到危险区域伤害
    private void Start()
    {
    }

    private void Update()
    {
        if (areaHurting)
            Debug.Log("在危险区中!!!");
    }

    private void FixedUpdate()
    {
        Moving();
        AreaHurtJudgement();
    }

    private void Moving()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * movingSpeed, Input.GetAxis("Vertical") * movingSpeed, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SafeArea")
                inSafeArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "SafeArea")
            inSafeArea = false;
    }

    private void AreaHurtJudgement()
    {
        if (areaHurtSwitch && !inSafeArea)
            areaHurting = true;
        else
            areaHurting = false;
    }

    private void AreaHurtSwitch()
    {
        areaHurtSwitch = !areaHurtSwitch;
    }
}
