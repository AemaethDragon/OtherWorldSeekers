using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetTargetNode : Node
{
    private Enemy _enemy;

    public GetTargetNode(Enemy enemy)
    {
        _enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        if (_enemy.iEnemy.TargetingBehavior())
        {
            return NodeState.SUCCESS;

        }
        return NodeState.FAILURE;
    }
}