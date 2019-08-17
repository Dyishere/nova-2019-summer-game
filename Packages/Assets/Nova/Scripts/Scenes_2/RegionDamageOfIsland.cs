using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionDamageOfIsland : MonoBehaviour
{
    public int ownerPlayerID;
    public float regionDamage;
    private float getRegionDamageRate = 0.2f; // 每过x秒，触发一次区域伤害 
    private float getRegionDamageTimer = 0f;
    private void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log(other.gameObject.name);
        PlayerHealth m_PlayerHealth = other.GetComponent<PlayerHealth>();
        if (m_PlayerHealth == null)
            return;

        if (ownerPlayerID != GetPlayerIDTest(other.gameObject.name))
        {
            getRegionDamageTimer += Time.deltaTime;
            if (getRegionDamageTimer > getRegionDamageRate)
            {
                m_PlayerHealth.GetRegionHurt(regionDamage);
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
