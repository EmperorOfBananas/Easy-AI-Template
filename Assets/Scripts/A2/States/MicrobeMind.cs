using EasyAI;
using UnityEngine;
using A2.Sensors;
using System.Collections;
using A2.Pickups;
using System.Collections.Generic;

namespace A2.States
{
    /// <summary>
    /// The global state which microbes are always in.
    /// </summary>
    [CreateAssetMenu(menuName = "A2/States/Microbe Mind", fileName = "Microbe Mind")]
    public class MicrobeMind : State
    {
        public override void Enter(Agent agent){
            agent.SetState<MicrobeRoamingState>();
            Debug.Log("Mind Entered");
        }
        public override void Execute(Agent agent)
        {
            
        }
        public override void Exit(Agent agent)
        {
            Debug.Log("Mind Exited");
        }
    }
}