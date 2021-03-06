﻿using UnityEngine;
public class ActivePlayerState : ActivePlayerStateBase
{
    public GameObject snubbe;
    public int playerNumber;
 
    [SerializeField] private string charakterOne = "1";
    [SerializeField] private string charakterTwo = "2";
    [SerializeField] private string charakterThree = "3";
    public ActivePlayerState(ActivePlayerStateMachine stateMachine, GameObject gameObjectPlayer, int number)
    {
        snubbe = gameObjectPlayer;
        if (stateMachines == null)
        {
            stateMachines = stateMachine;
        }

        playerNumber = number;

    }

    public override void UpdateState()
    {

        ChangePlayer();

    }
    public override void Enter()
    {
   
        stateMachines.transform.position = snubbe.transform.position;
       
        stateMachines.transform.parent = snubbe.transform;
        snubbe.GetComponent<PlayerController>().enabled = true;
    }
    public override void Exit()
    {
    
        stateMachines.transform.parent = null;
        snubbe.GetComponent<PlayerController>().enabled = false;



    }
    public override void OnTransision(ActivePlayerStateBase nextState)
    {
    }
    public override void OnTransisionFrom(ActivePlayerStateBase nextState)
    {
    }
    void ChangePlayer()
    {




        if (Input.GetButtonDown(charakterOne) && playerNumber != 1)
        {
            stateMachines.ChangeState(stateMachines.activePlayerStateOtter);

        }
        if (Input.GetButtonDown(charakterTwo) && playerNumber != 2)
        {
            stateMachines.ChangeState(stateMachines.activePlayerStateSeal);
        }
        if (Input.GetButtonDown(charakterThree) && playerNumber != 3)
        {
            stateMachines.ChangeState(stateMachines.activePlayerStateFrog);
        }

    }
}
