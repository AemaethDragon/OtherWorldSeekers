using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Aligment")]
public class AligmentBehavior : FlockBehavior
{
    public override Vector2 CalculateMovement(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return current aligment

        if (context.Count == 0)
            return new Vector2(agent.transform.forward.x,agent.transform.forward.z);

        //add all points together an average
        Vector2 alignmentMove = Vector2.zero;
        foreach (Transform item in context)
        {
            alignmentMove += new Vector2(item.transform.forward.x,item.transform.forward.z);
        }
        alignmentMove /= context.Count;

        //Vector2 finalAlignmentMove = new Vector2(alignmentMove.x + Random.Range(-.1f, .1f), alignmentMove.y + Random.Range(-.1f, .1f));

        return alignmentMove;
    }
}
