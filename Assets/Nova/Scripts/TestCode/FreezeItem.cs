using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeItem : MonoBehaviour
{
    [SerializeField] private float FreezeTime = 1.5f;
    Rigidbody2D playerR2D ;

    private void OnTriggerEnter2D (Collider2D player)
    {
        playerR2D = player.GetComponent<Rigidbody2D>();
        playerR2D.constraints = RigidbodyConstraints2D.FreezePosition;
        Invoke("UnFreezed", FreezeTime);
        
    }

    private void UnFreezed()
    {
        playerR2D.constraints =~ RigidbodyConstraints2D.FreezePosition;
        Destroy(gameObject);
    }



}
