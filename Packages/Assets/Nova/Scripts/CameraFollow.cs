using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followingSmoothTime;
    public float maxFollowingSpeed;
    public Vector3 offset;
    public float zoomingSmoothTime;
    public float minZoom;
    public float maxZoom;
    public float zoomingRatio;
    // public AnimationCurve zoomCurve;
    public float minPosX;
    public float maxPosX;
    public float minPosY;
    public float maxPosY;
    public Transform[] Targets;
    private Vector3 curFollowingVelocity;
    private Camera m_Camera;
    private void Awake()
    {
        m_Camera = GetComponent<Camera>();
    }
    void FixedUpdate()
    {
        if (Targets.Length == 0) return;

        // 跟随
        var m_bounds = new Bounds(Targets[0].position, Vector3.zero);
        for (int i = 0; i < Targets.Length; ++i)
        {
            m_bounds.Encapsulate(Targets[i].position);
        }
        Vector3 targetPos = m_bounds.center;
        targetPos += offset;

        // 地图边缘限制
        targetPos.x = Mathf.Max(minPosX, targetPos.x);
        targetPos.x = Mathf.Min(maxPosX, targetPos.x);
        targetPos.y = Mathf.Max(minPosY, targetPos.y);
        targetPos.y = Mathf.Min(maxPosY, targetPos.y);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref curFollowingVelocity, followingSmoothTime, maxFollowingSpeed);

        // 缩放
        float size = Mathf.Max(m_bounds.size.x, m_bounds.size.y);
        size = Mathf.Max(minZoom, size);
        size = Mathf.Min(size, maxZoom);
        size *= zoomingRatio;
        m_Camera.orthographicSize = Mathf.Lerp(m_Camera.orthographicSize, size, Time.deltaTime * zoomingSmoothTime);
        // zoomCurve.Evaluate((maxFollowingSpeed * maxFollowingSpeed) / curVelocity.sqrMagnitude)
    }
}
