using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            this.gameObject.transform.Translate(Vector3.left * Time.deltaTime*8f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.gameObject.transform.Translate(Vector3.up * Time.deltaTime*-8f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.gameObject.transform.Translate(Vector3.right * Time.deltaTime*8f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.gameObject.transform.Translate(Vector3.up * Time.deltaTime*8);
            
        }
    }
}
