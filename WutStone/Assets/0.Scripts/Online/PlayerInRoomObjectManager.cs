using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInRoomObjectManager : MonoBehaviour
{

    private GameObject toggleReady;
    private GameObject toggleForOthers;
    private GameObject kickButton;

    private Decks decks;

    private Toggle readyToggle;

    private TextMeshProUGUI nameText;
    private TMP_Dropdown deckDropDown;

    private PhotonView pView;

    private MenuManager mManagerScript;


    void Start()
    {
        Intialization();
        ActivateDeactivate();
        SetUpAfterSpawn();
    }

    void Intialization()
    {
        toggleReady = transform.Find("ReadyToggle").gameObject;
        readyToggle = toggleReady.GetComponent<Toggle>();
        toggleForOthers = transform.Find("AppearanceForOthers").gameObject;
        kickButton = transform.Find("KickButton").gameObject;
        nameText = transform.Find("PlayerName").GetComponent<TextMeshProUGUI>();
        pView = GetComponent<PhotonView>();
        mManagerScript = GameObject.Find("Canvas").GetComponent<MenuManager>();
        decks = GameObject.Find("Decks").GetComponent<Decks>();
        deckDropDown = GameObject.Find("Canvas").transform.Find("FindGame").Find("RoomPanel").Find("DeckDropdown").GetComponent<TMP_Dropdown>();
    }

    void ActivateDeactivate()
    {
        toggleForOthers.SetActive(false);
        kickButton.SetActive(!pView.isMine && PhotonNetwork.isMasterClient);
    }

    void SetUpAfterSpawn()
    {
        transform.SetParent(GameObject.Find("Canvas").transform.Find("FindGame").Find("RoomPanel").Find("PlayersInRoom"));
        transform.name = pView.owner.NickName;
        nameText.text = pView.owner.NickName;
        transform.localScale = new Vector3(1, 1, 1);
    }

    private void Update()
    {
        readyToggle.interactable = (decks.deckLists[deckDropDown.value].Count == decks.deckMaxCards && pView.isMine);// && PhotonNetwork.room.MaxPlayers == PhotonNetwork.room.PlayerCount
        if (PhotonNetwork.room.PlayerCount < 2)
        {
            readyToggle.isOn = false;
        }
        
    }

    [PunRPC]
    void GetKicked()
    {
        if (pView.isMine)
        {
            mManagerScript.BackToRoomsPanel();
        }
    }

    [PunRPC]
    void ChangeIfReady(bool ready)
    {
        readyToggle.isOn = ready;
    }

    //UI
    public void KickButton()
    {
        pView.RPC("GetKicked", PhotonTargets.All);
    }

    public void ReadyToggle()
    {
        pView.RPC("ChangeIfReady", PhotonTargets.All, readyToggle.isOn);
    }
    

}
