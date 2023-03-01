using EasyAI;
using UnityEngine;
using A2.Pickups;
using A2.Sensors;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace A2.States
{
    /// <summary>
    /// State for microbes that are seeking a pickup.
    /// </summary>
    [CreateAssetMenu(menuName = "A2/States/Microbe Seeking Pickup State", fileName = "Microbe Seeking Pickup State")]
    public class MicrobeSeekingPickupState : State
    {
        Microbe m;
        List<MicrobeBasePickup> pickups; // used to detect pickups
        bool targetSelected; // used to detect if microbe has already begun to chase after a pickup
        public override void Enter(Agent agent)
        {
            // TODO - Assignment 2 - Complete this state. Have microbes look for pickups.
            m = agent as Microbe;
            targetSelected = false;
            Debug.Log("Seek pickup enter");
        }
        
        public override void Execute(Agent agent)
        {
            // TODO - Assignment 2 - Complete this state. Have microbes look for pickups.
            if(m.HasPickup){// if the microbe is currently chasing a pickup
                if(Vector3.Distance(agent.transform.position, m.Pickup.transform.position) > MicrobeManager.MicrobeInteractRadius){// and it is out of reach
                    agent.Move(m.Pickup.transform); // move to the pickup
                }
            }
            else{// microbe is not chasing a pickup
                if(targetSelected){// it has previously begun the chase of a pickup, therefore we conclude that the pickup has been taken by another microbe
                    targetSelected = false;
                    agent.SetState<MicrobeRoamingState>();
                }
                else{// microbe has not yet chosen a pickup to chase
                    pickups = agent.SenseAll<NearestPickupSensor, MicrobeBasePickup>();// scan for pickups
                    if(pickups.Count > 0){// if there are any pickups
                        if(Vector3.Distance(agent.transform.position, pickups[0].transform.position) > m.DetectionRange){// if the nearest one is outside detection range
                            agent.SetState<MicrobeRoamingState>();
                        }
                        else{//if the nearest one is within detection range
                            Debug.Log(agent.name + " heading towards pickup");
                            targetSelected = true; // microbe has begun to chase after a pickup
                            m.SetPickup(pickups[0]);
                        }
                    }
                    else{
                        agent.SetState<MicrobeRoamingState>();
                    } 
                }
            }
        }
        
        public override void Exit(Agent agent)
        {
            // TODO - Assignment 2 - Complete this state. Have microbes look for pickups.
            Debug.Log("No longer seeking pickup");
        }
    }
}