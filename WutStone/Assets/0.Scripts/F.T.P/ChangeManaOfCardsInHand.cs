using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeManaOfCardsInHand : MonoBehaviour
{

    public GameObject hand;

    public void ChangeMana()
    {
        if (hand.transform.childCount > 0)
        {
            string temp;
            for (int i = 0; i < hand.transform.childCount; i++)
            {
                temp = hand.transform.GetChild(i).Find("Mana").GetChild(0).GetComponent<TextMeshProUGUI>().text;
                temp = (int.Parse(temp) + int.Parse(transform.name)).ToString();
                if (int.Parse(temp) < 0)
                {
                    temp = "0";
                }
                hand.transform.GetChild(i).Find("Mana").GetChild(0).GetChild(0).GetComponent<EditText>().ChangeTextForOther(temp);
            }
        }
        
    }
}
