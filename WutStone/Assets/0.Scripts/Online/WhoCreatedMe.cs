using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WhoCreatedMe : MonoBehaviour
{

    private GameObject networkObject;

    private PhotonView pView;

    private Transform parent;

    public TextMeshProUGUI playerNameText;

    private string playerName;

    private bool isOwner;


    void Start()
    {
        networkObject = GameObject.Find("Network");
        pView = GetComponent<PhotonView>();
        isOwner = pView.owner == PhotonNetwork.player;
        playerName = GameObject.Find("Canvas").transform.Find("MainMenu").Find("PlayerNameInput").GetComponent<TMP_InputField>().text;
        
        WhoAmI();
    }

    void WhoAmI()
    {
        if (transform.name == "Hero(Clone)")
        {
            parent = GameObject.Find("Canvas").transform.Find("Arena").Find("Field");
            SetUpHero();
        }

    }

    void SetUpHero()
    {
        transform.SetParent(parent);
        transform.localScale = new Vector3(1, 1, 1);
        
        if (GetIfIAmGoodOrBad())
        {
            transform.name = "MyHero";
            playerNameText.text = pView.owner.NickName;
        }
        else
        {
            transform.name = "EnemyHero";
            transform.Find("HeroArt").GetComponent<RectTransform>().localPosition = new Vector3(0, 145, 0);
            transform.Find("Health").GetComponent<RectTransform>().localPosition = new Vector3(27.3f, 114, 0);
            transform.Find("HeroPowerArt").GetComponent<RectTransform>().localPosition = new Vector3(72, 129, 0);
            transform.Find("HeroPowerManaCost").GetComponent<RectTransform>().localPosition = new Vector3(74, 150, 0);
            transform.Find("ManaText").GetComponent<RectTransform>().localPosition = new Vector3(108, 194, 0);
            playerNameText.GetComponent<RectTransform>().localPosition = new Vector3(-290, 90, 0);
            playerNameText.text = pView.owner.NickName;
        }
    }

    bool GetIfIAmGoodOrBad()
    {
        if (isOwner)
        {
            return true;
        }
        else
        {
            if (networkObject.tag == "Player")
            {
                return false;
            }
            else if(networkObject.tag == "Spectator")
            {
                if (pView.ownerId == 1)
                {
                    return true;
                }
                else if (pView.ownerId == 2)
                {
                    return false;
                }
            }
        }
        return false;
    }

    [PunRPC]
    void SetPlayerName(string name)
    {
        playerNameText.text = name;
    }

}
