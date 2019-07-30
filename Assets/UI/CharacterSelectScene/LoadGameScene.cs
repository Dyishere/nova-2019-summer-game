using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
    private LoadSceneNext load;
    public SceneList list;

    void Awake()
    {
        list = GameObject.Find("SceneList").GetComponent<SceneList>();
        load = GameObject.Find("LoadSceneNext(Clone)").GetComponent<LoadSceneNext>();

        this.GetComponent<Button>().onClick.AddListener(delegate {
            list.AddToQueue();
            SceneManager.LoadScene("LoadScene");
        });
    }
}
