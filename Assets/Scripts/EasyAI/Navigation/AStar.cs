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

            //I don't believe there is any problem with my current implementation of A*, the math seems to work, the only thing that breaks it is when goal nodes come with no connections,
            //meaning no valid path can be found. But A* is supposed to be a complete algorithm so this cannot be the case. I feel the error must lie in the CornerGRaphGenerator.
            
            AStarNode start = new AStarNode(current, goal);//initialize start of algorithm
            List<AStarNode> open = new List<AStarNode>();//initialize open list of nodes
            List<AStarNode> closed = new List<AStarNode>();//initialize closed list of nodes
            open.Add(start);// add start to open
            AStarNode curr = start;// make start current node
            List<Vector3> path = new List<Vector3>();//initialize path to return for lookup calculation
            /**
            *forloop below checks if the goal node has neighbours and printes the result.
            **/
            bool test = false;//assume all goal nodes have connections
            foreach (Connection conn in connections){
                if(conn.A.Equals(goal)){//if a connection in connections has goal as one of it's members, it has at least 1 neighbour and is therefore traversable
                    test = true;
                }
            }
            Debug.Log("goal: " + goal + " neighbours: " + test);//debug which nodes have connections and which don't
            //A* algorithm loop
            while(open.Count > 0){
                curr = visit(open);//visit the most appropriate node in the open list
                open.Remove(curr);//remove it from open list
                curr.Close();//close it
                closed.Add(curr);//add to closed list
                if(curr.Position == goal){//if goal has been found, break loop
                    break;
                }
                //for each connection where one of the nodes is the current one and the other is not already in the open or closed lists. Add a new AStarNode
                foreach (Connection conn in connections){
                    if(conn.A.Equals(curr.Position) && !(closed.Any(c => c.Position == conn.B)) && !(open.Any(o => o.Position == conn.B))){
                        open.Add(new AStarNode(conn.B, goal, curr));
                    }
                }
            }
            //get the path
            while(curr.Position != start.Position){//if current position is not the start position
                path.Add(curr.Position);//add previous positions to path so we can trace a path from the goal back to the start using the Previous AStarNode property
                curr = curr.Previous;
            }
            if(curr.Position == start.Position){//If we have reached the start node, we have completed the path
                path.Add(curr.Position);
            }
            return path;//return path to goal from start
        }

        //this function resurns a node with the lowest H cost, if there is a tie, it is decided
        //by FIFO
        public static AStarNode visit(List<AStarNode> open){
            AStarNode select = open.ElementAt(0);//select first element in open
            foreach (AStarNode o in open){//each node in open is judged if it has an equal or lower CostH than 
                if(o.IsOpen){//if current node is not closed, has not been selected as curr in A* algo yet
                    if(o.CostH <= select.CostH || !select.IsOpen){//if select is closed, cannot be returned as new value for curr or if o is <= to CostH of select, select is equal to o
                        select = o;
                    }
                }
            }
            return select;
        }
    }
}