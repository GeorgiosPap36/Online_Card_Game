using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatScript : MonoBehaviour
{

    public AudioList audioList;
    private GameObject audioManager;

    public GameObject content;
    public TMP_InputField chatInput;

    private ChatForArena chatArena;

    [SerializeField]
    private List<string> textMessages;
    private PhotonView pView;



    void OnEnable()
    {
        pView = GetComponent<PhotonView>();
        audioManager = GameObject.Find("AudioManager");
        if (FindIfIAmInArena())
        {
            chatArena = GameObject.Find("Canvas").transform.Find("Arena").Find("ProductiveCriticismButton").GetComponent<ChatForArena>();
        }
        textMessages = new List<string>();
        textMessages.Clear();
    }
    
    void Update()
    {
       UpdateTextMessages();
    }

    void UpdateTextMessages()
    {
        for (int i = 0; i < textMessages.Count; i++)
        {
            content.transform.GetChild(i).gameObject.SetActive(true);
            content.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = textMessages[i];
            content.transform.GetChild(i).GetComponent<TextMeshProUGUI>().color = Color(textMessages[i]);
            
        }
        for (int i = textMessages.Count; i < 20; i++)
        {
            content.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    Color Color(string s)
    {
        if (ReturnSubstring(s) == PhotonNetwork.playerName)
        {
            return new Color32(61, 92, 255, 255);
        }
        return new Color32(255, 53, 53, 255);
    }

    string ReturnSubstring(string s)
    {
        if (s.Contains(":"))
        {
            return s.Substring(0, s.IndexOf(":"));
        }
        return s;
    }

    [PunRPC]
    void SendMessageMine(string text)
    {
        string number = text.Substring(0, 2);
        string textToSend = text.Substring(2);
        if (textMessages.Count < 20)
        {
            textMessages.Add(textToSend);
        }
        else
        {
            textMessages.RemoveAt(0);
            textMessages.Add(textToSend);
        }
        CommandForAudio(number);
        if (FindIfIAmInArena())
        {
            chatArena.isActive = true;
        }
    }

    public void InputFieldText()
    {
        if (chatInput.text != "")
        {
            string message = FinalMessage(chatInput.text);
            pView.RPC("SendMessageMine", PhotonTargets.All, message);
            chatInput.text = "";
        }
    }

    string FinalMessage(string s)
    {
        string message = PhotonNetwork.playerName + ": " + s;

        if (int.TryParse(s, out int result) && result > 0 && result < audioList.audioList.Length)
        {
            message = result + message + " - " + '"' + audioList.audioList[result].name + '"';
            if (result < 10)
            {
                message = "0" + message;
            }
        }
        else
        {
            message = "-1" + message;
        }
        return message;
    }

    bool FindIfIAmInArena()
    {
        if (transform.parent.name == "Arena")
        {
            return true;
        }
        return false;
    }

    void CommandForAudio(string s)
    {
        if (int.TryParse(s, out int result) && result > 0)
        {
            audioManager.SendMessage("PlaySound", result);
        }
    }
}
