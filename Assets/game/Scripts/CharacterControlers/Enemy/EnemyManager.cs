using System.Collections;
using UnityEngine;

public enum TheyAreComing
{
    LOGBER,
    NOMNOM,
    GNOME,
    BOONOG
};

public class EnemyManager : MonoBehaviour
{
    #region variables

    //Public
    [HideInInspector] public EnemySpawnControl enemySpawnControl;
    [HideInInspector] public EnemyList enemyListComponent;
    public GameManager gameManager;
    public GameObject parent;

    //Private
    private GameObject Enemy;
    private Enemy myEnemy;
    private int nextWave = 0;

    #endregion

    #region Methods

    private void Awake()
    {
        enemySpawnControl = GetComponent<EnemySpawnControl>();
        gameManager = GetComponentInParent<GameManager>();
        enemyListComponent = GetComponent<EnemyList>();
    }

    public IEnumerator ExecuteEnemyBehaviour()
    {
        foreach (Enemy currentEnemy in enemyListComponent.enemyList)
        {
            SelectionManager.SelectEnemy(currentEnemy);
            SelectionManager.SelectAnimatable(currentEnemy);

            if (currentEnemy.enemyType == TheyAreComing.BOONOG)
            {
                currentEnemy.iEnemy.GetConditionNode();
            }
            else
            {
                currentEnemy.iEnemy.GetBehaviourNode().Evaluate();
            }

            yield return new WaitForSeconds(7f);
            SelectionManager.ClearSelectedEnemy();
            SelectionManager.ClearSelectedAnimatable();
        }
        StartCoroutine(gameManager.turnManager.ChangeTurnCoroutine());
    }
    #endregion
}