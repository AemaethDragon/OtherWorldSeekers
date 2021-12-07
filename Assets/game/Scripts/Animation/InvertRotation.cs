using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertRotation : MonoBehaviour
{
    public Transform rotation;

    // Update is called once per frame
    void Update()
    {
        rotation.Rotate(new Vector3(0f, 0f, -50f) * Time.deltaTime);
    }
}
