using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAI;
using System.Linq;

namespace A1.States
{
    /// <summary>
    /// The global state which the cleaner is always in.
    /// </summary>
    [CreateAssetMenu(menuName = "A1/States/Cleaner Mind", fileName = "Cleaner Mind")]
    public class CleanerMind : State
    {
        public static Floor target = null; //used to store the floor object selected by the sensor in the Idle state
        public override void Enter(Agent agent){
            agent.SetState<Idle>();// initiate in the Idle state
            Debug.Log("Mind Entered");
        }

        public override void Execute(Agent agent)
        {
            // TODO - Assignment 1 - Complete the mind of this agent along with any sensors and actuators you need.
        }

        public override void Exit(Agent agent){
            Debug.Log("Mind Exited");
        }
    }
}