using UnityEngine;

public interface IAnimatable
{
    Animator animator { get; set; }

    void WalkBool(bool walking);

    void AttackTrigger();

    void DieTrigger();
}
