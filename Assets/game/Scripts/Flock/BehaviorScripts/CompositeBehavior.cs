using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    public FlockBehavior[] behaviors;
    public float[] weights;

    public override Vector2 CalculateMovement(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //handel data missmatch
        if (weights.Length != behaviors.Length)
        {
            //Debug.LogError("Data Missmatch in " + name, this);
            return Vector2.zero;
        }

        //setup move

        Vector2 move = Vector2.zero;


        //inetarte throught behaviors
        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = behaviors[i].CalculateMovement(agent, context, flock) * weights[i];

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;

            }
        }
        return move;
    }
}
