using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNode : Node
{
    private readonly Enemy _enemy;
    
    public AttackNode(Enemy enemy)
    {
        _enemy = enemy;
    }
    
    public override NodeState Evaluate()
    {
        _enemy.iEnemy.StartAttackAnimation();

        _enemy.iEnemy.Rotation();

        if (_enemy.iEnemy.currentTarget.currentHealth <= 0)
        {
            foreach (var enemy in _enemy.gameManager.enemyManager.enemyListComponent.enemyList)
            {
                if (enemy.iEnemy.currentTarget == _enemy.iEnemy.currentTarget)
                {
                    enemy.iEnemy.currentTarget = null;
                }
            }
            _enemy.iEnemy.currentTarget = null;
        }
        return NodeState.SUCCESS;
    }
}
