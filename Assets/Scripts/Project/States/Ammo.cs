using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Project.Sensors;
using Project.Pickups;
using EasyAI;

namespace Project.States{
public class Ammo : State
{
    Soldier s;
    List<HealthAmmoPickup> target;//list of ammo pickups
    public override void Enter(Agent agent){
        target = null;
        Debug.Log("Ammo Entered");
    }
    public override void Execute(Agent agent){
        Debug.Log("Ammo Executed");
        s = agent as Soldier;
        if(target != null){//if ammo pickup is selected
            if(target.Count > 0){
                if(!target[0].Ready){//if ammo not ready
                    target = null;
                    agent.SetState<Idle>();
                }
                else{//otherwise, go to ammo
                    agent.Navigate(target[0].transform.position);
                }
            }
        }
        else{//if ammo pickup not yet selected
            target = agent.SenseAll<NearestAmmoPickupSensor, HealthAmmoPickup>();
            if(target == null){//no ammo
                agent.SetState<Idle>();
            }
        }   
    }
    public override void Exit(Agent agent){
        target = null;
        Debug.Log("Ammo Exited");}
}
}