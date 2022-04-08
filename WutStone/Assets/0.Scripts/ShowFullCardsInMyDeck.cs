using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFullCardsInMyDeck : MonoBehaviour
{

    private Card card;

    private GameObject showCase;

    private void OnEnable()
    {
        Initialization();
    }

    public void Initialization()
    {
        card = GetComponent<DisplayCardMyDeck>().card;
        showCase = transform.parent.parent.Find("ShowcaseContainer").GetChild(0).gameObject;
    }

    public void OnHoverEnter()
    {
        showCase.GetComponent<DisplayCard>().card = card;
        showCase.GetComponent<DisplayCard>().Initialization();
        showCase.SetActive(true);
    }

    public void OnHoverExit()
    {
        showCase.SetActive(false);
    }
}
