﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {
    //Jonas Thunberg 2019-02-26
    public static EventManager instance;
    //public delegate void events(GameObject n);
    //public static events onKilled;

    public System.Action<GameObject> onKilld;
    


    private void Awake()
    {
        instance = this;

    }
}
