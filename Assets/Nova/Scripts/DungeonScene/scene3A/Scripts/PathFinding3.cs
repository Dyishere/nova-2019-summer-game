using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding3 : MonoBehaviour
{
    Grid myGrid;

    Node startNode;
    Node targetNode;
    // Start is called before the first frame update
    void Awake()
    {
        myGrid = GetComponent<Grid>();
    }
    void FindPath(Vector3 startPos,Vector3 targetPos,List<Node> path)
    {
        startNode = myGrid.GetNodeFromPosition(startPos);
        targetNode = myGrid.GetNodeFromPosition(targetPos);
        List<Node> openSet = new List<Node>();
        HashSet<Node> closeSet = new HashSet<Node>();
        openSet.Add(startNode);
        

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                    currentNode = openSet[i];
            }
            openSet.Remove(currentNode);
            closeSet.Add(currentNode);
            if (currentNode == targetNode)
            {

                RetrievePath(currentNode,path);
                return;
            }

            foreach(Node n in GetNeighbors(currentNode))
            {
                if (!n.walkable || closeSet.Contains(n))
                    continue;
                int newgCost = currentNode.gCost + GetDistance(currentNode, n);
                bool inOpenset = openSet.Contains(n);
                if (newgCost < n.gCost || !inOpenset)
                {
                    n.gCost = newgCost;
                    n.hCost = GetDistance(n, targetNode);
                    n.parent = currentNode;
                    if (!inOpenset)
                    {
                        openSet.Add(n);
                    }
                }
                
            }
        }
    }

    private void RetrievePath(Node n,List<Node> path)
    {
        List<Node> p = new List<Node>();
        
        while (n != startNode)        //从后往前添加路径
        {
            p.Add(n);
            n = n.parent;
        }
        p.Reverse();                  //翻转
        myGrid.path3 = p;
    }

    int GetDistance(Node n1,Node n2)
    {
        int distanceX = (int)Mathf.Abs(n1.x - n2.x);
        int distanceY = (int)Mathf.Abs(n1.y - n2.y);
        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        else
            return 14 * distanceX + 10 * (distanceY - distanceX);
    }
    private List<Node> GetNeighbors(Node n)
    {
        List<Node> neighbors = new List<Node>();
        int xx = n.x, yy = n.y;
        for(int x= -1;x<=1;x++)
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                if (x == 1 && y == 1)
                    continue;
                if (x == -1 && y == 1)
                    continue;
                if (x == 1 && y == -1)
                    continue;
                if (x == -1 && y == -1)
                    continue;
                if (xx + x >= 0&& xx + x< myGrid.nodeNumX && yy + y >= 0 && yy + y < myGrid.nodeNumY)
                {
                    neighbors.Add(myGrid.grid[xx + x, yy + y]);
                }
            }
        return neighbors;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //FindPath(myGrid.Gd1.position, myGrid.Target1.position,myGrid.path1);
        //FindPath(myGrid.Gd2.position, myGrid.Target2.position, myGrid.path2);
        FindPath(myGrid.Gd3.position, myGrid.Target3.position, myGrid.path3);
        //FindPath(myGrid.Gd4.position, myGrid.Target4.position, myGrid.path4);
    }

}
