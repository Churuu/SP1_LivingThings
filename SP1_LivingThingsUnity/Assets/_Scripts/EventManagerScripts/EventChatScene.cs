﻿using UnityEngine;

public class EventChatScene : MonoBehaviour
{//Jonas 2019-03-11

  [SerializeField]  GameObject[] gameObjects;
    // Use this for initialization
    void Start()
    {
        EventManager.instance.OnChatActiv += NoMomentOnChat;
        EventManager.instance.OnChatEnd += ChatEndMomentOn;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<PlayerController>().SetPlayerState(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void NoMomentOnChat()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<PlayerController>().SetPlayerState(false);
            gameObjects[i].GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        }
    }
    private void ChatEndMomentOn()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            gameObjects[i].GetComponent<PlayerController>().SetPlayerState(true);
        }
    }
}
