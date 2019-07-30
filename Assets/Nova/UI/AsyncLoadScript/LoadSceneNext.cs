using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneNext : MonoBehaviour
{
    public Queue<string> q;

    private void Awake()
    {
        q = new Queue<string>();
    }

    private void Start()
    {
        Object.DontDestroyOnLoad(this);
    }

    public void CleanSceneList()
    {
        q.Clear();
        q.Enqueue("StartScene");
    }
}
