using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAI;

namespace A1.States
{
    //The Move State is when the AI has selected a target and has not yet arrived at that destination
    public class Move : CleanerMind
    {
        Transform goal;//transform component of the target object
        public override void Enter(Agent agent){
            Debug.Log("Move Mode Entered");
            goal = CleanerMind.target.transform;//read target value and set to goal
            Debug.Log("Moving to floor position: " + goal.position);
            }

        public override void Execute(Agent agent){
            if(Vector3.Distance(agent.transform.position, goal.position) > 1){//if the agent is not at the goal position, Move() is imprecise so an agent has arrived if it is within 1 dist. of the goal
                agent.Move(goal);//move to the goal position
            }
            else{//else, if agent has arrive
                Debug.Log("Arrived at destination");
                agent.SetState<Clean>();//set state to Clean
            }
            }

        public override void Exit(Agent agent){
            Debug.Log("Move Mode Exited");
        }
    }
}
