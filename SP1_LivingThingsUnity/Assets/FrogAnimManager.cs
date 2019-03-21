﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAnimManager : MonoBehaviour
{

    [SerializeField] SpriteRenderer normalFrog;
    [SerializeField] AudioSource normalFrogAudio;

    [SerializeField] SpriteRenderer rockFrog;
    [SerializeField] AudioSource rockFrogAudio;

    public GameObject activeObject;
    int rocks = 0;
    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        rocks = GetComponentInParent<Frog>().rockCount;
        if (rocks == 0)
        {
            normalFrog.enabled = true;
            rockFrog.enabled = false;
        }

        else
        {
            normalFrog.enabled = false;
            rockFrog.enabled = true;
        }
    }

    public GameObject GetActiveObject()
    {
        return activeObject;
    }
}

