using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTempScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Hex")) return;
        Enemy tempEnemy = GetComponent<Enemy>();
        Hexagon tempHex = collision.gameObject.GetComponent<Hexagon>();
        tempEnemy.iTargetable.hexID = tempHex.matrixPos;
        tempEnemy.transform.position = new Vector3(tempHex.worldPos.x, tempHex.worldPos.y + 1, tempHex.worldPos.z);
        Destroy(this);
    }
}
