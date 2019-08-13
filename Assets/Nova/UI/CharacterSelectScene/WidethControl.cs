using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidethControl : MonoBehaviour
{
    private Vector2 Size = new Vector2(0f, 480f);

    void Update()
    {
        float n = this.transform.childCount * 77.5f;
        if (n <= 542.5)
            n = 542.5f;

        Size.y = n;
        this.GetComponent<RectTransform>().sizeDelta = Size;       
    }
}
