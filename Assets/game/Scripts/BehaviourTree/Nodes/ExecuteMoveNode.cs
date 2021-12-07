using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteMoveNode : Node
{
    private readonly Enemy _enemy;

    public ExecuteMoveNode(Enemy enemy)
    {
        _enemy = enemy;
    }
    public override NodeState Evaluate()
    {
        _enemy.iTargetable.hexID = _enemy.latestPath[_enemy.latestPath.Count - 1];
        _enemy.DoCoroutine();
        return NodeState.SUCCESS;
    }
}
