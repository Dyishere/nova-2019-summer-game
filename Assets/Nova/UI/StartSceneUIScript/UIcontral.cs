using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIcontral : MonoBehaviour
{
    public Button SettingButton;
    public Button ProducerButton;
    public Button CloseButton;
    public Button StartButton;

    public Image FadeText;
    public Canvas StartCanvas;
    public Canvas SettingCanvas;
    public Canvas ProducerCanvas;
    public Canvas QuitCanvas;

    private bool canShow = true;

    void Start()
    {
        FadeText.gameObject.SetActive(true);
        StartCanvas.gameObject.SetActive(false);
        SettingCanvas.gameObject.SetActive(false);
        ProducerCanvas.gameObject.SetActive(false);
        QuitCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.anyKey)
        {
            FadeText.gameObject.SetActive(false);
            StartCanvas.gameObject.SetActive(true);
        }


        // Show setting canvas
        if (Input.GetKey(KeyCode.Escape) && canShow)
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

        //SettingButton.interactable = SettingButton.interactable ? false : true;
        ProducerButton.interactable = ProducerButton.interactable ? false : true;
        CloseButton.interactable = CloseButton.interactable ? false : true;
    }

    // Method for show producer
    private bool ProducerisShow { get { return ProducerCanvas.gameObject.activeSelf; } }
    public void ShowProducer()
    {
        ProducerCanvas.gameObject.SetActive(ProducerisShow ? false : true);
        if (SettingCanvas.gameObject.activeSelf)
            SettingCanvas.gameObject.SetActive(false);

        SettingButton.interactable = SettingButton.interactable ? false : true;
        //ProducerButton.interactable = ProducerButton.interactable ? false : true;
        CloseButton.interactable = CloseButton.interactable ? false : true;
    }

    // Method for quit
    public void ComfirmQuit()
    {
        QuitCanvas.gameObject.SetActive(QuitCanvas.gameObject.activeSelf ? false : true);

        SettingButton.interactable = SettingButton.interactable ? false : true;
        ProducerButton.interactable = ProducerButton.interactable ? false : true;
        //CloseButton.interactable = CloseButton.interactable ? false : true;
    }

    // Method in quit canvas
    public void Quit()
    {
        Application.Quit();
    }

}
