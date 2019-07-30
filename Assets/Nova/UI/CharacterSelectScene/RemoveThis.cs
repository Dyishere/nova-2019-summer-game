using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveThis : MonoBehaviour
{
    private SceneList list;
    private int Index;

    private void Awake()
    {
        list = GameObject.Find("SceneList").GetComponent<SceneList>();
        Index = list.Count;
        list.Count = list.Count + 1;
    }

    public void Remove()
    {
        list.sceneList[Index].SetFalse();
        Destroy(this.gameObject);
    }
}
