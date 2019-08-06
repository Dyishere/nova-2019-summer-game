using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag!="Player")
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

            Invoke("TrashDestroy", 4f);
        }      
    }

    void TrashDestroy()
    {

        Creator.trashes.Remove((int)(transform.position.x-0.5f));
        Destroy(gameObject);

    }
}
