using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using EasyAI;
namespace Project.States{
public class Explore : State
{
    Soldier s;
    bool defensive;
    Vector3 target;
    public override void Enter(Agent agent){
        s = agent as Soldier;
        defensive = false;
        if(s.Role == Soldier.SoliderRole.Defender){defensive=true;}
        //SoldierManager.ReleaseStrategicPosition(s, agent.transform.position, defensive);
        target = SoldierManager.RandomStrategicPosition(s, defensive);
        Debug.Log("Explore Entered");
    }
    public override void Execute(Agent agent){
        Debug.Log("Explore Executed");
        if(agent.transform.position != target){
            agent.Navigate(target);
        }
        else{
            agent.SetState<Idle>();
        }
    }
    public override void Exit(Agent agent){
        Debug.Log("Explore Exited");}
}
}