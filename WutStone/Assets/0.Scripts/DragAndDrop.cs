using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IDragHandler, IEndDragHandler
{

    private GameManager manager;

    private enum TypeOfDrag
    {
        Play,
        Attack,
    };

    [SerializeField]
    private TypeOfDrag type;

    private GameObject myHand;
    private GameObject canvas;

    private Transform parent;

    private GraphicRaycaster rayCaster;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    private int siblingNumber;
    private bool wasParentHand;
    private bool dragging;


    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        canvas = GameObject.Find("Canvas");
        myHand = canvas.transform.Find("Arena").Find("Hands").Find("MyHand").gameObject;
        rayCaster = canvas.GetComponent<GraphicRaycaster>();
        parent = myHand.transform;
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        dragging = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragging)
        {
            dragging = true;
            siblingNumber = transform.GetSiblingIndex();
            Debug.Log(siblingNumber);
            if (transform.parent.tag == "MyHand")
            {
                Debug.Log(0);
                wasParentHand = true;
            }
            else
            {
                Debug.Log(1);
                wasParentHand = false;
            }
        }
        transform.position = PositionToWorld(Input.mousePosition, 10);
        transform.SetParent(canvas.transform);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(BestParent(CheckWhatGotHit()));
        if (transform.parent != null)
        {
            if (transform.parent.tag == "MyHand")
            {
                if (wasParentHand)
                {
                    transform.SetSiblingIndex(siblingNumber);
                }
            }
        }
        
        dragging = false;
    }

    List<RaycastResult> CheckWhatGotHit()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        rayCaster.Raycast(pointerEventData, results);
        return results;
    }

    Transform BestParent(List<RaycastResult> list)
    {
        foreach (RaycastResult thing in list)
        {
            if (thing.gameObject.transform.tag == "Destroy")
            {
                PhotonNetwork.Destroy(gameObject);
                return null;
            }
            else if (thing.gameObject.transform.tag == "Deck")
            {
                manager.currentDeck.Add(GetComponent<DisplayCard>().card);
                manager.DeckShuffle(manager.currentDeck);
                PhotonNetwork.Destroy(gameObject);
            }
            else if (thing.gameObject.transform.tag == "Field")
            {
                parent = thing.gameObject.transform;
                return parent;
            }
            else if (thing.gameObject.transform.tag == "MyHand")
            {
                parent = thing.gameObject.transform;
                return parent;
            }
        }
        return parent;
    }

    private Vector3 PositionToWorld(Vector3 position,int z)
    {
        Vector3 temp = position;
        temp.z = z;
        temp = Camera.main.ScreenToWorldPoint(temp);
        return temp;
    }
}
