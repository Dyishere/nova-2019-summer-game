using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public int x,y,hCost,gCost;
    public Vector3 Pos;
    public int fCost { get { return gCost + hCost; } }
    public Node parent;
    public Node(int x,int y,bool walkable,Vector3 pos)
    {
        this.x = x;
        this.y = y;
        this.walkable = walkable;
        this.Pos = pos;
    }
}
