using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    #region Variables
    //Public
    public Vector2 hexID;
    public GameObject floatingActivationText;
    
    //Private
    private GameManager _gameManager;
    private List<Vector2> _rangeList;
    private bool _isActive;

    #endregion

    #region Methods

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _isActive = false;
    }

    private void Start()
    {
        _rangeList = Utils.CreateRangeList(_gameManager.fieldManager.graph, hexID, 1, ListType.ATTACK);
    }

    public void Activate()
    {
        if (IsActive() || SelectionManager.SelectedPlayer == null || !_rangeList.Contains(SelectionManager.SelectedPlayer.hexID)) return;
        Instantiate(floatingActivationText, this.transform.position, Quaternion.Euler(90,0,0));
        _isActive = true;
        _gameManager.resourceManager.IncreaseMultiplier();
    }

    public bool IsActive()
    {
        return _isActive;
    }
    
    #endregion
}
