﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Nicholas Easterby - EAS12337350
//This script takes the information from the nodes attached to the grid and calculates the otpimal, lowest cost part.
//The script take two inputs, the Start Position and the Target Position.
//This script was created using a turorial by Daniel on youtube - link:https://www.youtube.com/watch?v=AKKpPmxx07w  
public class Pathfinding : MonoBehaviour {

    Grid grid;
    public Transform StartPosition;
    public Transform TargetPosition;

    private void Start()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        if (StartPosition != null && TargetPosition != null)
        {
            FindPath(StartPosition.position, TargetPosition.position);
        }
    }

    void FindPath(Vector2 a_StartPos, Vector2 a_TargetPos)
    {
        Node StartNode = grid.NodeFromWorldPosition(a_StartPos);
        Node TargetNode = grid.NodeFromWorldPosition(a_TargetPos);

        List<Node> OpenList = new List<Node>();
        HashSet<Node> ClosedList = new HashSet<Node>();

        OpenList.Add(StartNode);

        while (OpenList.Count > 0)
        {
            Node CurrentNode = OpenList[0];
            for(int i = 1; i < OpenList.Count; i++)
            {
                if(OpenList[i].Fcost<CurrentNode.Fcost || OpenList[i].Fcost == CurrentNode.Fcost && OpenList[i].hCost < CurrentNode.hCost)
                {
                    CurrentNode = OpenList[i];
                }                
            }
            OpenList.Remove(CurrentNode);
            ClosedList.Add(CurrentNode);

            if (CurrentNode == TargetNode)
            {
                GetFinalPath(StartNode, TargetNode);
            }

            foreach(Node NeighborNode in grid.GetNeighborNodes(CurrentNode))
            {
                if (!NeighborNode.IsWall || ClosedList.Contains(NeighborNode))
                {
                    continue;
                }
                int MoveCost = CurrentNode.gCost + GetManhattenDistance(CurrentNode, NeighborNode);

                if (MoveCost < NeighborNode.gCost || !OpenList.Contains(NeighborNode))
                {
                    NeighborNode.gCost = MoveCost;
                    NeighborNode.hCost = GetManhattenDistance(NeighborNode, TargetNode);
                    NeighborNode.Parent = CurrentNode;

                    if (!OpenList.Contains(NeighborNode))
                    {
                        OpenList.Add(NeighborNode);
                    }
                }
            }
        }
    }

    void GetFinalPath(Node a_StartingNode, Node a_EndNode)
    {
        List<Node> FinalPath = new List<Node>();
        Node CurrentNode = a_EndNode;

        while(CurrentNode != a_StartingNode)
        {
            FinalPath.Add(CurrentNode);
            CurrentNode = CurrentNode.Parent;
        }

        FinalPath.Reverse();

        grid.FinalPath = FinalPath;
    }

    int GetManhattenDistance(Node a_nodeA, Node a_nodeB)
    {
        int ix = Mathf.Abs(a_nodeA.gridX - a_nodeB.gridX);
        int iy = Mathf.Abs(a_nodeA.gridY - a_nodeB.gridY);

        return ix + iy;
    }
}
