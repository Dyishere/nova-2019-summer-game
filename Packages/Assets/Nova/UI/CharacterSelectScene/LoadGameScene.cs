using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
    public Image ListIsEmpty;

    private SceneList list;

    void Awake()
    {
        list = GameObject.Find("SceneList").GetComponent<SceneList>();

        this.GetComponent<Button>().onClick.AddListener(delegate {
            if (IsEmpty())
            {
                ListIsEmpty.gameObject.SetActive(true);
                Invoke("DestroyImage", 1f);
            }
            else
            {
                list.AddToQueue();
                SceneManager.LoadScene("LoadScene");
            }
        });
    }

    void DestroyImage()
    {
        ListIsEmpty.gameObject.SetActive(false);
    }

    bool IsEmpty()
    {
        bool flag = true;
        foreach (var x in list.sceneList)
        {
            if (x.Flag)
            {
                flag = false;
                break;
            }
        }
        return flag;
    }
}
