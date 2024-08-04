using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeType
{
    Walk,Stop
}

public class AStarNode
{
    public int x;
    public int y;

    public float f;
    public float g;
    public float h;

    public AStarNode father;

    public NodeType type;


    public AStarNode(int x,int y, NodeType type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
    }
}
