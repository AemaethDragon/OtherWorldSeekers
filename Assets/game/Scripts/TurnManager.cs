using System.Collections;
using UnityEngine;

public enum Turn
{
    PLAYER,
    AI
}

public class TurnManager : MonoBehaviour
{
    #region Variables
    //Public
    public Turn turn;
    [SerializeField] CountDownRound countDownRound;
    public WorkerManager workerManager;
    public EnemyManager enemyManager;

    //Private
    [SerializeField] private GameManager gameManager;
    private ScreenIndication _screenIndication;
    private TeamManager teamManager;
    private int turnCounter;
    private bool _started;
    
    #endregion

    #region Methods
    // Start is called before the first frame update
    private void Start()
    {
        _screenIndication = GetComponent<ScreenIndication>();
        turnCounter = 0;
        enemyManager = gameManager.enemyManager;
        teamManager = gameManager.teamManager;
        turn = Turn.PLAYER;
        _started = false;
    }

    private void Update()
    {
        if (turn == Turn.AI) return;
        if (_started) return;
        ChangeTurnDependingOnResources();
    }


    //Public

    public void ChangeTurn()
    {
        gameManager.CleanSelection();
        if (turn == Turn.PLAYER)
        {
            if (turnCounter == 0)
            {
            gameManager.fieldManager.hexagonControl.CheckObstacles();
            }

            enemyManager.enemySpawnControl.Spawn();
            
            if (enemyManager.enemyListComponent.enemyList.Count > 0)
            {
                StartCoroutine(enemyManager.ExecuteEnemyBehaviour());
            }
            gameManager.resourceManager.GatherResources();
            _screenIndication.SetEnemyTurn();
            foreach (var enemy in gameManager.enemyManager.enemyListComponent.enemyList)
            {
                enemy.ResetEchain();
            }
            turn = Turn.AI;
        }
        else
        {       
            foreach (var item in teamManager.eliteSquad)
            {
                item.ResetValues();
                item.myPlayerDeck.AddCardNextTurn();
            }
            gameManager.cameraControl.CenterCamera(teamManager.eliteSquad[0].transform);
            _started = false;
            _screenIndication.SetPlayerTurn();
            turn = Turn.PLAYER;
        }
        turnCounter++;
    }

    private void ChangeTurnDependingOnResources()
    {
        int tempCountSpeed = 0;

        int tempCountEnergy = 0;

        //Summs the remaining speed and energy off all characters
        foreach (var item in teamManager.eliteSquad)
        {
            tempCountSpeed += item.speed;
            tempCountEnergy += item.energy;
        }

        if (tempCountSpeed <= 0 && tempCountEnergy <= 0)
        {
            StartCoroutine(ChangeTurnCoroutine());
            _started = true;
        }
    }

    public IEnumerator ChangeTurnCoroutine()
    {
        yield return new WaitUntil(() => countDownRound.ActivateTimer());
        ChangeTurn();
    }

    #endregion
}
