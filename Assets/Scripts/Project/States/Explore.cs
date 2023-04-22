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
    bool selected_col, selected_atk, selected_def;//used to indicate that a target for the exploration, or a partner for the collector has been found
    List<Vector3> offense;//list of offense points
    List<Vector3> defense;//list of defense points
    Soldier partner;//escort of the collector agent
    int dist_col, dist_atk, dist_def;//distance between soldier and their destination
    Vector3 dir_col, dir_atk, dir_def;//direction a soldier is heading in
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
                if(selected_col){
                    if(partner != null){
                        dir_col = agent.transform.position - s.EnemyFlagPosition;
                        dist_col = Mathf.RoundToInt(dir_col.magnitude);
                        if(partner.Alive && dist_col >= 35){
                            s.Navigate(partner.transform.position);
                        }
                        else if(dist_col < 35){
                            agent.SetState<Capture>();
                        }
                        else if(!partner.Alive){
                            agent.SetState<Idle>();
                        }
                    }
                }
                else{
                    partner = SoldierManager.TeamBlue.OrderBy(b => Vector3.Distance(b.transform.position, s.transform.position)).FirstOrDefault();
                    if(partner != null){
                        selected_col = true;
                    }
                }
                break;
            case Soldier.SoliderRole.Attacker:
                if(selected_atk){//offense point selected
                    dir_atk = agent.transform.position - offense[0];//direction to point
                    dist_atk = Mathf.RoundToInt(dir_atk.magnitude);//distance to point
                    if(dist_atk > 5){
                        agent.Navigate(offense[0]);//move to offensive position
                    }
                    else{
                        agent.SetState<Idle>();
                    }
                }
                else{//offense point not selected
                    offense = agent.SenseAll<RandomOffensivePositionSensor, Vector3>();
                    if(offense.Count <= 0){//no offensive points found
                        agent.SetState<Idle>();
                    }
                    selected_atk = true;
                }
                break;
            case Soldier.SoliderRole.Defender:
                if(selected_def){//defensive point selected
                    dir_def = agent.transform.position - defense[0];//direction to point
                    dist_def = Mathf.RoundToInt(dir_def.magnitude);//distance to point
                    if(dist_def > 5){
                        agent.Navigate(defense[0]);//move to defensive position
                    }
                    else{
                        s.Cover = true;//take cover
                        agent.SetState<Idle>();
                    }
                }
                else{//defense point not selected
                    defense = agent.SenseAll<RandomDefensivePositionSensor, Vector3>();
                    if(defense.Count > 0){//no defensive points found
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