using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class HasTargetNode : Node
{
    private readonly Enemy _enemy;
    
    public HasTargetNode(Enemy enemy)
    {
        _enemy = enemy;
    }
    
    public override NodeState Evaluate()
    {
        return (_enemy.iEnemy.currentTarget != null) ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
