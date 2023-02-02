using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAI;
using System.Linq;

namespace A1
{
    public class CleanerSensor : Sensor
    {
        private void Start(){
            Debug.Log("Sensor Ready");
        }
        public override object Sense()
        {   
            Floor closestFloor = null;//used to store the closestFloor
            Floor dirtiestFloor = null;//used to store the dirtiest floor
            foreach (Floor floor in CleanerManager.Floors.Where(f => f.State != Floor.DirtLevel.Clean))
                {
                    //if closestFloor is null, set it equal to floor
                    //OR
                    //if floor is closer to the agent than closestFloor, change the value of closestFloor to floor
                    if(closestFloor == null || Vector3.Distance(Agent.transform.position, floor.transform.position) < Vector3.Distance(Agent.transform.position, closestFloor.transform.position)){
                        closestFloor = floor;
                    }

                    //set the value of dirtiestFloor to floor if it is null
                    //OR
                    //floor is dirtier than the dirtiestFloor
                    if(dirtiestFloor == null || floor.State > closestFloor.State){
                        dirtiestFloor = floor;
                    }
                }
            
            //If all Floor objects are clean, sense will return null and the AI will remain in the Idle state
            if(closestFloor == null){
                return null;
            }
            //else if, dirtiestFloor is not null
            //AND
            //it is dirtier than the closestFloor
            //Sense() will prioritize this tile because the performance measure gives more weight to the Floor objects depending on dirt level
            //Sense() returns dirtiestFloor
            else if(dirtiestFloor != null && dirtiestFloor.State > closestFloor.State){
                return dirtiestFloor;
            }
            //otherwise, Sense() returns the object stored in closestFloor
            else{
                Debug.Log("Sensor Active");
                return closestFloor;
            }
        }    
    }
}
