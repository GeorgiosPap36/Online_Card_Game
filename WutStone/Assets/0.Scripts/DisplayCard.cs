using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayCard : MonoBehaviour
{

    public Card card;

    private TextMeshProUGUI cardNameText;
    private TextMeshProUGUI descriptionText;
    private TextMeshProUGUI manaText;
    private TextMeshProUGUI attackText;
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI archTypeText;

    private Image rarity;
    private Image artwork;

    private Color32[] colors = new Color32[4];

    private string type;
    private string archType;

    void Start()
    {
        Initialization();
    }

    public void Initialization()
    {
        FindComponents();
        SetUpStats();
    }

    private void SetUpRarityColors()
    {
        colors[0] = new Color32(255, 147, 0, 255);
        colors[1] = new Color32(188, 10, 105, 255);
        colors[2] = new Color32(35, 30, 200, 255);
    }

    private void FindComponents()
    {
        cardNameText = transform.Find("Name").GetChild(0).GetComponent<TextMeshProUGUI>();
        descriptionText = transform.Find("Description").GetComponent<TextMeshProUGUI>();
        manaText = transform.Find("Mana").GetChild(0).GetComponent<TextMeshProUGUI>();
        attackText = transform.Find("Attack").GetChild(0).GetComponent<TextMeshProUGUI>();
        healthText = transform.Find("Health").GetChild(0).GetComponent<TextMeshProUGUI>();
        archTypeText = transform.Find("Archtype").GetChild(0).GetComponent<TextMeshProUGUI>();
        artwork = transform.Find("CardArtMask").GetChild(0).GetComponent<Image>();
        rarity = transform.Find("Rarity").GetComponent<Image>();
        
    }

    void SetUpStats()
    {
        if (card != null)
        {
            SetUpRarityColors();
            type = card.cardType.ToString();
            cardNameText.text = card.cardName;
            descriptionText.text = card.description;
            artwork.sprite = card.artwork;
            manaText.text = card.mana.ToString();
            rarity.color = colors[(int)card.rarity];

            artwork.transform.localPosition = card.offeset;
            artwork.transform.localScale = card.scale;

            GetComponent<Image>().color = card.color;

            if (type == "Minion")
            {
                attackText.text = card.attack.ToString();
                healthText.text = card.health.ToString();
                attackText.transform.parent.gameObject.SetActive(true);
                healthText.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                attackText.transform.parent.gameObject.SetActive(false);
                healthText.transform.parent.gameObject.SetActive(false);
            }

            if (card.archType.ToString() == "Nothing")
            {
                archTypeText.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                archTypeText.text = card.archType.ToString();
                archTypeText.transform.parent.gameObject.SetActive(true);
            }
        }
        
    }
}
