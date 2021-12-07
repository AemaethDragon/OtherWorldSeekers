using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/RadiusBehavior")]
public class RadiusBehavior : FlockBehavior
{
    Vector2 center;
    public float radius = 200f;
    public override Vector2 CalculateMovement(FlockAgent agent, List<Transform> context, Flock flock)
    {
        center = new Vector2(flock.transform.position.x, flock.transform.position.z);
        Vector2 centerOffSet = center - new Vector2(agent.transform.position.x, agent.transform.position.z);
        float t = centerOffSet.magnitude / radius;
        if (t < 0.8f)
        {
            return Vector2.zero;
        }

        return centerOffSet * t * t;
    }
}
