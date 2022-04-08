using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonsIfSpectator : MonoBehaviour
{

    private GameObject networkObject;

    [SerializeField]
    private GameObject[] toBeClosedObjects;

    void Awake()
    {
        networkObject = GameObject.Find("Network");

        for (int i = 0; i < toBeClosedObjects.Length; i++)
        {
            toBeClosedObjects[i].SetActive(networkObject.tag == "Player");
        }
    }

}
