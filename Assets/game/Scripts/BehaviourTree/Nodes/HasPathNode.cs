using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasPathNode : Node
{
    private Enemy _enemy;

    public HasPathNode(Enemy enemy)
    {
        _enemy = enemy;
    }

    public override NodeState Evaluate()
    {
        _enemy.latestPath = Utils.GetBestRoute(_enemy.iEnemy.currentTarget.hexID, _enemy.iTargetable.hexID, ref _enemy.gameManager.fieldManager.graph, _enemy.iEnemy.speed);
        return _enemy.latestPath.Count > 0 ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
