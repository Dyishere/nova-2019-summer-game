using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ListControl : MonoBehaviour
{
    public GameObject List;
    public GameObject[] PlayerSlider;
    public GameObject PressToContinue;
    public String[] SceneName;
    public GameObject[] Prefabs;

    private List<String> sceneList;
    private GameObject firstItem;
    private bool[] flags = new bool[4];
    private bool del = false;

    void Start()
    {
        sceneList = LoadSceneManager.GameList;
        firstItem = null;
        PressToContinue.SetActive(false);
        CreateList();
    }


    void Update()
    {
        GetBool();
        if (Done() && !del)
        {
            Destroy(firstItem);
            PressToContinue.SetActive(true);
            LoadSceneManager.GameList.RemoveAt(0);
            ScoringSystem.ResetScore();
            del = true;
        }
    }

    public void ContinuE()
    {
        SceneManager.LoadScene("LoadScene");
    }



    // 辅助方法
    void GetBool()
    {
        for (int i = 0; i < 4; i++)
            flags[i] = PlayerSlider[i].GetComponent<SliderChange>().done;
    }

    bool Done()
    {
        bool flag = true;
        foreach (var x in flags)
        {
            if (!x)
            {
                flag = false;
                break;
            }
        }
        return flag;
    }

    void CreateList()
    {
        foreach (var x in sceneList)
        {
            if (firstItem == null)
                firstItem = AddToList(x);
            else
                AddToList(x);

            var s = new WaitForSecondsRealtime(0.1f);
        }
    }

    GameObject AddToList(string s)
    {
        GameObject gameObject = Instantiate(Prefabs[RetIndex(s)]);
        gameObject.transform.SetParent(List.transform);
        return gameObject;
    }

    int RetIndex(string s)
    {
        int index = 0;
        for (int i = 0; i < 3; i++)
        {
            if (s.Equals(SceneName[i]))
            {
                index = i;
                break;
            }
        }
        return index;
    }
}
