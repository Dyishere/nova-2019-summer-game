using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapCreator : MonoBehaviour
{
    public GameObject trap;
    public static Dictionary<int, GameObject> traps = new Dictionary<int, GameObject>();

    public float waitTime;
    public int trashCountInMap = 7;

    int leftPos;
    int rightPos;
    
    void Start()
    {
        leftPos = (int)(gameObject.transform.position.x - gameObject.transform.localScale.x / 2 + 1);
        rightPos = (int)(gameObject.transform.position.x + gameObject.transform.localScale.x / 2);

        InvokeRepeating("CreateTrap", waitTime, 1f);
    }

    void CreateTrap()
    {
        if(traps.Count < trashCountInMap)
        {
            int createTrapCount = 1;

            for (int i = 0; i < createTrapCount; i++) 
            {
                int trapPos = Creator.ReturnFreePos(2, leftPos, rightPos);

                if(trapPos==1000)
                {
                    break;
                }

                GameObject temp = Instantiate(trap, new Vector3(trapPos-0.5f, transform.position.y + 0.55f, 0), Quaternion.identity);
                traps.Add(trapPos, temp);
            }
        }
    }

}
