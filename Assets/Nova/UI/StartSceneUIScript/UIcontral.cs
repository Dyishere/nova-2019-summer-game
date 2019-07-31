using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIcontral : MonoBehaviour
{
    public Text FadeText;
    public Canvas StartCanvas;
    public Canvas SettingCanvas;
    public Canvas ProducerCanvas;

    private bool canShow = true;
    // Start is called before the first frame update
    void Start()
    {
        FadeText.gameObject.SetActive(true);
        StartCanvas.gameObject.SetActive(false);
        SettingCanvas.gameObject.SetActive(false);
        ProducerCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            FadeText.gameObject.SetActive(false);
            StartCanvas.gameObject.SetActive(true);
        }


        // Show setting canvas
        if (Input.GetKey(KeyCode.Escape) && !FadeText.gameObject.activeSelf && canShow)
        {
            SettingCanvas.gameObject.SetActive((SettingCanvas.gameObject.activeSelf ? false : true));
            if (ProducerCanvas.gameObject.activeSelf)
                ProducerCanvas.gameObject.SetActive(false);
            canShow = false;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
            canShow = true;
    }

    // UGUI Method
    // Method for start button
    public void ChangeScene()
    {
        SceneManager.LoadScene(Scenes.CharacterSelectScene.ToString());
    }

    // Method for show setting
    private bool SettingisShow { get { return SettingCanvas.gameObject.activeSelf; } }
    public void ShowSetting()
    {
        SettingCanvas.gameObject.SetActive(SettingisShow ? false : true);
        if (ProducerCanvas.gameObject.activeSelf)
            ProducerCanvas.gameObject.SetActive(false);
    }

    // Method for show producer
    private bool ProducerisShow { get { return ProducerCanvas.gameObject.activeSelf; } }
    public void ShowProducer()
    {
        ProducerCanvas.gameObject.SetActive(ProducerisShow ? false : true);
        if (SettingCanvas.gameObject.activeSelf)
            SettingCanvas.gameObject.SetActive(false);
    }

    // Method for quit
    public void Quit()
    {
        Application.Quit();
    }
}
