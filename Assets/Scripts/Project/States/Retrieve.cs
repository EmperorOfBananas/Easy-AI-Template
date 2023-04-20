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
        s = agent as Soldier;
    }
    public override void Execute(Agent agent){
        while(s.CarryingFlag){
            agent.Navigate(s.BasePosition);
        }
        if(!s.CarryingFlag){
            agent.SetState<Idle>();
        }  
    }
    public override void Exit(Agent agent){Debug.Log("Retrieve Exited");}
}
}
