using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardForOnline : MonoBehaviour
{
    [SerializeField]
    public Filter filter;

    private GameObject networkObject;

    private Transform previousParent;
    private Transform arena;
    private Transform myHand;
    public Transform enemyHand;
    private PhotonView pView;

    public bool isMine;

    void Start()
    {
        networkObject = GameObject.Find("Network");
        arena = GameObject.Find("Canvas").transform.Find("Arena");
        myHand = arena.Find("Hands").Find("MyHand");
        enemyHand = arena.Find("Hands").Find("EnemyHand");
        pView = GetComponent<PhotonView>();
        SetUpCard();
    }

    void Update()
    {
        ManageComponents();
        UpdateParent();
    }

    void UpdateParent()
    {
        if (isMine)
        {
            if (transform.parent != previousParent && transform.parent != null && transform.parent != GameObject.Find("Canvas").transform)
            {
                previousParent = transform.parent;
                if (previousParent.name == "MySide")
                {
                    pView.RPC("ChangeParent", PhotonTargets.OthersBuffered, "EnemySide");
                }
                else if (previousParent.name == "MyHand")
                {
                    pView.RPC("ChangeParent", PhotonTargets.OthersBuffered, "EnemyHand");
                }
            }
        }
    }

    void SetUpCard()
    {
        isMine = pView.owner == PhotonNetwork.player;
        transform.SetParent(myHand);
        previousParent = transform.parent;
        if (!isMine)
        {
            transform.SetParent(enemyHand);
            transform.Find("CardBack").gameObject.SetActive(true);
            GetComponent<DragAndDrop>().enabled = false;
        }
        if (isMine)
        {
            pView.RPC("UpdateCard", PhotonTargets.OthersBuffered, GetComponent<DisplayCard>().card.name);
        }
        transform.localScale = new Vector3(1, 1, 1);
        transform.name = GetComponent<DisplayCard>().card.name;  
    }

    void ManageComponents()
    {
        if (networkObject.tag == "Player")
        {
            transform.Find("CardBack").gameObject.SetActive(!isMine && (transform.parent == enemyHand));
        }
        else
        {
            transform.transform.Find("CardBack").gameObject.SetActive(false);
        }
        
    }

    [PunRPC]
    void UpdateCard(string c)
    {
        Card card = FindCard(c);
        if (card != null)
        {
            GetComponent<DisplayCard>().card = card;
            transform.name = card.name;
            GetComponent<DisplayCard>().Initialization();
        }      
    }

    [PunRPC]
    void ChangeParent(string parent)
    {
        if (!arena.Find("Field").Find(parent))
        {
            transform.SetParent(arena.Find("Hands").Find(parent));
        }
        else
        {
            transform.SetParent(arena.Find("Field").Find(parent));
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
