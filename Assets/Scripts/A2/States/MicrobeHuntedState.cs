using EasyAI;
using UnityEngine;
using EasyAI.Navigation;

namespace A2.States
{
    /// <summary>
    /// State for microbes that are being hunted.
    /// </summary>
    [CreateAssetMenu(menuName = "A2/States/Microbe Hunted State", fileName = "Microbe Hunted State")]
    public class MicrobeHuntedState : State
    {
        Microbe m;
        public override void Enter(Agent agent)
        {
            // TODO - Assignment 3 - Complete this state. Add the ability for microbes to evade hunters.
            m = agent as Microbe;
        }

        public override void Execute(Agent agent)
        {
            // TODO - Assignment 3 - Complete this state. Add the ability for microbes to evade hunters.
            if(m.BeingHunted && Vector3.Distance(agent.transform.position, m.Hunter.transform.position) < m.DetectionRange){// if microbe is being hunted and their hunter is within detection range
                agent.Move(m.Hunter.transform, Steering.Behaviour.Evade); // microbe will evade
            }
            else{//otherwise, no longer being hunted or seemingly a safe distance away
                agent.SetState<MicrobeRoamingState>(); // return to roam
            }
        }
        
        public override void Exit(Agent agent)
        {

        }
    }
}