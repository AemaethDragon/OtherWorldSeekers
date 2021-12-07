using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public List<Enemy> enemyList;

    #region Methods

    private void Awake()
    {
        enemyList = new List<Enemy>();
    }
    
    //This method is to be called when a enemy spawn then add that spawned enemy in the list
    public void AddEnemyToTheList(Enemy enemy)
    {
        enemyList.Add(enemy);
    }
    
    //This method is to be called when a enemy die then remove that enemy in the list
    public void RemoveEnemyFromList(Enemy enemy)
    {
        enemyList.Remove(enemy);
    }

    #endregion
}
