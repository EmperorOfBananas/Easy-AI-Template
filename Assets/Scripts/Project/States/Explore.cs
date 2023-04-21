using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Project.Sensors;
using EasyAI;
namespace Project.States{
public class Explore : State
{
    Soldier s;
    bool selected_col, selected_atk, selected_def;
    List<Vector3> offense;
    List<Vector3> defense;
    List<Soldier> partner;
    int dist_col, dist_atk, dist_def;
    Vector3 dir_col, dir_atk, dir_def;
    public override void Enter(Agent agent){
        selected_col = false;
        selected_atk = false;
        selected_def = false;
        Debug.Log("Explore Entered");
    }
    public override void Execute(Agent agent){
        Debug.Log("Explore Executed");
        s = agent as Soldier;
        switch (s.Role) {
            case Soldier.SoliderRole.Collector:
                /*partner = s.SenseAll<TeamSensor, List<Soldier>>().SelectMany(x => x).ToList();
                if(partner.Count > 0){
                    dir_col = agent.transform.position - s.EnemyFlagPosition;
                    dist_col = Mathf.RoundToInt(dir_col.magnitude);
                    if(partner[0].Alive && dist_col > 20){
                        s.Navigate(s.EnemyFlagPosition);
                        partner[0].Navigate(s.EnemyFlagPosition);
                    }
                    else if(!partner[0].Alive){
                        agent.SetState<Idle>();
                    }
                    else if(dist_col <= 20){
                        agent.SetState<Capture>();
                    }
                }
                else{
                    agent.SetState<Idle>();
                }*/
                break;
            case Soldier.SoliderRole.Attacker:
                if(selected_atk){
                    dir_atk = agent.transform.position - offense[0];
                    dist_atk = Mathf.RoundToInt(dir_atk.magnitude);
                    if(dist_atk > 5){
                        agent.Navigate(offense[0]);
                    }
                    else{
                        agent.SetState<Idle>();
                    }
                }
                else{
                    if(s.RedTeam){
                        offense = agent.SenseAll<RandomOffensivePositionSensor, Vector3>();
                    }
                    else if(!s.RedTeam){
                        offense = agent.SenseAll<RandomOffensivePositionSensor, Vector3>();
                    }
                    if(offense.Count <= 0){
                        agent.SetState<Idle>();
                    }
                    selected_atk = true;
                }
                break;
            case Soldier.SoliderRole.Defender:
                if(selected_def){
                    dir_def = agent.transform.position - defense[0];
                    dist_def = Mathf.RoundToInt(dir_def.magnitude);
                    if(dist_def > 5){
                        agent.Navigate(defense[0]);
                    }
                    else{
                        agent.SetState<Idle>();
                    }
                }
                else{
                    if(s.RedTeam){
                        defense = agent.SenseAll<RandomDefensivePositionSensor, Vector3>();
                    }
                    else if(!s.RedTeam){
                        defense = agent.SenseAll<RandomDefensivePositionSensor, Vector3>();
                    }
                    if(defense.Count <= 0){
                        agent.SetState<Idle>();
                    }
                    selected_def = true;
                }
                break;
            default:
                break;
        }
    }
    public override void Exit(Agent agent){
        selected_col = false;
        selected_atk = false;
        selected_def = false;
        Debug.Log("Explore Exited");
    }
}
}