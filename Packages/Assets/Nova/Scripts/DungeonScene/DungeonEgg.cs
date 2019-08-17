using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEgg : MonoBehaviour
{
    public string curColor;
    private bool initiallize = false;

    // Start is called before the first frame update
    private void Awake()
    {
        CheckCurColor();
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CheckCurColor()
    {
        foreach (char c in gameObject.name)
            if (c == 'E')
                return;
            else
                curColor += c;
    }

    public bool InitializeCheck(string foundationColor)
    {
        if (initiallize || curColor == foundationColor)
            return false;
        else
        {
            transform.position = GameObject.Find(foundationColor + "Foundation").transform.position;
            initiallize = true;
            return true;
        }
    }
}
