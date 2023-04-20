using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Pickups;
using UnityEngine;
using UnityEngine.AI;
using EasyAI;

namespace Project.States{
public class Idle : State
{
    Soldier s;
    Vector3 dir;
    int dist;
    //private NavMeshAgent nav;
    //private GameObject go;
    public override void Enter(Agent agent){
        s = agent as Soldier;
        //nav = agent.GetComponent<NavMeshAgent>();
        Debug.Log("Idle Entered");
    }
    public override void Execute(Agent agent){
        if(s.Role == Soldier.SoliderRole.Collector && !s.CarryingFlag /*&& ((s.RedTeam && FlagPickup.BlueFlag != null) || (!s.RedTeam && FlagPickup.RedFlag != null))*/){
            //go.transform.position = s.EnemyFlagPosition;
            //nav.SetDestination(s.EnemyFlagPosition);   
        }
            //agent.SetState<Capture>();
            /*dir = s.BasePosition*(-1) - agent.transform.position;
            dist = Mathf.RoundToInt(dir.magnitude);
            if(dist > 10){
                agent.Navigate(s.EnemyFlagPosition);
            }
            else{
                agent.StopNavigating();
                agent.Navigate(s.EnemyFlagPosition);
            }
            //Sagent.Navigate(s.EnemyFlagPosition);
        }
        /*if(s.DetectedEnemies.Count > 0){
            Soldier.EnemyMemory target = s.DetectedEnemies.OrderBy(e => e.Visible).ThenBy(e => Vector3.Distance(agent.transform.position, e.Position)).First();
            s.SetTarget(new(){Enemy = target.Enemy, Position = target.Position, Visible = target.Visible});
            agent.SetState<Attack>();
        }
        if(s.Weapons[1].Ammo < s.Weapons[1].Ammo/2){
            agent.SetState<Ammo>();
        }
        else if(s.Health <= 50){
            agent.SetState<Heal>();
        }
        else if(!s.CarryingFlag){
            //agent.Navigate(s.EnemyFlagPosition);
            dir = s.BasePosition*(-1) - agent.transform.position;
            dist = Mathf.RoundToInt(dir.magnitude);
            if(dist > 5){
                agent.Navigate(s.BasePosition*(-1));
            }
            else{
                agent.Navigate(s.EnemyFlagPosition);
            }
        }*/    
    }
    public override void Exit(Agent agent){Debug.Log("Idle Exited");}
}
}
