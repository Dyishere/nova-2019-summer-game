﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneList : MonoBehaviour
{
    public List<ListItem> sceneList;

    private int _count = 0;
    public int Count
    {
        get { return _count; }
        set { _count = value; }
    }
    private void Awake()
    {
        sceneList = new List<ListItem>();
    }

    public void AddToQueue()
    {
        foreach (var x in sceneList)
        {
            if (x.Flag)
            {
                LoadSceneManager.SceneQueue.Enqueue(x.sceneName);
                LoadSceneManager.GameList.Add(x.sceneName);
            }
        }
    }
}

public class ListItem
{
    public ListItem(string name)
    {
        sceneName = name;
        Flag = true;
    }

    public string sceneName;
    public bool Flag;

    public void SetFalse()
    {
        this.Flag = false;
    }
}
