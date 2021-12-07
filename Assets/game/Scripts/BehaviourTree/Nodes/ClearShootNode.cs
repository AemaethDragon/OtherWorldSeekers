using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearShootNode : Node
{
    private Enemy _enemy;

    public ClearShootNode(Enemy enemy)
    {
        _enemy = enemy;
    }
    
    public override NodeState Evaluate()
    {
        Vector3 direction = _enemy.iEnemy.currentTarget.transform.position - _enemy.transform.position;
        if (!Physics.Raycast(_enemy.transform.position, direction, out var hit)) return NodeState.FAILURE;
        return hit.transform.CompareTag(_enemy.iEnemy.currentTarget.tag) ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
