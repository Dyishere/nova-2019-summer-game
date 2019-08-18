using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionDamageOfGap : MonoBehaviour
{
    public float gapDamage;

    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log(other.gameObject.name);
        PlayerHealth m_PlayerHealth = other.GetComponent<PlayerHealth>();
        GrappleMovement m_GrappleMovement = other.GetComponent<GrappleMovement>();
        if (m_PlayerHealth == null)
            return;
        if(m_GrappleMovement.isMovingToTree == false && other.gameObject.layer == 14)
        {
            m_PlayerHealth.GetRegionHurt(gapDamage);
            m_PlayerHealth.Respawn();
        }
    }

    // private int GetPlayerIDTest(string name) // 低效操作，应该引用相关组件来获取ID值
    // {
    // ???????
    //     switch (name)
    //     {
    //         case "Player1": return 1;
    //         case "Player2": return 2;
    //         case "Player3": return 3;
    //         case "Player4": return 4;
    //         default: return -1;
    //     }
    // }
}
