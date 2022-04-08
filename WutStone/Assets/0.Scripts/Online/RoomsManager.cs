using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RoomsManager : MonoBehaviour
{

    private GameObject createRoomPanel;
    private GameObject roomsPanel;
    private GameObject roomPanel;
    private GameObject availableRoomsList;

    private TMP_InputField roomNameInput;
    private Toggle isVisibleToggle;

    private Button backButton;

    private int maxNumberOfRooms;


    void Start()
    {
        IntializationOfUIStuff();
        UpdateRooms();
        maxNumberOfRooms = availableRoomsList.transform.childCount;
    }

    void IntializationOfUIStuff()
    {
        roomsPanel = transform.Find("RoomsPanel").gameObject;
        roomPanel = transform.Find("RoomPanel").gameObject;
        availableRoomsList = roomsPanel.transform.Find("AvailableRooms").gameObject;
        createRoomPanel = roomsPanel.transform.Find("CreateRoomPanel").gameObject;
        backButton = roomsPanel.transform.Find("BackButton").GetComponent<Button>();
        CreateRoomPanelStuff();
    }

    void CreateRoomPanelStuff()
    {
        roomNameInput = createRoomPanel.transform.Find("RoomNameInput").GetComponent<TMP_InputField>();
        isVisibleToggle = createRoomPanel.transform.Find("IsVisibleToAllToggle").GetComponent<Toggle>();
    }

    void UpdateRooms()
    {
        for (int i = 0; i < maxNumberOfRooms; i++)
        {
            availableRoomsList.transform.GetChild(i).gameObject.SetActive(false);
        }
        int k = 0;
        foreach (RoomInfo room in PhotonNetwork.GetRoomList())
        {
            availableRoomsList.transform.GetChild(k).gameObject.SetActive(true);
            availableRoomsList.transform.GetChild(k).name = room.Name;
            availableRoomsList.transform.GetChild(k).Find("RoomNameText").GetComponent<TextMeshProUGUI>().text = room.Name;
            availableRoomsList.transform.GetChild(k).Find("PlayersInRoomText").GetComponent<TextMeshProUGUI>().text = ": " + room.PlayerCount + "/" + room.MaxPlayers;
            k++;
        }
    }

    void CreateRoom()
    {
        string name = roomNameInput.text;
        RoomOptions roomOptions = new RoomOptions() { IsVisible = isVisibleToggle.isOn, MaxPlayers = 5, };
        PhotonNetwork.CreateRoom(name, roomOptions, TypedLobby.Default);
        PhotonNetwork.JoinOrCreateRoom(name, roomOptions, TypedLobby.Default);
    }

    bool CheckRoomName(string s)
    {
        foreach (RoomInfo room in PhotonNetwork.GetRoomList())
        {
            if (room.Name == s)
            {
                return false;
            }
        }
        return true;
    }

    //UI
    public void CreateButton()
    {
        if (PhotonNetwork.countOfRooms < maxNumberOfRooms)
        {
            backButton.interactable = false;
            createRoomPanel.SetActive(true);
        } 
    }

    public void RefreshButton()
    {
        UpdateRooms();
    }

    public void CreateRoomPanelCreate()
    {
        if (roomNameInput.text.Length > 0 && CheckRoomName(roomNameInput.text))
        {
            CreateRoom();
            roomsPanel.SetActive(false);
            roomPanel.SetActive(true);
            createRoomPanel.SetActive(false);
            backButton.interactable = true;
        }  
    }

    public void CreateRoomPanelBack()
    {
        backButton.interactable = true;
        createRoomPanel.SetActive(false);
    }

}
