using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetPhotonViewComponent : MonoBehaviour
{

    private PhotonView pView;

    void Start()
    {
        pView = GetComponent<PhotonView>();
        pView.ObservedComponents.Add(transform.parent.GetComponent<TextMeshProUGUI>());
    }

}
