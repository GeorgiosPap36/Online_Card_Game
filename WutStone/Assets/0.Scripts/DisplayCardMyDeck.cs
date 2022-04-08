using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayCardMyDeck : MonoBehaviour
{

    public Card card;

    private Decks decks;

    private TextMeshProUGUI cardNameText;
    private TextMeshProUGUI cardCopiesText;
    private TextMeshProUGUI cardManaText;

    private Image image;

    private int deckNumber;


    public void Initialization()
    {
        FindComponents();
        SetUpCard();
    }

    void SetUpCard()
    {
        if (card != null)
        {
            cardNameText.text = card.cardName;
            cardManaText.text = card.mana.ToString();
            image.color = card.color;
        }
    }

    public void UpdateCopies(int i)
    {
        cardCopiesText.text = i.ToString();
    }

    void FindComponents()
    {
        cardNameText = transform.Find("CardName").GetComponent<TextMeshProUGUI>();
        cardCopiesText = transform.Find("CardCopies").GetComponent<TextMeshProUGUI>();
        cardManaText= transform.Find("Mana").GetChild(0).GetComponent<TextMeshProUGUI>();
        decks = GameObject.Find("Decks").GetComponent<Decks>();
        image = GetComponent<Image>();
    }

}