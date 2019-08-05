using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    public static Dictionary<int, GameObject> eggs = new Dictionary<int, GameObject>();
    public static Dictionary<int, GameObject> trashes = new Dictionary<int, GameObject>();

    public GameObject egg;
    public GameObject[] trash;
    public float leftPos = -23f;
    public float rightPos = 23;
    public int trashCountInMap = 7;
    public int eggCountInMap = 6;

    int eggPos = 0;
    int trashPos = 0;
    int eggNumber = 0;
    int trashNumber = 0;

    void Start()
    {
        InvokeRepeating("EggCreate", 0f, 0.65f);
        InvokeRepeating("TrashCreate", 0.5f, 0.65f);
    }




    void TrashCreate()
    {
        int oneOrTwo = (int)Random.Range(0, 4);

        if(trashes.Count<=trashCountInMap)
        {
            SetTrash(oneOrTwo);
        }
        
    }




    void SetTrash(int oneOrTwo)
    {
        if (oneOrTwo == 3)
        {
            int trash1 = (int)Random.Range(0, 7);             //垃圾种类
            int trash2 = (int)Random.Range(0, 7);

            trashPos = ReturnFreePos(1, leftPos, rightPos);
            if(trashPos!=1000)
            {
                GameObject temp1 = Instantiate(trash[trash1], new Vector3(trashPos + 0.5f, transform.position.y, 0), Quaternion.identity);
                temp1.transform.name = "trash" + trashNumber;
                trashNumber++;
                trashes.Add(trashPos, temp1);
            }

            trashPos = ReturnFreePos(1, leftPos, rightPos);
            if(trashPos!=1000)
            {
                GameObject temp2 = Instantiate(trash[trash2], new Vector3(trashPos+0.5f, transform.position.y, 0), Quaternion.identity);
                temp2.transform.name = "trash" + trashNumber;
                trashNumber++;
                trashes.Add(trashPos, temp2);
            }
        }
        else
        {
            int whichTrash = (int)Random.Range(0, 7);

            trashPos = ReturnFreePos(1, leftPos, rightPos);
            if (trashPos != 1000)
            {
                GameObject temp = Instantiate(trash[whichTrash], new Vector3(trashPos + 0.5f, transform.position.y, 0), Quaternion.identity);
                temp.transform.name = "trash" + trashNumber;
                trashNumber++;
                trashes.Add(trashPos, temp);
            }
        }
    }



                                                             ///////////////////////////////
    void EggCreate()                                         //生成鸡蛋，并限制最大数量为6//
    {                                                        ///////////////////////////////
        if (eggs.Count==0)
        {
            eggPos = (int)Random.Range(leftPos, rightPos);

            if (eggPos != 1000)
            {
                GameObject temp = Instantiate(egg, new Vector3(eggPos + 0.5f, transform.position.y, 0), Quaternion.identity);
                temp.transform.name = "egg" + eggNumber;
                eggNumber++;
                eggs.Add(eggPos, temp);
            }
        }
        else if(eggs.Count<eggCountInMap)
        {
            eggPos = ReturnFreePos(0, leftPos, rightPos);

            if (eggPos != 1000)
            {
                GameObject temp = Instantiate(egg, new Vector3(eggPos + 0.5f, transform.position.y, 0), Quaternion.identity);
                temp.transform.name = "egg" + eggNumber;
                eggNumber++;
                eggs.Add(eggPos, temp);
            }
        }
    }



                                                                                    //////////////////////////////////////////
    public static int ReturnFreePos(int whichObject, float leftPos, float rightPos) //返回一个空闲的位置点，0是鸡蛋，1是垃圾//
    {                                                                               //////////////////////////////////////////
        int temp = (int)Random.Range(leftPos, rightPos);

        if (whichObject==0)
        {
            int count = 0;
            while (true)
            {
                if (eggs.ContainsKey(temp) == false && eggs.ContainsKey(temp + 1) == false && eggs.ContainsKey(temp - 1) == false)
                {
                    break;
                }
                else
                {
                    count++;
                    temp = (int)Random.Range(leftPos, rightPos);
                }

                if (count == 3)
                {
                    temp = 1000;
                    break;
                }
            }
        }
        else if(whichObject==1)
        {
            int count = 0;
            while (true)
            {
                if (eggs.ContainsKey(temp) == false && trashes.ContainsKey(temp) == false) 
                {
                    break;
                }
                else
                {
                    count++;
                    temp = (int)Random.Range(leftPos, rightPos);
                }

                if (count == 3)
                {
                    temp = 1000;
                    break;
                }
            }
        }
        else if(whichObject==2)
        {
            int count = 0;
            while (true)
            {
                if (eggs.ContainsKey(temp) == false && TrapCreator.traps.ContainsKey(temp) == false) 
                {
                    break;
                }
                else
                {
                    count++;
                    temp = (int)Random.Range(leftPos, rightPos);
                }

                if(count==3)
                {
                    temp = 1000;
                    break;
                }
            }
        }
        
        return temp;
    }
}
