using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenModeSelect : MonoBehaviour
{
    public bool p1;
    public bool p2;
    public bool p3;
    public bool p4;

    public Canvas ModeCanvas;
    public Image NotPrepare;

    public GameObject PrepareButton1;
    public GameObject PrepareButton2;
    public GameObject PrepareButton3;
    public GameObject PrepareButton4;

    // Start is called before the first frame update
    void Start()
    {
        NotPrepare.gameObject.SetActive(false);
        this.GetComponent<Button>().onClick.AddListener(delegate { SelectMode(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SelectMode()
    {
        if (p1 && p2 && p3 && p4)
        {
            ModeCanvas.gameObject.SetActive(true);
            this.gameObject.SetActive(false);

            PrepareButton1.GetComponent<Button>().interactable = false;
            PrepareButton2.GetComponent<Button>().interactable = false;
            PrepareButton3.GetComponent<Button>().interactable = false;
            PrepareButton4.GetComponent<Button>().interactable = false;
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

    public void SetBoolByName(string boolName, bool b)
    {
        switch (boolName)
        {
            case "p1":
                p1 = b;
                break;
            case "p2":
                p2 = b;
                break;
            case "p3":
                p3 = b;
                break;
            case "p4":
                p4 = b;
                break;
        }
    }
}
