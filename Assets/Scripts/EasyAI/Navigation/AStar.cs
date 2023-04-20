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
            
            AStarNode start = new AStarNode(current, goal);
            List<AStarNode> open = new List<AStarNode>();
            List<AStarNode> closed = new List<AStarNode>();
            open.Add(start);
            AStarNode curr = start;
            List<Vector3> path = new List<Vector3>();
            bool test = false;
            foreach (Connection conn in connections){
                if(conn.A.Equals(goal)){
                    test = true;
                }
            }
            Debug.Log("goal: " + goal + " neighbours: " + test);
            //A* algorithm loop
            while(open.Count > 0){
                curr = visit(open);
                open.Remove(curr);
                curr.Close();
                closed.Add(curr);
                if(curr.Position == goal){
                    break;
                }
                foreach (Connection conn in connections){
                    if(conn.A.Equals(curr.Position) && !(closed.Any(c => c.Position == conn.B)) && !(open.Any(o => o.Position == conn.B))){
                        open.Add(new AStarNode(conn.B, goal, curr));
                    }
                }
            }
            //get the path
            while(curr.Position != start.Position){
                path.Add(curr.Position);
                curr = curr.Previous;
            }
            if(curr.Position == start.Position){
                path.Add(curr.Position);
            }
            return path;
        }

        //this function resurns a node with the lowest H cost, if there is a tie, it is decided
        //by FIFO
        public static AStarNode visit(List<AStarNode> open){
            AStarNode select = open.ElementAt(0);
            foreach (AStarNode o in open){
                if(o.IsOpen){
                    if(o.CostH <= select.CostH || !select.IsOpen){
                        select = o;
                    }
                }
            }
            return select;
        }
    }
}