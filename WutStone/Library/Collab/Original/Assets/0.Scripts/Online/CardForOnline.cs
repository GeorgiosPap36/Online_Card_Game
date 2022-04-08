using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardForOnline : MonoBehaviour
{
    [SerializeField]
    private string[] folders;

    private Transform previousParent;
    private Transform arena;
    private Transform myHand;
    public Transform enemyHand;
    private PhotonView pView;

    public bool isMine;

    void Start()
    {
        arena = GameObject.Find("Canvas").transform.Find("Arena");
        myHand = arena.Find("Hands").Find("MyHand");
        enemyHand = arena.Find("Hands").Find("EnemyHand");
        pView = GetComponent<PhotonView>();
        
        SetUpCard();
    }

    void Update()
    {
        ManageComponents();
        if (isMine)
        {
            if (transform.parent != previousParent && transform.parent != null && transform.parent != GameObject.Find("Canvas").transform)
            {
                previousParent = transform.parent;
                if (previousParent.name == "MySide")
                {
                    pView.RPC("ChangeParent", PhotonTargets.Others, "EnemySide");
                }
                else if (previousParent.name == "MyHand")
                {
                    pView.RPC("ChangeParent", PhotonTargets.Others, "EnemyHand");
                }
            }
        }
    }

    void SetUpCard()
    {
        isMine = pView.owner == PhotonNetwork.player;
        transform.SetParent(myHand);
        previousParent = transform.parent;
        if (pView.owner != PhotonNetwork.player)
        {
            transform.SetParent(enemyHand);
            transform.Find("CardBack").gameObject.SetActive(true);
            GetComponent<DragAndDrop>().enabled = false;
        }
        if (isMine)
        {
            pView.RPC("UpdateCard", PhotonTargets.Others, GetComponent<DisplayCard>().card.name);
        }
        transform.localScale = new Vector3(1, 1, 1);
        transform.name = GetComponent<DisplayCard>().card.name;  
    }

    void ManageComponents()
    {
        transform.Find("CardBack").gameObject.SetActive(!isMine && (transform.parent == enemyHand));
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
        Debug.Log(5);
        
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
        foreach (string path in folders)
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
