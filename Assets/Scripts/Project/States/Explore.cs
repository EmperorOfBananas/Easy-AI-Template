using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using EasyAI;
namespace Project.States{
public class Explore : State
{
    Soldier s;
    Vector3 target;
    public override void Enter(Agent agent){
        s = agent as Soldier;
        target = SoldierManager.RandomStrategicPosition(s, false);
        Debug.Log("Explore Entered");
    }
    public override void Execute(Agent agent){
        Debug.Log("Explore Executed");
        if(s.Role == Soldier.SoliderRole.Attacker){
            if(agent.transform.position != target){
                agent.Navigate(target);
            }
            else{
                agent.SetState<SoldierMind>();
            }
        }
    }
    public override void Exit(Agent agent){
        agent.StopNavigating();
        Debug.Log("Explore Exited");}
}
}