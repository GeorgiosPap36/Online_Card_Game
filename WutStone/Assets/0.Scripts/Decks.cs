using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decks : MonoBehaviour
{

    public List<Card>[] deckLists;
    [HideInInspector]
    public int[] deckSize;
    [HideInInspector]
    public string[] deckNames;
    [SerializeField]
    public Filter filter;

    public int deckMaxCards;

    void Awake()
    {
        deckLists = new List<Card>[10];
        deckSize = new int[10];
        deckNames = new string[10];
        InitializeDeckNames();
        for (int i = 0; i < deckLists.Length; i++)
        {
            deckLists[i] = new List<Card>();
        }
        LoadDecks();
    }

    void InitializeDeckNames()
    {
        for (int i = 0; i < deckNames.Length; i++)
        {
            deckNames[i] = "";
            deckSize[i] = 0;
        }
    }

    private void LoadDecks()
    {
        PlayerDat dat = SaveDat.LoadDecks();

        if (dat != null)
        {
            for (int i = 0; i < 10; i++)
            {
                deckSize[i] = dat.deckSize[i];
                for (int j = 0; j < deckSize[i]; j++)
                {
                    deckLists[i].Add(FindCard(dat.deckLists[j,i]));
                }
                deckNames[i] = dat.deckNames[i];
            }
        }
    }

    Card FindCard(string name)
    {
        Card card;
        foreach (string path in filter.Filters)
        {
            card = Resources.Load("Cards/" + path + "/" + name) as Card;
            if (card != null)
            {
                return card;
            }
        }
        return null;
    }
}
