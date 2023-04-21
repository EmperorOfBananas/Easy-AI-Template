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
    List<HealthAmmoPickup> target;//list of health pickups
    bool selected;
    public override void Enter(Agent agent){
        selected = false;
        target = null;
        Debug.Log("Heal Entered");
    }
    public override void Execute(Agent agent){
        Debug.Log("Heal Executed");
        s = agent as Soldier;
        if(target != null){//if health pickup not found
            if(!target[0].Ready){//if pickup not ready
                target = null;
                agent.SetState<Idle>();
            }
            else{//otherwise, travel to pickup
                agent.Navigate(target[0].transform.position);
            }
        }
        else{//find pickup
            target = agent.SenseAll<NearestHealthPickupSensor, HealthAmmoPickup>();
            if(target.Count == 0){//no pickup found
                agent.SetState<Idle>();
            }
        }   
    }
    public override void Exit(Agent agent){
        Debug.Log("Heal Exited");}
}
}
