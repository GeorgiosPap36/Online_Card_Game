using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public GameObject cardList;

    public GameObject networkObject;

    private bool open;

    void Start()
    {
        open = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && networkObject.tag == "Player")
        {
            open = !open;
            cardList.SetActive(open);
        }
    }


}
