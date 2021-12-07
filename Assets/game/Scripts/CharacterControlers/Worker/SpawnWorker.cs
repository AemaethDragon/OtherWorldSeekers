using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWorker : MonoBehaviour
{
    #region variables
    //Public
    public WorkerWarehouse workerWarehouse;
    public GameObject workerPrefab;
    public Transform spawnPoint;
    public Transform parent;

    //Private
    private List<Vector2> _myRangeList;
    private GameManager _gameManager;
    private Resource _resource;
    
    #endregion

    #region Methods

    private void Awake()
    {
        _resource = GetComponent<Resource>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _myRangeList = Utils.CreateRangeList(_gameManager.fieldManager.graph, _resource.hexID, 1, ListType.ATTACK);
    }

    public void SpawnWorkerMethod()
    {
        if (!_myRangeList.Contains(SelectionManager.SelectedPlayer.hexID)) return;
        Transform _sp = spawnPoint;
        GameObject go = Instantiate(workerPrefab, _sp.position, _sp.rotation);
        Worker temp = go.AddComponent<Worker>();
        temp.targetResource = _resource.hexID;
        temp.targetWarehouse = workerWarehouse.hexID;
        go.transform.SetParent(parent);
    }

    #endregion
}
