using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    void FixedUpdate()
    {
        gameObject.transform.RotateAround(new Vector3(0, -0.5f, 0), new Vector3(0, 0, 1), 0.5f);
    }
}
