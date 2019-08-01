using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image text;

    private Color color;
    private bool UpOrDown = true;
    // Start is called before the first frame update
    void Start()
    {
        color = new Color(1, 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (UpOrDown)
            color.a -= Time.deltaTime;
        else if (!UpOrDown)
            color.a += Time.deltaTime;
        if (color.a <= 0)
        {
            color.a = 0;
            UpOrDown = false;
        }
        else if (color.a >= 1)
        {
            color.a = 1;
            UpOrDown = true;
        }
        text.color = color;
    }
}
