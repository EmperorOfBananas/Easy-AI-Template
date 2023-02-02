using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAI;
using System.Linq;

namespace A1
{
    public class CleanerActuator : Actuator
    {
        Floor target;//tile to be cleaned
        private float time;//used to slowdown the Clean() function
        private float cooldown;//a cooldown for the Clean() function
        private void Start(){
            time = Time.time;//set time equal to current time at start
            cooldown = 0.5f;//cooldown is half a second
            Debug.Log("Actuator Ready");
        }

        public override bool Act(object act)
        { 
            Debug.Log("Actuator Active");
            if(Time.time >= time){//if current time is greater than the set time
                //clean the target
                target = act as Floor;
                target.Clean();
                if(target.State == Floor.DirtLevel.Clean){//if target is clean, return true to end the function of the Actuator
                    return true;
                }
                time = Time.time + cooldown;//set the new value of time to current time + the cooldown
            }
            return false;//return false to continue the function of the Actuator
        }
    }

}
