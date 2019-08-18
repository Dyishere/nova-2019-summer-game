using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.transform.GetChild(1).gameObject.layer = 8;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.GetChild(1).gameObject.layer = 15;
        }
    }
}
