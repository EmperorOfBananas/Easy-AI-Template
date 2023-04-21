using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Project.Sensors;
using Project.Pickups;
using EasyAI;

namespace Project.States{
public class Heal : State
{
    Soldier s;
    List<HealthAmmoPickup> target;
    bool selected;
    public override void Enter(Agent agent){
        selected = false;
        target = null;
        Debug.Log("Heal Entered");
    }
    public override void Execute(Agent agent){
        Debug.Log("Heal Executed");
        s = agent as Soldier;
        if(target != null){
            if(!target[0].Ready){
                target = null;
                agent.SetState<Idle>();
            }
            else{
                agent.Navigate(target[0].transform.position);
            }
        }
        else{
            target = agent.SenseAll<NearestHealthPickupSensor, HealthAmmoPickup>();
            if(target == null){
                agent.SetState<Idle>();
            }
        }   
    }
    public override void Exit(Agent agent){
        Debug.Log("Heal Exited");}
}
}
