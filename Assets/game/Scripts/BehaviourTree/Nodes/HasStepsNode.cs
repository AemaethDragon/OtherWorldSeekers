using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasStepsNode : Node
{
    private readonly Enemy _enemy;

    public HasStepsNode(Enemy enemy)
    {
        _enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        return _enemy.iEnemy.speed > 0 ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
