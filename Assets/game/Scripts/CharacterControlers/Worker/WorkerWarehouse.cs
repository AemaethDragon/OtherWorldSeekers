using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkerWarehouse : MonoBehaviour
{
    #region Variables

    [Range(0, 1000000)] public int resources;
    public List<Transform> spawnPoints;
    public TMP_Text displayAmount;
    public Vector2 hexID;

    #endregion

    private void Update()
    {
        displayAmount.text = "Resources: " + resources;
    }
}
