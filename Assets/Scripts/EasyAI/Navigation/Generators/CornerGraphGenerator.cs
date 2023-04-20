using UnityEngine;
using System;

namespace EasyAI.Navigation.Generators
{
    /// <summary>
    /// Convex corner graph placement for nodes.
    /// </summary>
    public class CornerGraphGenerator : NodeGenerator
    {
        [SerializeField]
        [Min(0)]
        [Tooltip("How far away from corners should the nodes be placed.")]
        private int cornerNodeSteps = 3;
        public int x;//Length of NodeArea
        public int z;//Width of NodeArea
    
        /// <summary>
        /// Place nodes at convex corners.
        /// </summary>
        public override void Generate()
        {
            // TODO - Assignment 4 - Complete corner-graph node generation.

            x = NodeArea.RangeX;
            z = NodeArea.RangeZ;
            for(int i = 0; i < x; i++){
                for(int j = 0; j < z; j++){
                    if(j > 0 && j < z - 1){//if within the boundaries of RangeZ
                        if(NodeArea.IsOpen(i, j) && !NodeArea.IsOpen(i, j-1)){//current area is open and last one was closed
                            if(isConvex(i, j-1)){//check if last area was a convex corner
                                Place(i, j - 1);//calculate where the node for this corner should be
                            }
                        }
                        if(!NodeArea.IsOpen(i, j) && NodeArea.IsOpen(i, j-1)){//current area is closed and last one was open
                            if(isConvex(i, j)){//check if current area is a convex corner
                                Place(i, j);//calculate where the node for this corner should be
                            }
                        }
                    }
                }
            }
        }

        //Takes the position of a potential convex corner and return true if it is so or false if otherwise
        public bool isConvex(int cur_x, int cur_z){
            int convex_check = 0;
            //if there is a direction on the vertical axis that is open, 
            //it will mean that this area has two open areas adjacent to each other.
            //this would make it a convex corner
            if(0 < cur_x){
                if(NodeArea.IsOpen(cur_x - 1, cur_z)){convex_check++;}
            }     
            if(cur_x < x - 1){
                if(NodeArea.IsOpen(cur_x + 1, cur_z)){convex_check++;}
            }
            
            if(convex_check > 0){
                return true;
            }
            else{
                return false;
            }
        }

        
        //Takes the position of a corner and calculates a new position to place the node for that corner
        public void Place(int corner_x, int corner_z){
            int adj1, adj2;
            //if a direction leading away from the corner is clear, node_x will go 4 positions towards that direction
            if(NodeArea.IsOpen(corner_x - 1, corner_z)){
                adj1 = corner_x - 1;
            }
            else{
                adj1 = corner_x + 1;
            }
            //same as above
            if(NodeArea.IsOpen(corner_x, corner_z - 1)){
                adj2 = corner_z - 1;
            }
            else{
                adj2 = corner_z + 1;
            }

            for(int i = 0; i <= cornerNodeSteps; i++){

            }
            //add node at position (node_x, node_z)
            NodeArea.AddNode(node_x, node_z);
        }
    }
}