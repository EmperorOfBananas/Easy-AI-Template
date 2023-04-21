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
            //dir = agent.transform.position - s.EnemyFlagPosition;
            //dist = Mathf.RoundToInt(dir.magnitude);
            agent.Navigate(s.EnemyFlagPosition);
        }
        else{
            agent.SetState<Retrieve>();
        }
    }
    public override void Exit(Agent agent){
        Debug.Log("Capture Exited");}
}
}
