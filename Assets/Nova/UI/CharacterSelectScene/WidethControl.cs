using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WidethControl : MonoBehaviour
{
    private Vector2 Size = new Vector2(0f, 480f);

    void Update()
    {
        float n = this.transform.childCount * 80f;
        if (n <= 480)
            n = 480;

        Size.y = n;
        this.GetComponent<RectTransform>().sizeDelta = Size;       
    }
}
