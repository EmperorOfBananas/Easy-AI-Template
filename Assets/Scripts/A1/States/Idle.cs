using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAI;

namespace A1.States
{
    //The Idle State is the state where the AI has not yet locked on a target or is not currently cleaning a target
    public class Idle : CleanerMind
    {
        public override void Enter(Agent agent){
            Debug.Log("Idle Mode Entered");
        }

        public override void Execute(Agent agent){
            List<Floor> data = agent.SenseAll<CleanerSensor, Floor>();//used to read data from sensor

            if(data.Count != 0){//if sensor did not return null
                Debug.Log("Floor is Dirty, closest floor position: " + data[0].transform.position);
                CleanerMind.target = data[0];//set target equal to data[0]
                agent.SetState<Move>();//switch to Move State
            }
            else{
                Debug.Log("Floor is Clean");
            }
        }

        public override void Exit(Agent agent){
            Debug.Log("Idle Mode Exited");
        }
    }
}
