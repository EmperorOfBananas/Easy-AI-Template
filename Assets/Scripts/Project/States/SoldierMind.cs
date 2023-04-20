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
        public override void Enter(Agent agent)
        {   
            agent.SetState<Idle>();
            // TODO - Project - Create unique behaviours for your soldiers to play capture the flag.
        }
    }
}