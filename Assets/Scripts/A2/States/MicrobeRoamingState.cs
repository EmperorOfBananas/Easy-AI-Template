using EasyAI;
using UnityEngine;
using A2.Sensors;
using System.Collections;
using A2.Pickups;
using System.Collections.Generic;

namespace A2.States
{
    /// <summary>
    /// Roaming state for the microbe, doesn't have any actions and only logs messages.
    /// </summary>
    [CreateAssetMenu(menuName = "A2/States/Microbe Roaming State", fileName = "Microbe Roaming State")]
    public class MicrobeRoamingState : State
    {
        Microbe m;
        public override void Enter(Agent agent)
        {
            // TODO - Assignment 2 - Complete this state. Have microbes randomly roam around.
            m = agent as Microbe;
            Debug.Log("Roaming State entered");
        }

        public override void Execute(Agent agent)
        {
            // TODO - Assignment 2 - Complete this state. Have microbes randomly roam around.
            if(m.BeingHunted){// if the microbe is being hunted
                Debug.Log(agent.name + " is being hunted");
                agent.SetState<MicrobeHuntedState>();
            }
            else if(m.IsHungry){// microbe is safe but hungry
                Debug.Log(agent.name + " started hunting a prey");
                agent.SetState<MicrobeHungryState>();
            }
            else if(!m.DidMate && m.IsAdult){//microbe is safe, hungry, grown, and ready to mate
                Debug.Log(agent.name + " is looking for a mate");
                agent.SetState<MicrobeMatingState>();
            }
            else{// microbe has nothing else to do other than look for pickups
                Debug.Log(agent.name + " is looking for a pickup");
                agent.SetState<MicrobeSeekingPickupState>();
            }
        }
        
        public override void Exit(Agent agent)
        {
            // TODO - Assignment 2 - Complete this state. Have microbes randomly roam around.
            Debug.Log("Roam Exit");
        }
    }
}