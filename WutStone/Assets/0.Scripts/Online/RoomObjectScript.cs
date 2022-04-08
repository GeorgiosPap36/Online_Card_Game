using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomObjectScript : MonoBehaviour
{

    public GameObject gameManager;

    private GameObject findGamePanel;
    private GameObject roomPanel;
    private GameObject roomsPanel;
    private GameObject arenaPanel;
    private GameObject network;


    void Start()
    {
        findGamePanel = GameObject.Find("Canvas").transform.Find("FindGame").gameObject;
        roomPanel = findGamePanel.transform.Find("RoomPanel").gameObject;
        roomsPanel = findGamePanel.transform.Find("RoomsPanel").gameObject;
        arenaPanel = GameObject.Find("Canvas").transform.Find("Arena").gameObject;
        network = GameObject.Find("Network").gameObject;
    }

    public void JoinRoom()
    {
        string name;
        if (transform.parent != roomsPanel.transform)
        {
            name = transform.name;
        }
        else
        {
            name = GetComponent<TMP_InputField>().text;
        }
        if (CanFit(name))
        {
            PhotonNetwork.JoinRoom(name);
            roomPanel.SetActive(true);
            roomsPanel.SetActive(false);
        }   
    }

    public void SpectateRoom()
    {
        network.transform.tag = "Spectator";
        gameManager.SetActive(true);
        PhotonNetwork.JoinRoom(name);
        findGamePanel.SetActive(false);
        arenaPanel.SetActive(true);
    }

    bool CanFit(string name)
    {
        foreach (RoomInfo room in PhotonNetwork.GetRoomList())
        {
            if (room.Name == name)
            {
                if (room.PlayerCount <= room.MaxPlayers)
                {
                    return true;
                }
            }
        }
        return false;
    }

}
