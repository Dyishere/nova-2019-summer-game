using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonFoundation : MonoBehaviour
{
    public bool isMatch = false;
    private bool sendPermit = true;
    public string myColor;


    // Start is called before the first frame update
    void Start()
    {
        CheckCurColor();
        RandomPositionInitialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag != "MissionProp")
            return;
        else if (!collision.GetComponent<Pickable>().isPicked && collision.GetComponent<DungeonEgg>().curColor == myColor)
        {
            isMatch = true;
            collision.transform.position = transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.tag != "MissionProp")
            return;
        else if (!collision.GetComponent<Pickable>().isPicked && collision.GetComponent<DungeonEgg>().curColor == myColor)
            isMatch = false;
    }

    private void CheckCurColor()
    {
        foreach (char c in gameObject.name)
            if (c == 'F')
                return;
            else
                myColor += c;
    }

    private void RandomPositionInitialize()
    {
        bool done = false;
        int step = Random.Range(1, 4);
        int i = step;
        for (; ; )
        {
            switch(i)
            {
                case 1:
                    done = GameObject.Find("redEgg").GetComponent<DungeonEgg>().InitializeCheck(myColor);
                    break;
                case 2:
                    done = GameObject.Find("yellowEgg").GetComponent<DungeonEgg>().InitializeCheck(myColor);
                    break;
                case 3:
                    done = GameObject.Find("blueEgg").GetComponent<DungeonEgg>().InitializeCheck(myColor);
                    break;
                case 4:
                    done = GameObject.Find("greenEgg").GetComponent<DungeonEgg>().InitializeCheck(myColor);
                    i = 0;
                    break;
            }
            if (done)
                break;
            i++;
        }

        
    }

}
