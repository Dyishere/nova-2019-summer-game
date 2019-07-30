using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSCcontral : MonoBehaviour
{
    public Canvas ModeSelectCanvas;

    public GameObject OKButton;

    public GameObject PrepareButton1;
    public GameObject PrepareButton2;
    public GameObject PrepareButton3;
    public GameObject PrepareButton4;

    void Start()
    {
        ModeSelectCanvas.gameObject.SetActive(false);
    }


    public void BackToSelectPlayer()
    {
        ModeSelectCanvas.gameObject.SetActive(false);
        OKButton.SetActive(true);

        PrepareButton1.GetComponent<Button>().interactable = true;
        PrepareButton2.GetComponent<Button>().interactable = true;
        PrepareButton3.GetComponent<Button>().interactable = true;
        PrepareButton4.GetComponent<Button>().interactable = true;
    }
}
