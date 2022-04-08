using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class DeckBuilder : MonoBehaviour
{

    private GameObject myDeck;

    private TMP_InputField deckNameInput;

    private TextMeshProUGUI numberOfCardsInDeck;

    private Decks decks;
    private MenuManager mManager;
    private ManaGraphManager mGraphManager;

    private int deckNumber;


    void Awake()
    {
        decks = GameObject.Find("Decks").GetComponent<Decks>();
        myDeck = transform.Find("MyDeck").gameObject;
        mManager = transform.parent.parent.GetComponent<MenuManager>();
        deckNameInput = transform.Find("DeckNameInputField").GetComponent<TMP_InputField>();
        mGraphManager = transform.Find("ShowManaGraph").GetComponent<ManaGraphManager>();
        numberOfCardsInDeck = transform.Find("DeckNumberOfCards").GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        InitializeMyDeck();
    }

    private void Update()
    {
        numberOfCardsInDeck.text = "Number Of Cards: " + decks.deckLists[deckNumber].Count;
    }

    void InitializeMyDeck()
    {
        deckNumber = mManager.DeckNumber();
        deckNameInput.text = decks.deckNames[deckNumber];
        Initialization();
    }

    void Initialization()
    {
        for (int i = 0; i < myDeck.transform.childCount; i++)
        {
            myDeck.transform.GetChild(i).gameObject.SetActive(false);
            mGraphManager.Clear();
        }
        decks.deckLists[deckNumber].Sort(SortByCardName);
        decks.deckLists[deckNumber].Sort(SortByCardMana);
        for (int i = 0; i < decks.deckLists[deckNumber].Count; i++)
        {
            mGraphManager.UpdateMana(1, decks.deckLists[deckNumber][i].mana);
            if (FindCardWithCard(decks.deckLists[deckNumber][i]) == null)
            {
                myDeck.transform.GetChild(i).gameObject.SetActive(true);
                myDeck.transform.GetChild(i).GetComponent<DisplayCardMyDeck>().card = decks.deckLists[deckNumber][i];
                myDeck.transform.GetChild(i).GetComponent<DisplayCardMyDeck>().Initialization();
                myDeck.transform.GetChild(i).GetComponent<ShowFullCardsInMyDeck>().Initialization();
                myDeck.transform.GetChild(i).GetComponent<DisplayCardMyDeck>().UpdateCopies(NumberOfCopies(decks.deckLists[deckNumber][i]));
            }
        }
    }

    public void AddCard(Card card, int i)
    {
        if (decks.deckLists[deckNumber].Count < decks.deckMaxCards && NumberOfCopies(card) < (int)card.rarity + 1 && card.canBePutInDeck)
        {
            decks.deckLists[deckNumber].Add(card);
            Initialization();
        }
    }

    static int SortByCardName(Card c1, Card c2)
    {
        return c1.name.CompareTo(c2.name);
    }

    static int SortByCardMana(Card c1, Card c2)
    {
        return c1.mana.CompareTo(c2.mana);
    }

    public void RemoveFromDeck(Card card, int i)
    {
        if (decks.deckLists[deckNumber].Count > 0)
        {
            mGraphManager.UpdateMana(-1, card.mana);
            decks.deckLists[deckNumber].Remove(card);
            AddRemove(i, card, false, NumberOfCopies(card));
        }
    }

    void AddRemove(int n, Card c, bool b, int copies)
    {
        myDeck.transform.GetChild(n).gameObject.GetComponent<DisplayCardMyDeck>().card = c;
        myDeck.transform.GetChild(n).GetComponent<DisplayCardMyDeck>().Initialization();
        
        if (copies < 1)
        {
            myDeck.transform.GetChild(n).gameObject.SetActive(b);
        }
        else
        {
            FindCardWithCard(c).GetComponent<DisplayCardMyDeck>().UpdateCopies(copies);
        }
    }

    void PrintDeck()
    {
        Debug.Log("Deck contains");
        for (int i = 0; i < decks.deckLists[deckNumber].Count; i++)
        {
           
            Debug.Log(decks.deckLists[deckNumber][i]);
        }
    }

    int NumberOfCopies(Card card)
    {
        int n = 0;
        foreach (Card c in decks.deckLists[deckNumber])
        {
            if (c == card)
            {
                n++;
            }
        }
        return n;
    }

    GameObject FindCardWithCard(Card c)
    {
        GameObject temp;
        for (int i = 0; i < myDeck.transform.childCount; i++)
        {
            temp = myDeck.transform.GetChild(i).gameObject;
            if (temp.GetComponent<DisplayCardMyDeck>().card == c && temp.activeInHierarchy)
            {
                return temp;
            }
        }
        return null;
    }

}
