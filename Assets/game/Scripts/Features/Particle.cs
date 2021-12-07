using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public IEnumerator MoveParticle(Vector3 end, float speed)
    {
        yield return new WaitUntil(() => MyMoveTowards(end, speed) == end);
        Destroy(this.gameObject);
    }

    private Vector3 MyMoveTowards(Vector3 end, float speed)
    {
        transform.position = Vector3.MoveTowards(transform.position, end, speed * Time.deltaTime);
        Vector3 tempDirection = (end - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(tempDirection);
        return transform.position;
    }
}
