using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inverter : Node
{
    private readonly Node _node;

    public Inverter(Node node)
    {
        _node = node;
    }

    public override NodeState Evaluate()
    {
        switch(_node.Evaluate())
        {
            case NodeState.RUNNING:
            nodeState = NodeState.RUNNING;
            return nodeState;
            case NodeState.SUCCESS:
            nodeState = NodeState.FAILURE;
            return nodeState;
            case NodeState.FAILURE: 
            nodeState = NodeState.SUCCESS;
            return nodeState;
        }
        return nodeState;
    }
}
