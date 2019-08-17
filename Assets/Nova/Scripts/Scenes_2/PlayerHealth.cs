using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Transform RespawnPorint;
    public float hp = 100f;
    public void GetRegionHurt(float damage)
    {
        hp -= damage;
        if (hp < 0)
        {
            hp = 0f;
            Die();
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + "死了");
    }
    public void Respawn()
    {
        transform.position = RespawnPorint.position;
    }
}
