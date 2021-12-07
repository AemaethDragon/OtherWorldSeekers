using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missil : MonoBehaviour
{
    public Vector3 worldPos;
    public Enemy enemy;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
        transform.localScale /= 3;
    }   
    private void Update()
    {
        if (worldPos.y >= enemy.transform.position.y)
        {
            transform.position = new Vector3(worldPos.x, worldPos.y - 0.7f, worldPos.z);
            worldPos = new Vector3(worldPos.x, worldPos.y - 0.7f, worldPos.z);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
