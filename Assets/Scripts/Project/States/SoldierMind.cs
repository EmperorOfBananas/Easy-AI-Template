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
        //Soldier s;
        public override void Enter(Agent agent)
        {   
            //s = agent as Soldier;
            agent.SetState<Idle>();
        }
        public override void Execute(Agent agent)
        {   
            
        }
        public override void Exit(Agent agent)
        {   
            
        }
    }
}