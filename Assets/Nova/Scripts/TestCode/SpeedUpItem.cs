using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpItem : MonoBehaviour
{
    [Header("道具属性修改")]
    [SerializeField] private float Duration;
    [SerializeField] private float SpeedScale;

    private void OnTriggerEnter2D(Collider2D Player)
    {
        
    }


}
