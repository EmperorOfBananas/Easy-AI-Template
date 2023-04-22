using System.Collections;
using System.Collections.Generic;
using EasyAI;
using Project.Pickups;
using System.Linq;
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
        if(s.Role != Soldier.SoliderRole.Collector){//if not collector
            agent.SetState<Idle>();
        }
        if(!s.CarryingFlag) {//if not in possession of flag
            Soldier.EnemyMemory target = s.DetectedEnemies.OrderBy(e => e.Visible).ThenBy(e => Vector3.Distance(agent.transform.position, e.Position)).FirstOrDefault();
            if(target != null){
                s.SetTarget(new(){Enemy = target.Enemy, Position = target.Position, Visible = target.Visible});
            }
            agent.Navigate(s.EnemyFlagPosition);//go to enemy flag
        }
        else{//if flag captured
            agent.SetState<Retrieve>();//go to retrieve
        }
    }
    public override void Exit(Agent agent){
        Debug.Log("Capture Exited");}
}
}
