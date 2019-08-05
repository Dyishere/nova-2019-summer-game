using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("EggDestroy", 3.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("EggDestroy", 4f);              //拾取蛋用下面注释掉的那句，不要用这句
        //EggDestroy();
    }

    void EggDestroy()
    {
        
        Creator.eggs.Remove((int)(transform.position.x-0.5f));          //先从字典里面移除，然后再删除
        Destroy(gameObject);
        
    }
}
