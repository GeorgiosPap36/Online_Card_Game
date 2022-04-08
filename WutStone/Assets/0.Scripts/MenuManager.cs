using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;
using System;


public class MenuManager : MonoBehaviour
{
    private GameObject canvas;
    private GameObject mainMenuPanel;
    private GameObject deckBuildingPanel;
    private GameObject arenaPanel;
    private GameObject decksPanel;
    private GameObject deckPanel;
    private GameObject findGamePanel;
    private GameObject roomsPanel;
    private GameObject roomPanel;
    private GameObject startButton;

    private TMP_Dropdown deckDropdown;
    private TMP_Dropdown filterDropdown;

    public GameObject gameManager;
    public GameObject netWork;

    private Decks decks;

    [SerializeField]
    public Filter filter;

    private List<string> paths;

    private TMP_InputField nameInput;
    private TMP_InputField deckNameInput;

    public int deckNumber;


    void Start()
    {
        FindStuff();   
        InitializeDecknames();
        SetUpDeckNamesDropdown();
        SetUpDropdownFilter();
    }

    private void Update()
    {
        if (PhotonNetwork.connectedAndReady)
        {
            if (roomsPanel.activeInHierarchy)
            {
                if (!PhotonNetwork.insideLobby && !PhotonNetwork.inRoom)
                {
                    PhotonNetwork.JoinLobby();
                }
            }            
        }

        if (roomPanel.activeInHierarchy)
        {
            if (PhotonNetwork.inRoom)
            {
                startButton.SetActive(PhotonNetwork.isMasterClient);
            }
        }
    }

    private void SetUpDropdownFilter()
    {
        paths = new List<string>();
        filterDropdown.ClearOptions();
        foreach (string filter in filter.Filters)
        {
            if (filter != "")
            {
                paths.Add(filter);
            }
            else
            {
                paths.Add("All");
            }
            
        }
        filterDropdown.AddOptions(paths);
    }

    void FindStuff()
    {
        canvas = GameObject.Find("Canvas");
        mainMenuPanel = canvas.transform.Find("MainMenu").gameObject;
        deckBuildingPanel = canvas.transform.Find("DeckBuilder").gameObject;
        arenaPanel = canvas.transform.Find("Arena").gameObject;
        decksPanel = deckBuildingPanel.transform.Find("DecksPanel").gameObject;
        deckPanel = deckBuildingPanel.transform.Find("DeckPanel").gameObject;
        findGamePanel = canvas.transform.Find("FindGame").gameObject;
        roomsPanel = findGamePanel.transform.Find("RoomsPanel").gameObject;
        roomPanel = findGamePanel.transform.Find("RoomPanel").gameObject;
        deckNameInput = deckPanel.transform.Find("DeckNameInputField").GetComponent<TMP_InputField>();
        decks = GameObject.Find("Decks").GetComponent<Decks>();
        deckDropdown = roomPanel.transform.Find("DeckDropdown").GetComponent<TMP_Dropdown>();
        filterDropdown = deckPanel.transform.Find("Dropdown").GetComponent<TMP_Dropdown>();
        startButton = roomPanel.transform.Find("StartButton").gameObject;
        nameInput = mainMenuPanel.transform.Find("PlayerNameInput").GetComponent<TMP_InputField>();
    }

    public int DeckNumber()
    {
        string s1 = EventSystem.current.currentSelectedGameObject.name;
        string s2 = s1.Substring(s1.Length - 1);
        return int.Parse(s2);
    }

    private void SetDeckName()
    {
        if (deckNameInput.text != "")
        {
            decks.deckNames[deckNumber] = deckNameInput.text;
            decksPanel.transform.GetChild(deckNumber).GetChild(0).GetComponent<TextMeshProUGUI>().text = deckNameInput.text;
        } 
    }

    private void InitializeDecknames()
    {
        for (int i = 0; i < decks.deckNames.Length; i++)
        {
            if (decks.deckNames[i] != "")
            {
                decksPanel.transform.Find("Deck " + i).GetChild(0).GetComponent<TextMeshProUGUI>().text = decks.deckNames[i];
            }
        }
    }

    private void SetUpDeckNamesDropdown()
    {
        for (int i = 0; i < decksPanel.transform.childCount - 1; i++)
        {
            if (decks.deckNames[i] != "")
            {
                deckDropdown.options[i].text = decks.deckNames[i];
                deckDropdown.transform.Find("Label").GetComponent<TextMeshProUGUI>().text = decks.deckNames[deckDropdown.value];
            }
            
        }
    }

    private bool CheckIfReady()
    {
        Transform playersInRoom = roomPanel.transform.Find("PlayersInRoom");
        bool ready = true;
        for (int i = 0; i < playersInRoom.childCount; i++)
        {
            ready = ready && playersInRoom.GetChild(i).Find("ReadyToggle").GetComponent<Toggle>().isOn;
        }
        return ready;
    }

    [PunRPC]
    void GOFight()
    {
        roomPanel.SetActive(false);
        roomsPanel.SetActive(true);
        findGamePanel.SetActive(false);
        arenaPanel.SetActive(true);
        gameManager.SetActive(true);
        PhotonNetwork.Instantiate("Hero", Vector3.zero, Quaternion.identity, 0);
    }

    //Buttons

    public void DeckBuilderButton()
    {
        mainMenuPanel.SetActive(false); 
        deckBuildingPanel.SetActive(true);
        InitializeDecknames();
    }

    public void ArenaButton()
    {
        netWork.SetActive(true);
        mainMenuPanel.SetActive(false);
        findGamePanel.SetActive(true);
        if (nameInput.text == "")
        {
            nameInput.text = "Player" + Random.Range(1, 100);
        }
        PhotonNetwork.playerName = nameInput.text;
        PhotonNetwork.JoinLobby();
    }

    public void BackButton()
    {
        findGamePanel.SetActive(false);
        deckBuildingPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        if (PhotonNetwork.insideLobby)
        {
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.Disconnect();
        }
        netWork.SetActive(false);
    }

    public void DeckButton()
    {
        decksPanel.SetActive(false);
        deckPanel.SetActive(true);
        deckNumber = DeckNumber();
    }

    public void BackToDeckBuildingMenuButton()
    {
        deckPanel.SetActive(false);
        SetDeckName();
        SaveDat.SaveDecks(decks);
        SetUpDeckNamesDropdown();
        decksPanel.SetActive(true);
    }

    public void RoomStartButton()
    {
        if (PhotonNetwork.inRoom)
        {
            if (CheckIfReady())// && PhotonNetwork.room.PlayerCount == PhotonNetwork.room.MaxPlayers && decks.deckLists[deckDropdown.value].Count == decks.deckMaxCards 
            {
                GetComponent<PhotonView>().RPC("GOFight", PhotonTargets.All);
            }
        }
    }

    public void BackToRoomsPanel()
    {
        if (PhotonNetwork.inRoom)
        {
            PhotonNetwork.LeaveRoom();
            roomPanel.SetActive(false);
            roomsPanel.SetActive(true);
        }
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.inRoom)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LeaveLobby();
            PhotonNetwork.Disconnect();
            netWork.tag = "Player";
            gameManager.SetActive(false);
            netWork.SetActive(false);
            arenaPanel.SetActive(false);
            netWork.SetActive(false);
            mainMenuPanel.SetActive(true);
            netWork.SetActive(false);
        } 
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
