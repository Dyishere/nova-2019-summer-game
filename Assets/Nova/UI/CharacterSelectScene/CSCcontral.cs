using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CSCcontral : MonoBehaviour
{
    public Canvas CharacterSelectCanvas;

    void Start()
    {
        CharacterSelectCanvas.gameObject.SetActive(true);
    }


    void Update()
    {
        
    }

    public void Back()
    {
        SceneManager.LoadScene(Scenes.StartScene.ToString());
    }
}
