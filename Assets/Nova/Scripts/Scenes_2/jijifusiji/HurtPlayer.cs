using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.name != "Player" + gameObject.name) 
        {
            Debug.Log(collision.gameObject.name + "受到伤害");
        }
    }
}
