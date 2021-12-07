using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    #region Variables
    //Public
    [SerializeField] public Vector2 hexID;
    public Vector2 targetWarehouse;
    public Vector2 targetResource;
    public int resQuantity;

    //Private
    private WorkerHealth _workerHealth;
    private GameManager _gameManager;
    private int _range;
    private int _speed;

    #endregion
    
    #region Methods

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _workerHealth = GetComponent<WorkerHealth>();
        _range = 1;
        _speed = 10;
    }

    public void Die(Worker worker)
    {
        _gameManager.ClearHexagon(worker.hexID);
        //_gameManager.workerManager.workerListComponent.workerList.Remove(worker);
        //we need to remove from both lists 
        Destroy(worker.gameObject);
    }
    
    #endregion
    
    #region ToRemoveBehaviourTree

    public void GatherResource()
    {
        //Stop walking animation
        //Start gather animation
        //Start gather sound
        resQuantity += 40;
    }

    public void GiveResource()
    {
        //IF gatherAnimation Stop
        //IF gatherSound Stop
        //Start walk animation
        
        resQuantity -= 40;
    }

    #endregion
}
