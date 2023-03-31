using System.Collections.Generic;
using EasyAI.Navigation.Nodes;
using System.Linq;
using System;
using UnityEngine;

namespace EasyAI.Navigation
{
    /// <summary>
    /// A* pathfinding.
    /// </summary>
    public static class AStar
    {
        /// <summary>
        /// Perform A* pathfinding.
        /// </summary>
        /// <param name="current">The starting position.</param>
        /// <param name="goal">The end goal position.</param>
        /// <param name="connections">All node connections in the scene.</param>
        /// <returns>The path of nodes to take to get from the starting position to the ending position.</returns>
        public static List<Vector3> Perform(Vector3 current, Vector3 goal, List<Connection> connections)
        {
            // TODO - Assignment 4 - Implement A* pathfinding.
            List<Vector3> closed = new List<Vector3>();//store the path that has been taken
            List<AStarNode> open = new List<AStarNode>();//opened nodes
            List<AStarNode> suc = new List<AStarNode>();//successor nodes
            AStarNode select = new AStarNode(current, goal);//current node
            suc = getSuccessors(select, goal, connections);
            open = open.Concat(suc).ToList();//merge open and successors lists
            select.Close();
            closed.Add(select.Position);//add vector position to closed 
            
            //code below not working path gen takes forever
            /*while(select.Position != goal){//if the select node != goal
                select = visit(suc);
                //Errors occurring in the code below
                //path generation takes forever
                if(suc.Count() > 0){//if there are successors
                    select = visit(suc);
                }
                else{//if there are no successors
                    select = visit(open);  
                }
                select.Close();
                closed.Add(select.Position);
                suc = getSuccessors(select, goal, connections);
                open = open.Concat(suc).ToList();
            }*/
            return closed;
        }
    
        //this function takes an AStarNode, goal vector, and the Connections list, and outputs a 
        //list of AStarNode's that are connected to node
        public static List<AStarNode> getSuccessors(AStarNode node, Vector3 goal, List<Connection> c){
            List<AStarNode> results = new List<AStarNode>();
            foreach (Connection conn in c){
                if(conn.A.Equals(node.Position)){
                    results.Add(new AStarNode(conn.B, goal, node));
                }
            }
            return results;
        }

        //this function resurns a node with the lowest H cost, if there is a tie, it is decided
        //by FIFO
        public static AStarNode visit(List<AStarNode> suc){
            AStarNode selection = suc[0];
            foreach (AStarNode s in suc){
                if(s != suc[0]){
                    if(s.CostH <= selection.CostH){
                        selection = s;
                    }
                }
            }
            return selection;
        }
    }
}