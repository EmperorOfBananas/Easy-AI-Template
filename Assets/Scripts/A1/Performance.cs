using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyAI;
using System.Linq;

namespace A1
{
    public class Performance : PerformanceMeasure
    {
        private float measure;//the value each individual Dirtlevel is worth in the peformance measure
        private float performance;//the value of the performance measure
        private float floor_size;//the number of tiles
        void Start()
        {
            floor_size = (float)CleanerManager.Floors.Count;
            measure = (100f/floor_size)/3f;//divide 100 by floor_size to get the worth of each individual tile, then by 3 to get the worth of each dirt level
            Debug.Log("Performance Measure Ready"); 
        }

        public override float CalculatePerformance(){
            Debug.Log("Calculating Performance Measure");
            performance = 0.0f;//set performance equal to 0
            //increase performance by m*measure for each floor, where m is a multiplier that awards more points to the performance measure depending on how clean the tile is
            //if a tile is extremely dirty, that tile contributes 0.0f points to the performance measure
            foreach (Floor floor in CleanerManager.Floors.Where(f => f.State != Floor.DirtLevel.ExtremelyDirty)){
                if(floor.State == Floor.DirtLevel.Clean){
                    performance += 3*measure;
                }
                else if(floor.State == Floor.DirtLevel.Dirty){
                    performance += 2*measure;
                }
                else if(floor.State == Floor.DirtLevel.VeryDirty){
                    performance += 1*measure;
                }
            }
            return performance;
        }
    }
}