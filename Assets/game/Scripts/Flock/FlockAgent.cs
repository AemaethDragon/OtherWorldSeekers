using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    Collider agentCollider;
    public Collider AgentCollider { get{ return agentCollider; } }
    private void Start()
    {
        agentCollider = GetComponent<Collider>();
    }

    public void Move(Vector2 velocity)
    {
        transform.forward = new Vector3(velocity.x, 0, velocity.y);
        transform.position += new Vector3(velocity.x,0,velocity.y) * Time.deltaTime;
    }
}
