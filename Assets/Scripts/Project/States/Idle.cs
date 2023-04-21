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
        defensive = false;
        Debug.Log("Idle Entered");
    }
    public override void Execute(Agent agent){
        s = agent as Soldier;
        dir = agent.transform.position - s.EnemyFlagPosition;
        dist = Mathf.RoundToInt(dir.magnitude);
        switch (s.Role) {
            case Soldier.SoliderRole.Collector:
                Debug.Log("agent: " + s.name + " is a collector");
                break;
            case Soldier.SoliderRole.Attacker:
                if(s.DetectedEnemies.Count > 0 && s.Health > 50){
                    Soldier.EnemyMemory target = s.DetectedEnemies.OrderBy(e => e.Visible).ThenBy(e => Vector3.Distance(agent.transform.position, e.Position)).First();
                    s.SetTarget(new(){Enemy = target.Enemy, Position = target.Position, Visible = target.Visible});
                    agent.SetState<Attack>();
                }
                else if(s.Health <= 50){
                    agent.SetState<Heal>();}
                else if(s.Weapons[1].Ammo < s.Weapons[1].Ammo/2){
                    agent.SetState<Ammo>();}
                else{
                    agent.SetState<Explore>();
                }
                break;
            case Soldier.SoliderRole.Defender:
                if(s.DetectedEnemies.Count > 0 && s.Health > 30){
                    Soldier.EnemyMemory target = s.DetectedEnemies.OrderBy(e => e.Visible).ThenBy(e => Vector3.Distance(agent.transform.position, e.Position)).First();
                    s.SetTarget(new(){Enemy = target.Enemy, Position = target.Position, Visible = target.Visible});
                    agent.SetState<Attack>();
                }
                else if(s.Health <= 30){
                    agent.SetState<Heal>();}
                else if(s.Weapons[1].Ammo < s.Weapons[1].Ammo/3){
                    agent.SetState<Ammo>();}
                else{
                    agent.SetState<Explore>();
                }
                break;
            default:
                break;
        }
    }
    public override void Exit(Agent agent){
        Debug.Log("Idle Exited");}
}
}
