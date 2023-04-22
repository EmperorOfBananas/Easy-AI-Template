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
    Vector3 dir;//direction to enemy flag
    int dist;//distance to enemy flag
    public override void Enter(Agent agent){
        Debug.Log("Idle Entered");
    }
    public override void Execute(Agent agent){
        s = agent as Soldier;
        switch (s.Role) {
            case Soldier.SoliderRole.Collector:
                dir = agent.transform.position - s.EnemyFlagPosition;//direction to enemy flag
                dist = Mathf.RoundToInt(dir.magnitude);//distance to enemy flag
                if(!s.CarryingFlag && dist < 35){//if close to enemy flag
                    agent.SetState<Capture>();
                }
                else if(s.Health <= 60 && !s.CarryingFlag){//if health 60 or lower and not carrying flag
                    agent.SetState<Heal>();}
                else if(s.DetectedEnemies.Count > 0 && s.Health > 60 && !s.CarryingFlag){//if enemies detected, health greater than 60, and not carrying flag
                    Soldier.EnemyMemory target = s.DetectedEnemies.OrderBy(e => e.Visible).ThenBy(e => Vector3.Distance(agent.transform.position, e.Position)).First();
                    s.SetTarget(new(){Enemy = target.Enemy, Position = target.Position, Visible = target.Visible});
                    agent.SetState<Attack>();
                }
                else if(s.Weapons[0].Ammo < s.Weapons[0].MaxAmmo/2 && !s.CarryingFlag){//if main gun is half empty
                    agent.SetState<Ammo>();}
                else if(dist > 35){
                    agent.SetState<Explore>();
                }
                break;
            case Soldier.SoliderRole.Attacker:
                if(s.Weapons[0].Ammo < s.Weapons[0].MaxAmmo/2){//if main gun is half empty
                    agent.SetState<Ammo>();}
                else if(s.DetectedEnemies.Count > 0 && s.Health > 50){//if enemies detected, and health greater than 50
                    Soldier.EnemyMemory target = s.DetectedEnemies.OrderBy(e => e.Visible).ThenBy(e => Vector3.Distance(agent.transform.position, e.Position)).First();
                    s.SetTarget(new(){Enemy = target.Enemy, Position = target.Position, Visible = target.Visible});
                    agent.SetState<Attack>();
                }
                else if(s.Health <= 50){//if health is below 50
                    agent.SetState<Heal>();}
                else{//go to strategic offensive point
                    s.Cover = false;//leaving protection to find a better place to shoot
                    agent.SetState<Explore>();
                }
                break;
            case Soldier.SoliderRole.Defender:
                if(!s.FlagAtBase){//if flag is not at base
                    s.Cover = false;//exit cover
                    agent.SetState<Retrieve>();
                }
                else if(s.DetectedEnemies.Count > 0 && s.Health > 30 && s.Weapons[0].Ammo > s.Weapons[0].MaxAmmo/3){//if detected enemies is greater than 0, and health and ammo are at acceptable levels
                    Soldier.EnemyMemory target = s.DetectedEnemies.OrderBy(e => e.Visible).ThenBy(e => Vector3.Distance(agent.transform.position, e.Position)).First();
                    s.SetTarget(new() { Enemy = target.Enemy, Position = target.Position, Visible = target.Visible });
                    agent.SetState<Attack>();
                }    
                else if(s.Health <= 30){//health is low
                    s.Cover = false;//exit cover
                    agent.SetState<Heal>();}
                else if(s.Weapons[0].Ammo < s.Weapons[0].MaxAmmo/3){//ammo is 2/3s out
                    s.Cover = false;//exit cover
                    agent.SetState<Ammo>();}
                else if(!s.Cover){//search for strategic defensive point to take cover
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
