using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAI;
using System.Linq;
using Project.Pickups;
using Project;
namespace Project.Sensors{
    public class TeamSensor : Sensor
    {
        /// <summary>
        /// Sense team member closest to enemy flag.
        /// </summary>
        /// <returns>A random offensive position.</returns>
        public override object Sense()
        {
            if(Agent is not Soldier s){
                return null;
            }

            IEnumerable<Soldier> team = (s.RedTeam ? SoldierManager.TeamRed : SoldierManager.TeamBlue).Where(s => s.Alive);
            if(s.RedTeam)
            {
                if (FlagPickup.BlueFlag != null)
                {
                    team = team.OrderBy(s => Vector3.Distance(s.transform.position, FlagPickup.BlueFlag.transform.position));
                }
            }
            else{
                if (FlagPickup.RedFlag != null)
                {
                    team = team.OrderBy(s => Vector3.Distance(s.transform.position, FlagPickup.RedFlag.transform.position));
                }
            }

            return team.ToArray();
        }
    }
}