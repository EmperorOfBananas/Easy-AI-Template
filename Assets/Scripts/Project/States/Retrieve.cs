using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Pickups;
using EasyAI;
using System.Linq;

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
        if(s.CarryingFlag && s.FlagAtBase){//if soldier has enemy flag
            Soldier.EnemyMemory target = s.DetectedEnemies.OrderBy(e => e.Visible).ThenBy(e => Vector3.Distance(agent.transform.position, e.Position)).FirstOrDefault();
            if(target != null){
                s.SetTarget(new(){Enemy = target.Enemy, Position = target.Position, Visible = target.Visible});
            }
            agent.Navigate(s.BasePosition);//go to base
        }
        else if(!s.FlagAtBase){//if team flag not at base
            Soldier.EnemyMemory target = s.DetectedEnemies.OrderBy(e => e.HasFlag).ThenBy(e => e.Visible).FirstOrDefault();//search detected enemies for flag
            if(target != null){//if enemy found, set target and attack
                s.SetTarget(new(){Enemy = target.Enemy, Position = target.Position, Visible = target.Visible});
                agent.SetState<Attack>();
            }
            else{//else, navigate to team flag position
                agent.Navigate(s.TeamFlagPosition);
            }
        }
        else{//otherwise, return to idle
            agent.SetState<Idle>();
        }  
    }
    public override void Exit(Agent agent){
        Debug.Log("Retrieve Exited");}
}
}
