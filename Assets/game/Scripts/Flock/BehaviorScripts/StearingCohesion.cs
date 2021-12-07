using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/StearingCohesion")]
public class StearingCohesion : FlockBehavior
{

    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public int asd;
    public override Vector2 CalculateMovement(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjustment
        asd = context.Count;
        if (context.Count == 0)
            return Vector2.zero;

        //add all points together an average
        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform item in context)
        {
            cohesionMove += new Vector2(item.position.x,item.position.z);
        }
        cohesionMove /= context.Count;


        //create offset from  agent position
        cohesionMove -= new Vector2(agent.transform.position.x,agent.transform.position.z);
        cohesionMove = Vector2.SmoothDamp(new Vector2(agent.transform.forward.x, agent.transform.forward.z), cohesionMove, ref currentVelocity, agentSmoothTime);
        //Vector2 finalCoesionMove = new Vector2(cohesionMove.x + Random.Range(-.1f, .1f), cohesionMove.y + Random.Range(-.1f, .1f));

        return cohesionMove;
    }
}
