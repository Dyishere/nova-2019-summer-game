using System;
using System.Collections.Generic;
using UnityEngine;

public static class LoadSceneManager
{
    public static Queue<String> SceneQueue = new Queue<string>();
    public static List<String> GameList = new List<string>();

    public static void CleanSceneQueue()
    {
        SceneQueue.Clear();
        SceneQueue.Enqueue(Scenes.StartScene.ToString());
    }

    public static void InsertSceneToQueue(string SceneNameToLoadNext)
    {
        List<string> l = new List<string>();
        l.Add(SceneNameToLoadNext);
        while (SceneQueue.Count != 0)
        {
            l.Add(SceneQueue.Dequeue());
        }
    }
}

public enum Scenes
{
    StartScene,
    CharacterSelectScene,
}