using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EditText : MonoBehaviour
{

    private GameObject inputField;

    private TextMeshProUGUI parentText;

    PhotonView pView;

    private bool isBeingEdited;
    //private bool firstIn;


    void Start()
    {
        pView = GetComponent<PhotonView>();
        inputField = GameObject.Find("Canvas").transform.Find("Arena").Find("EditInputField").gameObject;
        GetComponent<RectTransform>().sizeDelta = transform.parent.GetComponent<RectTransform>().sizeDelta;
        parentText = transform.parent.GetComponent<TextMeshProUGUI>();
        transform.localPosition = Vector3.zero;
        isBeingEdited = false;
        //firstIn = false;
    }

    void Update()
    {
        CloseInputField(); 
    }

    public void Edit()
    {
        if (!inputField.activeInHierarchy && IsMine())
        {
            inputField.transform.Find("Text Area").Find("Placeholder").GetComponent<TextMeshProUGUI>().text = parentText.text;
            isBeingEdited = true;
            inputField.SetActive(true);
        }  
    }

    void CloseInputField()
    {
        if (isBeingEdited && Input.GetButtonDown("Submit"))
        {
            ChangeTextForOther(inputField.GetComponent<TMP_InputField>().text);
            inputField.SetActive(false);
            inputField.GetComponent<TMP_InputField>().text = "";
            isBeingEdited = false;
        }
    }

    public void ChangeTextForOther(string s)
    {
        pView.RPC("ChangeText", PhotonTargets.AllBuffered, s);
    }

    bool IsMine()
    {
        return pView.owner == PhotonNetwork.player;
    }

    [PunRPC]
    private void ChangeText(string text)
    {
        parentText.text = text;
    }

    private void OnDestroy()
    {
        if (inputField)
        {
            if (inputField.activeInHierarchy && isBeingEdited)
            {
                inputField.SetActive(false);
            }
        }
    }
}
