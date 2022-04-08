using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatForArena : MonoBehaviour
{

    public GameObject chatPanel;

    public bool isActive;

    void Awake()
    {
        isActive = false;
    }

    private void Update()
    {
        if (!isActive)
        {
            chatPanel.transform.position = new Vector2(-600, 0);
        }
        else
        {
            chatPanel.transform.position = new Vector2(0, 0);
        }
    }

    public void ChatButton()
    {
        isActive = !isActive;

    }
}

