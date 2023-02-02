using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAI;

namespace A1.States
{
    //the Clean state is used to clean a tile and reverts back to the Idle state once that function has been completed
    public class Clean : CleanerMind
    {
        Floor target;//floor tile to be cleaned
        public override void Enter(Agent agent){
            target = CleanerMind.target;//set the target equal to the value stored in the CleanerMind
            Debug.Log("Clean Mode Entered");
            }

        public override void Execute(Agent agent){
            Debug.Log("Clean Mode Executed");
            if(target.State == Floor.DirtLevel.Clean){//if the tile is already clean, return to the Idle state
                target = null;
                agent.SetState<Idle>();
            }
            else{//otherwise, call the Actuator
                agent.Act(target);
            }
        }

        public override void Exit(Agent agent){
            Debug.Log("Clean Mode Exited");
        }
    }
}    
