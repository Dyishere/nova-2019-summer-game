using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSCcontral : MonoBehaviour
{
    public Canvas ModeSelectCanvas;

    public GameObject OKButton;

    void Start()
    {
        ModeSelectCanvas.gameObject.SetActive(false);
    }


    public void BackToSelectPlayer()
    {
        ModeSelectCanvas.gameObject.SetActive(false);
        OKButton.GetComponent<Button>().interactable = true;
    }
}
