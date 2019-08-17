using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowWiner : MonoBehaviour
{
    public GameObject winner;
    public GameObject[] slider;
    public Text text;
    public GameObject continueButton;

    private bool[] done = new bool[4];
    // Start is called before the first frame update
    void Start()
    {
        winner.SetActive(false);
        continueButton.SetActive(false);
        ScoringSystom.Init();
    }

    // Update is called once per frame
    void Update()
    {
        GetBool();
        if (Done())
        {
            CreateText();
            winner.SetActive(true);
            continueButton.SetActive(true);
        }
    }

    public void Continue()
    {
        SceneManager.LoadScene("StartScene");
    }

    private bool Done()
    {
        bool flag = true;
        foreach (var x in done)
            if (!x)
            {
                flag = false;
                break;
            }
        return flag;
    }

    private void GetBool()
    {
        for (int i = 0; i < 4; i++)
            done[i] = slider[i].GetComponent<SliderControl>().done;
    }

    private void CreateText()
    {
        text.text = "Winner is ";
        foreach (var x in slider)
        {
            if (x.GetComponent<SliderControl>().isWinner)
                text.text += x.GetComponent<SliderControl>().player.ToString() + " ";
        }
    }
}
