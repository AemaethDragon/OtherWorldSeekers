using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;


    [Range(10, 500)]
    public int startingCount = 250;
    const float agentDensity = 0.001f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }


    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            float rand = Random.Range(-20f, 20f);
            float rand2 = Random.Range(-1f, 1f);
            Vector3 asd = new Vector3(rand2, rand2, rand2);
            FlockAgent newAgent = Instantiate(agentPrefab, new Vector3(transform.position.x + rand, transform.position.y, transform.position.z +rand), Quaternion.Euler(Vector3.forward + asd),transform);
            newAgent.name = "Agent " + i;
            agents.Add(newAgent);

        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            agent.GetComponentInChildren<Transform>().GetComponentInChildren<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.red, context.Count / 15f);

            Vector2 move = behavior.CalculateMovement(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextCollider = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach (Collider c in contextCollider)
        {
            if (c!= agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
