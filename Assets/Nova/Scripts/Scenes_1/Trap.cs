using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void Start()
    {
        
   
        Invoke("TrapDestroy", 4f);
        //TrapDestroy();
    }

    void TrapDestroy()
    {

        TrapCreator.traps.Remove((int)(transform.position.x+0.5f));
        Destroy(gameObject);

    }
}
