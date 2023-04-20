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
    List<HealthAmmoPickup> target;
    bool selected;
    public override void Enter(Agent agent){
        s = agent as Soldier;
        selected = false;
        target = null;
        Debug.Log("Ammo Entered");
    }
    public override void Execute(Agent agent){
        if(target != null){
            if(!target[0].Ready){
                target = null;
                agent.SetState<Idle>();
            }
            agent.Navigate(target[0].transform.position);
        }
        else{
            target = agent.SenseAll<NearestAmmoPickupSensor, HealthAmmoPickup>();
            if(target == null){
                agent.SetState<Idle>();
            }
        }   
    }
    public override void Exit(Agent agent){Debug.Log("Ammo Exited");}
}
}