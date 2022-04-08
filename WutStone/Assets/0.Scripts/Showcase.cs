using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Showcase : MonoBehaviour
{

    private CardForOnline cOnline;

    public GameObject showcase;

    private Canvas canvas;
    private GameObject extraFormsPanel;

    private Card card;
 
    void Start()
    {
        cOnline = GetComponent<CardForOnline>();
        canvas = transform.GetComponent<Canvas>();
        card = GetComponent<DisplayCard>().card;
        extraFormsPanel = GameObject.Find("Canvas").transform.Find("Arena").Find("ExtraFormsPanel").gameObject;
    }

    public void HoverEnter()
    {
        if (!cOnline.isMine && transform.parent == cOnline.enemyHand)
        {
            return;
        }
        CreateShowcase();
    }

    public void HoverExit()
    {
        DestroyShowcase();
        extraFormsPanel.SetActive(false);
    }

    void CreateShowcase()
    {
        if (card.forms.Length > 0)
        {
            for (int i = 0; i < card.forms.Length; i++)
            {
                Instantiate(showcase, extraFormsPanel.transform, false).GetComponent<DisplayCard>().card = card.forms[i];
            }
            extraFormsPanel.SetActive(true);
        }
    }

    void DestroyShowcase()
    {
        foreach (Transform child in extraFormsPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
