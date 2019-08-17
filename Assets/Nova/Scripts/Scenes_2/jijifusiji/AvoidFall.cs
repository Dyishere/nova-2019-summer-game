using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidFall : MonoBehaviour
{
    GameObject player;

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        if(collision.gameObject.GetComponent<GrappleMovement>().isMovingToTree == false)
        {
            player = collision.gameObject;
            collision.gameObject.GetComponent<GrappleMovement>().stopMoving = true;
            Invoke("resetPlayer", 0.5f);
        }
    }

    void resetPlayer()
    {
        player.transform.position = new Vector3(-6.5f, 1.9f, 0);
        player.GetComponent<GrappleMovement>().stopMoving = false;
    }
}
