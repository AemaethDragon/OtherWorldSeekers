using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenIndication : MonoBehaviour
{
    [SerializeField] private GameObject _playerTurn;
    [SerializeField] private GameObject _enemyTurn;
    
    
    public void SetPlayerTurn()
    {
        _enemyTurn.SetActive(false);
        _playerTurn.SetActive(true);
    }
    
    public void SetEnemyTurn()
    {
        _playerTurn.SetActive(false);
        _enemyTurn.SetActive(true);
    }
}
