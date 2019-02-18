﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jesper Li 07/02 - 19
public class Reciever : MonoBehaviour
{

    public enum DoorType
    {
        flip = 0, move, moveTimer
    }
    [SerializeField]
    DoorType doorType;

    //Motion Variables
    [Space]
    [Header("Motion Variables")]
    [SerializeField]
    private Vector3 start;
    [SerializeField]
    private Vector3 target;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float acceleration = 1.1f;
    [SerializeField]
    private float tolerance = 0.15f;

    //Variables for different door states
    [Space]
    [SerializeField][Tooltip("changes state of object, if false at start object will be disabled")]
    private bool gameObjectToggle = true;
    [SerializeField][Tooltip("Cant use button if false")]
    private bool lockBool = true;
    [SerializeField][Tooltip("Wont move if false")]
    private bool moveBool;

    private bool componentToggle;

    [Space]
    [Header("Timed Platform Variables")]
    [SerializeField]
    private bool timerToggle;
    [SerializeField]
    private float timer;

    float timerFloat = 0;

    // Use this for initialization
    void Start ()
    {
        
        start = transform.position;
        ToggleObjectComponents();
    }
    
	// Update is called once per frame
	void Update ()
    {
        if (moveBool)
            ToggleElevator(start, target, speed, acceleration, Time.deltaTime, tolerance, gameObjectToggle);
	}

    public void ToggleObject()
    {
        if (lockBool)
        {
            //gameObjectToggle = !gameObjectToggle;
            

            if (doorType == DoorType.flip)
            {
                gameObjectToggle = !gameObjectToggle;
                ToggleObjectComponents();
                GetComponent<ObjectAudioClip>().PlaySingle(0);
            }

            else if (doorType == DoorType.move)
            {
                GetComponent<ObjectAudioClip>().PlaySingle(0);
                gameObjectToggle = !gameObjectToggle;
            }

            else if (doorType == DoorType.moveTimer)
            {
                timerToggle = !timerToggle;
                
            }
        }
        else
            GetComponent<ObjectAudioClip>().PlaySingle(1);
    }

    public void ToggleObjectComponents()
    {
        if (gameObject.activeInHierarchy)
        {
            GetComponent<BoxCollider2D>().enabled = gameObjectToggle;
            GetComponent<SpriteRenderer>().enabled = gameObjectToggle;
        }
        
    }

    public void BoolToogle()
    {
        if (lockBool && doorType == DoorType.moveTimer)
        {
            timerToggle = !timerToggle;
        }
        moveBool = true;
        lockBool = false;
    }


    private void ToggleElevator(Vector3 start, Vector3 target, float speed, float acceleration,
        float time, float tolerance, bool flip)
    {

        Motion motion = new Motion(start, target, speed, acceleration, time, tolerance);
        motion.Source = transform.position;

        if (flip)
        {
            motion.Target = target;

        }
        else
        {
            motion.Target = start;

        }
        if (doorType != DoorType.flip)
        {
            transform.Translate(motion.SourceToTarget * motion.Velocity * Time.deltaTime);
        }
        

        if (timerToggle && motion.InTargetRegion)
        {

            if (timerFloat < timer && moveBool)
            {
                timerFloat += Time.deltaTime;
            }
            else if (timerFloat >= timer)
            {
                gameObjectToggle = !gameObjectToggle;
                GetComponent<ObjectAudioClip>().PlaySingle(0);
                //timerToggle = false;
                timerFloat = 0;
            }
        }
    }

    public bool GetDoorType(DoorType dT)
    {
        if (dT == doorType)
        {
            return true;
        }
        else
            return false;
    }
}


//public void ToggleObject(bool senderBool)
//{
//    if (toggle)
//    {
//        audioSource.Play();
//        if (gameObjectToggle == false)
//        {
//            gameObjectToggle = senderBool;
//        }
//        else
//            gameObjectToggle = false;
//        if (doorType == DoorType.flip)
//        {
//            gameObject.SetActive(gameObjectToggle);
//        }
//        else if (doorType == DoorType.moveTimer)
//        {
//            timerToggle = true;
//        }
//    }
//}
