using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRemoveCardsFromDeck : MonoBehaviour
{

    private DeckBuilder dBuilder;

    private Card card;

    public GameObject myDeck;

    private GameObject showcase;

    void Start()
    {
        dBuilder = GameObject.Find("DeckPanel").GetComponent<DeckBuilder>();
        showcase = transform.parent.parent.Find("ShowcaseContainer").GetChild(0).gameObject;
    }

    public void OnClick()
    { 
        if (transform.parent.name == "ListOfCards")
        {
            card = GetComponent<DisplayCard>().card;
            dBuilder.AddCard(card, FirstSleeping());
        }
        else if (transform.parent.name == "MyDeck")
        {
            showcase.SetActive(false);
            card = GetComponent<DisplayCardMyDeck>().card;
            dBuilder.RemoveFromDeck(card, transform.GetSiblingIndex());
        }
    }

    int FirstSleeping()
    {
        int number;
        for (int i = 0; i < myDeck.transform.childCount; i++)
        {
            if (!myDeck.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                number = i;
                return number;
            }
        }
        return 0;
    }
}
