using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    #region Variables
    List<TeamCharacter> possibleTargets { get; set; }
    TeamCharacter currentTarget { get; set; }
    int power { get; set; }
    int range { get; set; }
    int speed { get; set; }
    #endregion

    #region Methods

    void StartVariables();

    void GetPossibleTargets();

    void PlayAttackSound();

    IEnumerator PlayAttackAnimationCoroutine();

    void StartAttackAnimation();
    
    Selector GetBehaviourNode();

    ConditionDT GetConditionNode();

    bool TargetingBehavior();

    void Rotation();

    #endregion
}
