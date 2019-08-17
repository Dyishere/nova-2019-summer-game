using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    public string AddString;
    public GameObject Prefab;
    private SceneList list;
    private GameObject Panel;

    private void Awake()
    {
        list = GameObject.Find("SceneList").GetComponent<SceneList>();
        Panel = GameObject.Find("ItemPanel");
    }
    
    public void Add()
    {
        (Instantiate(Prefab) as GameObject).transform.SetParent(Panel.transform);
        list.sceneList.Add(new ListItem(AddString));
    }
}
