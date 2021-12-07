using System;
using UnityEngine;

public class HexagonCollision : MonoBehaviour
{
    public bool free;

    public Hexagon hexagon;
    
    private void Start()
    {
        free = true;
    }

    //Change color if colliding with something
    public void CollisionEnter()
    {
        if (hexagon == null) return;
        free = false;
    }

    public void CollisionStay()
    {
        if (hexagon == null) return;
        free = false;
    }

    //Change color if collision stops
    public void CollisionExit()
    {
        if (hexagon == null) return;
        free = true;
    }
}
