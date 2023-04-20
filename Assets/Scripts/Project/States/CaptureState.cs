using System.Collections;
using System.Collections.Generic;
using EasyAI;
using Project.Pickups;
using UnityEngine;
namespace Project.States{
public class CaptureState : State
{
    Soldier s;
    Vector3 dir;
    int dist;
    public override void Enter(Agent agent){
        Debug.Log("Capture Entered");
        s = agent as Soldier;
    }
    public override void Execute(Agent agent){
        Debug.Log("Capture Executed");
        if (!s.CarryingFlag) {
            Debug.Log("Getting flag");
            agent.Navigate(s.EnemyFlagPosition);
        }
        else {
            Debug.Log("Carrying flag, setting Retrieve state");
            agent.SetState<Retrieve>();
        }

        if (!s.Alive) {
            Debug.Log("Agent is dead, setting Idle state");
            agent.SetState<Idle>();
        }
    }
    public override void Exit(Agent agent){
        Debug.Log("Capture Exited");}
}
}
