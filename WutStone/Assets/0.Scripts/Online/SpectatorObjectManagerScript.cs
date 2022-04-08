using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorObjectManagerScript : MonoBehaviour
{

    private PhotonView pView;
    private GameObject network;
    private GameObject hands;
    private GameObject field;
    
    void Start()
    {
        pView = GetComponent<PhotonView>();
        hands = GameObject.Find("Canvas").transform.Find("Arena").Find("Hands").gameObject;
        field = GameObject.Find("Canvas").transform.Find("Arena").Find("Field").gameObject;
        network = GameObject.Find("Network").gameObject;
    }

    void Update()
    {
        if (transform.parent != null)
        {
            if (network.tag == "Spectator")
            {
                RearrangeCards();
            }
        }  
    }

    void RearrangeCards()
    {
        if (transform.parent.name == "EnemyHand")
        {
            if (pView.ownerId == 1)
            {
                transform.SetParent(hands.transform.Find("MyHand"));
            }
            else if (pView.ownerId == 2)
            {
                transform.SetParent(hands.transform.Find("EnemyHand"));
            }
        }
        else if (transform.parent.name == "EnemySide")
        {
            if (pView.ownerId == 1)
            {
                transform.SetParent(field.transform.Find("MySide"));
            }
            else if (pView.ownerId == 2)
            {
                transform.SetParent(field.transform.Find("EnemySide"));
            }
        }
    }

}
