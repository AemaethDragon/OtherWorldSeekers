using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HasRangeNode : Node
{
    private readonly Enemy _enemy;

    public HasRangeNode(Enemy enemy)
    {
        _enemy = enemy;
    }
    
    public override NodeState Evaluate()
    {
        if (_enemy.enemyType != TheyAreComing.BOONOG && _enemy.enemyType != TheyAreComing.NOMNOM && _enemy.enemyType != TheyAreComing.LOGBER) return NodeState.FAILURE;
        _enemy.rangeList = Utils.CreateRangeList(_enemy.gameManager.fieldManager.graph, _enemy.iTargetable.hexID, _enemy.iEnemy.range, ListType.ATTACK);
        return _enemy.rangeList.Contains(_enemy.iEnemy.currentTarget.hexID) ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
