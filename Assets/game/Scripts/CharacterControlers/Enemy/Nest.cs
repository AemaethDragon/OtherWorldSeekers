using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : MonoBehaviour, ITargetable
{
    #region Variables

    //Public
    public List<Vector2> closeRange;
    public List<Vector2> farRange;
    public HealthBar healthBar;
    public Boonog boonog;
    public Vector2 hexID { get; set; }
    public int currentHealth { get; set; }
    public int maxHealth { get; set; }


    //Private
    private ProtectionCheck _protectionCheck;
    private EnemyManager _enemyManager;
    
    #endregion
    
    #region Methods

    private void Awake()
    {
        _protectionCheck = GetComponent<ProtectionCheck>();
        _enemyManager = FindObjectOfType<EnemyManager>();
        maxHealth = 2000;
        currentHealth = 100;
        hexID = _protectionCheck.hexID;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
    }

    private void Start()
    {
        MakeCloseRange();
        MakeFarRange();
    }

    private void Update()
    {
        healthBar.SetHealth(currentHealth);
        _protectionCheck.isProtected = boonog != null;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            StartCoroutine(DieCoroutine());
        }
    }

    public void OnClick()
    {
        SelectionManager.SelectTargetable(this);
    }
    public Vector3 ReturnPos()
    {
        return transform.position;
    }

    private IEnumerator DieCoroutine()
    {
        List<Vector2> toClean = Utils.CreateRangeList(_enemyManager.gameManager.fieldManager.graph, hexID, 1, ListType.ATTACK);
        _enemyManager.enemySpawnControl.RemoveSpawn(_protectionCheck);
        
        
        //Run sounds and animations
        yield return new WaitForSeconds(1);
        foreach (var item in toClean)
        {
            _enemyManager.gameManager.fieldManager.hexagons[item].SetFree(true);
        }
        Destroy(gameObject);
    }

    private void MakeCloseRange()
    {
        closeRange = Utils.CreateRangeList(_enemyManager.gameManager.fieldManager.graph, hexID, 4, ListType.MOVE);
        List<Vector2> temp = Utils.CreateRangeList(_enemyManager.gameManager.fieldManager.graph, hexID, 2, ListType.MOVE);
        foreach (var vector2 in temp.ToArray())
        {
            if (closeRange.Contains(vector2))
            {
                closeRange.Remove(vector2);
            }
        }
    }

    private void MakeFarRange()
    {
        farRange = Utils.CreateRangeList(_enemyManager.gameManager.fieldManager.graph, hexID, 7, ListType.MOVE);
        List<Vector2> temp = Utils.CreateRangeList(_enemyManager.gameManager.fieldManager.graph, hexID, 2, ListType.MOVE);
        foreach (var vector2 in temp.ToArray())
        {
            if (closeRange.Contains(vector2))
            {
                closeRange.Remove(vector2);
            }
        }
    }

    #endregion
}
