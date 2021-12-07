using System.Collections.Generic;
using UnityEngine;

public class ProtectionCheck : MonoBehaviour
{
    #region Variables

    //Public
    public List<Transform> spawnPoints;
    public Vector2 hexID;
    public bool isProtected;

    //Private
    [SerializeField] private int protectedRange;
    private List<Vector2> _protectedRangeList;
    private GameManager _gameManager;
    
    #endregion

    #region Methods

    private void Awake()
    {
        isProtected = false;
        _gameManager = FindObjectOfType<GameManager>();
        _protectedRangeList = new List<Vector2>();
        _protectedRangeList = Utils.CreateRangeList(_gameManager.fieldManager.graph, hexID, protectedRange, ListType.ATTACK);
    }
    
    #endregion
}
