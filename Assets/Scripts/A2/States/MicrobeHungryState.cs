using EasyAI;
using UnityEngine;
using A2.Sensors;
using EasyAI.Navigation;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace A2.States
{
    /// <summary>
    /// State for microbes that are hungry and wanting to seek food.
    /// </summary>
    [CreateAssetMenu(menuName = "A2/States/Microbe Hungry State", fileName = "Microbe Hungry State")]
    public class MicrobeHungryState : State
    {
        Microbe m;
        List<Microbe> prey; //used to detect the nearest prey
        bool targetSelected; //used to determine if a target has been selected in this state once before, to avoid refering to a prey that might have already been consumed
        public override void Enter(Agent agent)
        {
            // TODO - Assignment 2 - Complete this state. Have microbes search for other microbes to eat.
            m = agent as Microbe;
            targetSelected = false;
            Debug.Log("Hungry state entered");
        }
        
        public override void Execute(Agent agent)
        {
            // TODO - Assignment 2 - Complete this state. Have microbes search for other microbes to eat.
            if(m.HasTarget){// if the microbe is chasing a prey
                if(Vector3.Distance(agent.transform.position, m.TargetMicrobeTransform.position) > MicrobeManager.MicrobeInteractRadius){// if the prey is outside of reach
                    agent.Move(m.TargetMicrobeTransform, Steering.Behaviour.Pursue); // move using pursue behaviour
                    Debug.Log(agent.name + " is moving towards a prey");
                }
                else{ // if the microbe is within reach of prey
                    if(m != null){ // and the microbe has not been killed/has not died
                        m.Eat(); // the microbe eats their target
                        Debug.Log(agent.name + " ate their prey");
                    }
                    agent.SetState<MicrobeRoamingState>();
                }
            }
            else{// if the microbe is not chasing a prey
                if(targetSelected){ // and it has already begun to chase one in this state before
                    targetSelected = false;
                    agent.SetState<MicrobeRoamingState>();// the prey has been destroyed, return to roam
                }
                else{// the microbe has not started to chase any prey in this state
                    prey = agent.SenseAll<NearestPreySensor, Microbe>(); // find nearest prey
                    if(prey.Count > 0){ // if such a prey exists, start hunting it and switch targetSelected to true
                        targetSelected = true;
                        m.StartHunting(prey[0]);
                    }
                    else{//else, there are no prey to hunt
                        Debug.Log(agent.name + " no prey to hunt");
                        agent.SetState<MicrobeRoamingState>(); 
                    }
                }
            }
        }
        
        public override void Exit(Agent agent)
        {
            // TODO - Assignment 2 - Complete this state. Have microbes search for other microbes to eat.
        }
    }
}