using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrepareScript : MonoBehaviour
{
    public string boolName;
    public GameObject ReadyImage;
    public GameObject NotReadyImage;

    private OpenModeSelect modeSelect;
    private void Awake()
    {
        modeSelect = GameObject.Find("OK").GetComponent<OpenModeSelect>();
    }

    void Start()
    {
        ReadyImage.SetActive(false);
        NotReadyImage.SetActive(true);

        this.GetComponent<Button>().onClick.AddListener(delegate { ChangeStatus(); });
    }

    void Update()
    {
        
    }

    private void ChangeStatus()
    {
        ReadyImage.SetActive(ReadyImage.activeSelf ? false : true);
        NotReadyImage.SetActive(NotReadyImage.activeSelf ? false : true);
        //modeSelect.SetBoolByName(boolName, ReadyImage.activeSelf ? true : false);
    }
}
