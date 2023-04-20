using System.Collections;
using System.Collections.Generic;
using EasyAI;
using UnityEngine;

namespace Project.States{
    public class Attack : State{
        Soldier s;
        Vector3 dir;
        int dist;
        public override void Enter(Agent agent){
            Debug.Log("Attack Entered");
            s = agent as Soldier;
        }
        public override void Execute(Agent agent){
            Debug.Log("Attack Executed");
            if(s.Target != null){
                agent.Navigate(s.Target.Value.Position);
                dir = agent.transform.position - s.Target.Value.Position;
                dist = Mathf.RoundToInt(dir.magnitude);
                //short range
                if(dist <= 10){
                    s.SetWeaponPriority(shotgun:1, machineGun:3, pistol:2, rocketLauncher:4, sniper:5);
                }
                //mid range
                else if(dist <= 30){
                    s.SetWeaponPriority(shotgun:3, machineGun:1, pistol:2, rocketLauncher:4, sniper:5);
                }
                else{
                    s.SetWeaponPriority(shotgun:5, machineGun:3, pistol:4, rocketLauncher:2, sniper:1);
                }
            }
            else{
                s.NoTarget();
                agent.SetState<Idle>();
            }
        }
        public override void Exit(Agent agent){Debug.Log("Attack Exited");}
    }
}
