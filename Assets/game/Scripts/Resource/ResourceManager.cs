using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    #region Variables
    
    //Public
    public TMP_Text displayAmount;
    public int resources;

    //Private
    private List<Resource> _resourceList;
    private int _multiplier;
    private int _perTurn;

    #endregion


    #region Methods

    private void Awake()
    {
        _resourceList = new List<Resource>();
        _resourceList.AddRange(FindObjectsOfType<Resource>());
        resources = 0;
        _multiplier = 0;
        _perTurn = 350;
    }

    private void Update()
    {
        displayAmount.text = "Resources: " + resources;
    }

    public void IncreaseMultiplier()
    {
        _multiplier++;
    }
    
    public void GatherResources()
    {
        if(_multiplier == 0) return;
        resources += _perTurn * _multiplier;
    }

    #endregion
}
