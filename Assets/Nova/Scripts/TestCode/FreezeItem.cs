using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeItem : MonoBehaviour
{
    [SerializeField] private float FreezeTime = 1.5f;
    Rigidbody2D playerR2D ;

    private void OnTriggerEnter(Collider player)
    {
        playerR2D = player.GetComponent<Rigidbody2D>();
        playerR2D.constraints = RigidbodyConstraints2D.FreezePosition;
        Invoke("UnFreezed", FreezeTime);
        Destroy(gameObject);
    }

    private void UnFreezed()
    {
        playerR2D.constraints =~ RigidbodyConstraints2D.FreezePosition;
    }



}
