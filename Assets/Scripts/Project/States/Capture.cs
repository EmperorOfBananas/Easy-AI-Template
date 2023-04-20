using System.Collections;
using System.Collections.Generic;
using EasyAI;
using Project.Pickups;
using UnityEngine;
namespace Project.States{
public class Capture : State
{
    Soldier s;
    Vector3 dir;
    int dist;
    public override void Enter(Agent agent){
        Debug.Log("Capture Entered");
        s = agent as Soldier;
    }
    public override void Execute(Agent agent){
        if(!s.CarryingFlag){
            agent.Navigate(s.EnemyFlagPosition);
        }
        else{
            agent.SetState<Retrieve>();
        }
        /*else if(!s.Alive){
            agent.SetState<Idle>();
        }*/     
    }
    public override void Exit(Agent agent){Debug.Log("Capture Exited");}
}
}
