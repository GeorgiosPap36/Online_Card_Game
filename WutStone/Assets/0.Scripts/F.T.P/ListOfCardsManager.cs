using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListOfCardsManager : MonoBehaviour
{
    public Transform parent;

    public Card card;

    private GameObject c;

    void Update()
    {
        card = GetComponent<DisplayCard>().card;
    }

    public void AddToHand()
    {
        c = PhotonNetwork.Instantiate("Card", Vector3.zero, Quaternion.identity, 0);
        c.GetComponent<DisplayCard>().card = card;
        c.GetComponent<DisplayCard>().Initialization();
    }

}
