using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float t;
    public Vector3 offset;
    public Transform Target;
    void Update()
    {
        Vector3 targetPos = Target.position;
        targetPos.z = transform.position.z;
        targetPos += offset;
        transform.position = Vector3.Lerp(transform.position, targetPos, t * 100f * Time.deltaTime);
    }
}
