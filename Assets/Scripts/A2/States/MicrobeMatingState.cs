using System.Collections;
using System.Collections.Generic;
using EasyAI;
using UnityEngine;
using A2.Sensors;
using System.Linq;

namespace A2.States
{
    /// <summary>
    /// State for microbes that are seeking a mate.
    /// </summary>
    [CreateAssetMenu(menuName = "A2/States/Microbe Mating State", fileName = "Microbe Mating State")]
    public class MicrobeMatingState : State
    {
        Microbe m;
        List<Microbe> mates; // used to detect nearest possible mate
        bool targetSelected; // used to determine if a microbe has already begun to chase a mate once before
        public override void Enter(Agent agent)
        {
            m = agent as Microbe;
            targetSelected = false;
        }
        
        public override void Execute(Agent agent)
        {
            // TODO - Assignment 2 - Complete this state. Have microbes look for mates and reproduce.
            if(m.HasTarget){ // if the microbe is currently chasing a mate
                if(Vector3.Distance(agent.transform.position, m.TargetMicrobeTransform.position) > MicrobeManager.MicrobeInteractRadius){// and it is not within reach
                    agent.Move(m.TargetMicrobeTransform);
                }
                else{// the mate is within reach
                    if(m != null){ // and the microbe still exists
                        m.Mate(); // mate the microbes
                        Debug.Log(agent.name + " has mated successfully");
                    }
                    agent.SetState<MicrobeRoamingState>();
                }
            }
            else{
                if(targetSelected){ // if a target has been selected but the microbe object has no target, then potential mate has been destroyed
                    targetSelected = false;
                    agent.SetState<MicrobeRoamingState>();
                }
                else{// otherwise, no mate has yet been found, look for one
                    mates = agent.SenseAll<NearestMateSensor, Microbe>();
                    if(mates.Count > 0){
                        m.AttractMate(mates[0]);
                        if(m.HasTarget){// if m succeeded in attracting a mate, it will now have a target
                            targetSelected = true;
                            Debug.Log(agent.name + " has attracted a mate");
                        }
                    }
                    else if(mates.Count < 1 || !m.HasTarget){ // no potential mates found
                        agent.SetState<MicrobeRoamingState>();
                    }
                }
                
            }
        }
        
        public override void Exit(Agent agent)
        {
        }
    }
}