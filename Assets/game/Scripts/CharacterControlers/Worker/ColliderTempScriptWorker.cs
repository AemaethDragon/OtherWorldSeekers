using UnityEngine;

public class ColliderTempScriptWorker : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Hex")
        {
            Worker tempWorker = GetComponent<Worker>();
            Hexagon tempHex = collision.gameObject.GetComponent<Hexagon>();
            tempWorker.hexID = tempHex.matrixPos;
            tempWorker.transform.position = new Vector3(tempHex.worldPos.x, tempHex.worldPos.y + 1, tempHex.worldPos.z);
            Destroy(this);
        }
    }
}
