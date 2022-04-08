using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ServerHandler : MonoBehaviour
{

    void OnEnable()
    {
        PhotonNetwork.ConnectUsingSettings("v1.0");
    }

    void OnJoinedRoom()
    {
        if (transform.tag == "Player")
        {
            PhotonNetwork.Instantiate("PlayerInRoom", Vector3.zero, Quaternion.identity, 0);
        }
    }

}
