using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverOverCard : MonoBehaviour
{

    private CardForOnline cOnline;

    private Canvas canvas;

    private Vector3 startingScale;

    [SerializeField, Range(1, 2)]
    private float hoverScale = 1;


    private void Start()
    {
        cOnline = GetComponent<CardForOnline>();
        canvas = GetComponent<Canvas>();
        startingScale = transform.localScale;
    }

    public void HoverEnter()
    {
        if (!cOnline.isMine && transform.parent == cOnline.enemyHand)
        {
            return;
        }
        canvas.sortingOrder = 10;
        transform.localScale = hoverScale * startingScale;
    }

    public void HoverExit()
    {
        canvas.sortingOrder = 0;
        transform.localScale = startingScale;
    }
}
