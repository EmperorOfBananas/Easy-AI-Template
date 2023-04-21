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
    }
    public override void Execute(Agent agent){
        Debug.Log("Capture Executed");
        s = agent as Soldier;
        if(!s.CarryingFlag) {
            Debug.Log("Getting flag");
            agent.Navigate(s.EnemyFlagPosition);
        }
        else{
            Debug.Log("Carrying flag, setting Retrieve state");
            agent.SetState<Retrieve>();
        }
    }
    public override void Exit(Agent agent){
        Debug.Log("Capture Exited");}
}
}
