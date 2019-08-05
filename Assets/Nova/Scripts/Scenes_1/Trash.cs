using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("TrashDestroy", 4f);
        //EggDestroy();
    }

    void TrashDestroy()
    {

        Creator.trashes.Remove((int)(transform.position.x-0.5f));
        Destroy(gameObject);

    }
}
