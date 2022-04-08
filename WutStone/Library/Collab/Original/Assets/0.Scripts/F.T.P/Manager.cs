using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public GameObject cardList;

    private bool open;

    void Start()
    {
        open = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            open = !open;
            cardList.SetActive(open);
        }
    }

}
