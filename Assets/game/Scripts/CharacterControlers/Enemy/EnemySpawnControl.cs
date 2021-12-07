using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnControl : MonoBehaviour
{
    #region Variables

    public List<Vector3> spawnPoints;
    
    [SerializeField] private GameObject logberPrefab;
    [SerializeField] private GameObject boonogPrefab;
    [SerializeField] private GameObject nomnomPrefab;
    //[SerializeField] private GameObject gnomePrefab;
    private List<ProtectionCheck> _enemySpawnerList;
    private EnemyList _enemyListComponent;
    private EnemyManager _enemyManager;

    #endregion

    #region Methods

    private void Awake()
    {
        _enemyListComponent = GetComponent<EnemyList>();
        _enemyManager = GetComponent<EnemyManager>();
        _enemySpawnerList = FindObjectsOfType<ProtectionCheck>().ToList();
    }

    private void Start()
    {
        FirstSpawn();
    }

    public void Spawn()
    {
        float spawn = Random.Range(0.0f, 1.0f);
        if (spawn <= 0.60f) return;

        int tempNest = Random.Range(0, _enemySpawnerList.Count);
        int tempSp = Random.Range(0, _enemySpawnerList[tempNest].spawnPoints.Count);
        int tempEnemy = Random.Range(0, 3);
        bool whileExit = false;

        Transform _sp = _enemySpawnerList[tempNest].spawnPoints[tempSp];

        while (!whileExit)
        {
            switch (tempEnemy)
            {
                case 0:
                    Instantiate(logberPrefab, _sp.position, _sp.rotation);
                    _enemyManager.gameManager.audioManager.PlayAudio(("enemiesSpawn"));
                    whileExit = true;
                    break;
                case 1:
                    Instantiate(nomnomPrefab, _sp.position, _sp.rotation);
                    _enemyManager.gameManager.audioManager.PlayAudio(("enemiesSpawn"));
                    whileExit = true;
                    break;
                //case 2:
                    //Instantiate(gnomePrefab, _sp.position, _sp.rotation);
                    //_enemyManager.gameManager.audioManager.PlayAudio(("enemiesSpawn"));
                    //whileExit = true;
                    //break;
                case 2:
                    if (_enemySpawnerList[tempNest].isProtected) break;
                    Instantiate(boonogPrefab, _sp.position, _sp.rotation);
                    _enemyManager.gameManager.audioManager.PlayAudio(("enemiesSpawn"));
                    whileExit = true;
                    break;
            }

            tempEnemy = Random.Range(0, 3);
        }
    }

    private void FirstSpawn()
    {
        foreach (var spawn in spawnPoints)
        {
            int rand = Random.Range(0, 2);
            
            switch (rand)
            {
                case 0:
                    Instantiate(logberPrefab, spawn, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(nomnomPrefab, spawn, Quaternion.identity);
                    break;
                // case 2:
                //     Instantiate(gnomePrefab, spawn, Quaternion.identity);
                //     break;
            }
        }
        spawnPoints = null;
    }
    
    
    public void RemoveSpawn(ProtectionCheck protectionCheck)
    {
        _enemySpawnerList.Remove(protectionCheck);
    }

    public int ListAmount()
    {
        return _enemySpawnerList.Count;
    }
    #endregion
}