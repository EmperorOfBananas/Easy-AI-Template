using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Pickups;
using UnityEngine;
using EasyAI;

namespace Project.States{
public class Idle : State
{
    Soldier s;
    Vector3 dir;
    int dist;
    bool defensive;
    Vector3 target;
    public override void Enter(Agent agent){
        s = agent as Soldier;
        defensive = false;
        Debug.Log("Idle Entered");
    }
    public override void Execute(Agent agent){
        Debug.Log("Idle Executed");
        /*if(s.Role == Soldier.SoliderRole.Collector){
            if(!s.CarryingFlag){
                agent.SetState<Heal>();
            }
        }
            //agent.SetState<Capture>();
            dir = s.BasePosition*(-1) - agent.transform.position;
            dist = Mathf.RoundToInt(dir.magnitude);
            if(dist > 10){
                agent.Navigate(s.EnemyFlagPosition);
            }
            else{
                agent.Navigate(s.EnemyFlagPosition);
            }
            //Sagent.Navigate(s.EnemyFlagPosition);
        }*/
        /*if(s.Role == Soldier.SoliderRole.Collector){
            Debug.Log("test 1");
        }*/
        if(s.Role == Soldier.SoliderRole.Attacker){
            //Debug.Log(agent.name + " is a " + s.Role);
            dir = agent.transform.position - s.EnemyFlagPosition;
            dist = Mathf.RoundToInt(dir.magnitude);
            if(s.DetectedEnemies.Count > 0 && s.Health > 50){
                Debug.Log("test 1");
                Soldier.EnemyMemory target = s.DetectedEnemies.OrderBy(e => e.Visible).ThenBy(e => Vector3.Distance(agent.transform.position, e.Position)).First();
                s.SetTarget(new(){Enemy = target.Enemy, Position = target.Position, Visible = target.Visible});
                agent.SetState<Attack>();
            }
            else if(s.Health <= 50){
                Debug.Log("test 2");
                agent.SetState<Heal>();}
            else if(s.Weapons[1].Ammo < s.Weapons[1].Ammo/2){
                Debug.Log("test 3");
                agent.SetState<Ammo>();}
            else{
                if(agent.transform.position != target){
                    agent.Navigate(target);
                }
                else{
                    target = SoldierManager.RandomStrategicPosition(s, false);
                }
            }   
        }
        /*else if(s.Role == Soldier.SoliderRole.Defender){
            Debug.Log("test 2");
        }*/
    }
    public override void Exit(Agent agent){
        Debug.Log("Idle Exited");}
}
}
