using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManaGraphManager : MonoBehaviour
{
    private Decks decks;

    [SerializeField]
    private int[] cardsPerManaCost;

    void Awake()
    {
        decks = GameObject.Find("Decks").GetComponent<Decks>();
        cardsPerManaCost = new int[16];
    }

    public void UpdateMana(int x, int i)
    {
        cardsPerManaCost[i] += x;
        UpdateUI(i);
    }

    public void Clear()
    {
        Awake();
        for (int i = 0; i < cardsPerManaCost.Length; i++)
        {
            cardsPerManaCost[i] = 0;
            transform.GetChild(i).Find("NumberOfCards").GetComponent<TextMeshProUGUI>().text = cardsPerManaCost[i].ToString();
            transform.GetChild(i).Find("Mask").GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(10, cardsPerManaCost[i] * (240 / decks.deckMaxCards));
        }
    }

    void UpdateUI(int i)
    {
        transform.GetChild(i).Find("NumberOfCards").GetComponent<TextMeshProUGUI>().text = cardsPerManaCost[i].ToString();
        transform.GetChild(i).Find("Mask").GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(10, cardsPerManaCost[i] * (240 / decks.deckMaxCards));
    }
     
}
