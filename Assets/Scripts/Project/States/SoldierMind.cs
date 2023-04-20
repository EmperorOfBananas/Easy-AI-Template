using EasyAI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Pickups;

namespace Project.States
{
    /// <summary>
    /// The global state which soldiers are always in.
    /// </summary>
    [CreateAssetMenu(menuName = "Project/States/Soldier Mind", fileName = "Soldier Mind")]
    public class SoldierMind : State
    {
        Soldier s;
        Vector3 dir;
        int dist;
        public override void Execute(Agent agent)
        {
            s = agent as Soldier;
            // TODO - Project - Create unique behaviours for your soldiers to play capture the flag.
            /*if(s.Role == Soldier.SoliderRole.Collector){
        
            }
        else if(s.Role == Soldier.SoliderRole.Attacker){
            dir = agent.transform.position - s.EnemyFlagPosition;
            dist = Mathf.RoundToInt(dir.magnitude);
            if(s.DetectedEnemies.Count > 0 && s.Health > 50){
                Soldier.EnemyMemory target = s.DetectedEnemies.OrderBy(e => e.Visible).ThenBy(e => Vector3.Distance(agent.transform.position, e.Position)).First();
                s.SetTarget(new(){Enemy = target.Enemy, Position = target.Position, Visible = target.Visible});
                agent.SetState<Attack>();
            }
            else if(s.Health <= 50){agent.SetState<Heal>();}
            else if(s.Weapons[1].Ammo < s.Weapons[1].Ammo/2){agent.SetState<Ammo>();}*/
            //else{
                agent.SetState<Explore>();
            //}   
        //}
        //else if(s.Role == Soldier.SoliderRole.Defender){

        //}
        }
    }
}