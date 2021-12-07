using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogberAttack : MonoBehaviour
{
    public Vector3 end;
    private float animation;

    void LateUpdate()
    {
        Shoot();
    }

    public void Shoot()
    {

        animation += Time.deltaTime;
        animation = animation % 1f;
        transform.position = ParabolaMath.Parabola(transform.position, end, 1f, animation);
        Destroy(gameObject, 1f);
    }
}
