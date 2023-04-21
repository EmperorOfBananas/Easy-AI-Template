using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Pickups;
using EasyAI;

namespace Project.States{
public class Retrieve : State
{
    Soldier s;
    public override void Enter(Agent agent){
        Debug.Log("Retrieve Entered");
    }
    public override void Execute(Agent agent){
        Debug.Log("Retrieve Executed");
        s = agent as Soldier;
        if(s.CarryingFlag){
            agent.Navigate(s.BasePosition);
        }
        else{
            agent.SetState<Idle>();
        }  
    }
    public override void Exit(Agent agent){
        Debug.Log("Retrieve Exited");}
}
}
