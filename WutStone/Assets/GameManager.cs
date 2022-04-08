using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    private Decks decks;

    public List<Card> currentDeck = new List<Card>();

    private TextMeshProUGUI deckCards;
    private TMP_Dropdown dropdown;

    private int selectedDeck;


    void OnEnable()
    {
        dropdown = GameObject.Find("Canvas").transform.Find("FindGame").Find("RoomPanel").Find("DeckDropdown").GetComponent<TMP_Dropdown>();
        selectedDeck = dropdown.value;
        decks = GameObject.Find("Decks").GetComponent<Decks>();
        SetUpCurrentDeck();
        DeckShuffle(currentDeck);
        deckCards = GameObject.Find("Canvas").transform.Find("Arena").Find("DeckCards").GetComponent<TextMeshProUGUI>();
    }

    void SetUpCurrentDeck()
    {
        currentDeck.Clear();
        for (int i = 0; i < decks.deckLists[selectedDeck].Count; i++)
        {
            currentDeck.Add(decks.deckLists[selectedDeck][i]);
        }
    }

    private void Update()
    {
        deckCards.text = currentDeck.Count.ToString();
    }

    public void Draw()
    {
        if (currentDeck.Count >= 1)
        {
            GameObject card = PhotonNetwork.Instantiate("Card", Vector3.zero, Quaternion.identity, 0);
            card.GetComponent<DisplayCard>().card = currentDeck[0];
            currentDeck.RemoveAt(0);
        }
    }

    public void DeckShuffle(List<Card> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Card tmp = list[i];
            int r = Random.Range(i, list.Count);
            list[i] = list[r];
            list[r] = tmp;
        }
    }

}
