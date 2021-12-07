using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/Avoindence")]
public class AvoidenceBehavior : FlockBehavior
{
    public override Vector2 CalculateMovement(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return current aligment

        if (context.Count == 0)
            return Vector2.zero;

        //add all points together an average
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        foreach (Transform item in context)
        {
            if (Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                Vector3 temp = agent.transform.position - item.position;
                avoidanceMove += new Vector2(temp.x,temp.z);
            }
        }
        if (nAvoid > 0)
            avoidanceMove /= nAvoid;

        //Vector2 finalAvoidenceMove = new Vector2(avoidanceMove.x + Random.Range(-.1f, .1f), avoidanceMove.y + Random.Range(-.1f, .1f));

        return avoidanceMove;
    }
}
