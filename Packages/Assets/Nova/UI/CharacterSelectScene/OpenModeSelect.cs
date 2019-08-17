using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenModeSelect : MonoBehaviour
{
    private bool[] verified = { false, false, false, false };
    public Canvas ModeCanvas;
    public Image NotPrepare;

    private CharSelect p3charSelect;
    private CharSelect p4charSelect;

    void Start()
    {
        p3charSelect = GameObject.Find("P3Select").GetComponent<CharSelect>();
        p4charSelect = GameObject.Find("P4Select").GetComponent<CharSelect>();
        for (int i = 0; i < 4; i++)
        {
            verified[i] = false;
        }

        NotPrepare.gameObject.SetActive(false);
        this.GetComponent<Button>().onClick.AddListener(delegate { SelectMode(); });
    }

    void Update()
    {
        verified[0] = GameObject.Find("P1Select").GetComponent<CharSelect>().Verifyed;
        verified[1] = GameObject.Find("P2Select").GetComponent<CharSelect>().Verifyed;
        verified[2] = p3charSelect.Verifyed;
        verified[3] = p4charSelect.Verifyed;
    }

    void SelectMode()
    {
        if (verified[0] && verified[1] && (verified[2] || p3charSelect.unlock) && (verified[3] || p4charSelect.unlock))
        {
            ModeCanvas.gameObject.SetActive(true);
            this.GetComponent<Button>().interactable = false;
        }
        else
        {
            NotPrepare.gameObject.SetActive(true);
            Invoke("DestroyImage", 1f);
        }
    }

    void DestroyImage()
    {
        NotPrepare.gameObject.SetActive(false);
    }

    public void SetBoolByEnum(Player p, bool b)
    {
        verified[(int)p] = true;
    }
}
