using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 注意：浮岛之间的空隙 与 浮岛 边缘不能靠太近，否则会有双重伤害
/// </summary>
public class IslandRegion : MonoBehaviour
{
    public bool isGap;
    public int playerID;

    public float regionDamage;
    private float getRegionDamageRate = 0.2f; // 每过x秒，触发一次区域伤害 
    private float getRegionDamageTimer = 0f;
    private void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log(other.gameObject.name);
        GrappleMovement grappleMovement = other.GetComponent<GrappleMovement>();
        if (grappleMovement == null)
            return;

        if (isGap || playerID != GetPlayerIDTest(other.gameObject.name))
        {
            getRegionDamageTimer += Time.deltaTime;
            if (getRegionDamageTimer > getRegionDamageRate)
            {
                grappleMovement.GetRegionHurt(regionDamage);
                getRegionDamageTimer = 0f;
            }
        }
    }

    private int GetPlayerIDTest(string name) // 低效操作，应该引用相关组件来获取ID值
    {
        switch (name)
        {
            case "Player1": return 1;
            case "Player2": return 2;
            case "Player3": return 3;
            case "Player4": return 4;
            default: return -1;
        }
    }
}
