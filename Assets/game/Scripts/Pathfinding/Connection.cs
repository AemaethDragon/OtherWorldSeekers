using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection
{
    private Vector2 fromNode;
    private Vector2 toNode;
    private float cost;
    private bool free;


    public Vector2 FromNode => fromNode;
    public Vector2 ToNode => toNode;
    public float Cost => cost;
    public bool Free => free;

    public Connection(Vector2 fromNode, Vector2 toNode, float cost)
    {
        this.fromNode = fromNode;
        this.toNode = toNode;
        this.cost = cost;
        free = true;
    }

    public void SetFree(bool free)
    {
        this.free = free;
    }
}
