using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    //本脚本为安全区域刷新的时间控制脚本
    //暂时测试为只存在一位角色的场景，待角色输入区别做好后再扩展。

    public float DangerTime;        //危险区域存在时间
    public float SafeTime;          //危险区域间隔的时间

    private bool AreaSwitch = false;
    private Color greenBack = Color.green;
    private Color redBack =Color.red;


    // Update is called once per frame
    private void Update()
    {
        //测试用代码，只需按o触发一次即可。
        if (Input.GetKeyDown(KeyCode.O))
            AreaSwitch = !AreaSwitch;       

        if (AreaSwitch)
        {
            AreaSwitch = false;
            StartCoroutine(MainController());
        }
    }

    IEnumerator MainController()
    {
        StartCoroutine(DangerAreaSwitch());
        yield return new WaitForSecondsRealtime(DangerTime + SafeTime);
        StartCoroutine(MainController());
    }

    IEnumerator DangerAreaSwitch()
    {
        DangerSwicthOn();
        yield return new WaitForSecondsRealtime(DangerTime);
        DangerSwicthOff();
    }

    private void DangerSwicthOn()
    {
        GameObject.Find("Player1").SendMessage("AreaHurtSwitch");
        transform.GetComponent<Camera>().backgroundColor = redBack;

    }

    private void DangerSwicthOff()
    {
        GameObject.Find("Player1").SendMessage("AreaHurtSwitch");
        transform.GetComponent<Camera>().backgroundColor = greenBack;
    }
}
