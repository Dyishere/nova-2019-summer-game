using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Transform RespawnPoint;
    public float hp = 100f;
    private GrappleMovement m_grappleMovement;
    private void Awake()
    {
        m_grappleMovement = GetComponent<GrappleMovement>();
    }
    public void GetRegionHurt(float damage)
    {
        if (m_grappleMovement.isMovingToTree) return;

        hp -= damage;
        Debug.Log(gameObject.name + " now Hp is" + hp);
        if (hp < 0)
        {
            hp = 0f;
            Die();
            //Respawn();
        }
    }

    public void Die()
    {
        Debug.Log(gameObject.name + "死了");
    }
    public void Respawn()
    {
        transform.position = RespawnPoint.position;
    }
}
