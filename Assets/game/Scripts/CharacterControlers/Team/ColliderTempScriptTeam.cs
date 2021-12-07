using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTempScriptTeam : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.transform.CompareTag("Hex")) return;
        TeamCharacter tempTeam = GetComponent<TeamCharacter>();
        Hexagon tempHex = collision.gameObject.GetComponent<Hexagon>();
        tempTeam.hexID = tempHex.matrixPos;
        tempTeam.transform.position = new Vector3(tempHex.worldPos.x, tempHex.worldPos.y + 1, tempHex.worldPos.z);
        Destroy(this);
    }
}
