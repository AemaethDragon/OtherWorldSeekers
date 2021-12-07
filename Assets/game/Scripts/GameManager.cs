using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables
    //Public
    public ResourceManager resourceManager;
    public AbilityManager abilityManager;
    public CameraControl cameraControl;
    public FieldManager fieldManager;
    public EnemyManager enemyManager;
    public TeamManager teamManager;
    public TurnManager turnManager;
    public AudioManager audioManager;
    public int deadEnemies;

    //Private
    #endregion

    #region Methods

    private void Awake()
    {
        deadEnemies = 0;
    }

    private void Update()
    {
        MainLogic();
        WinGame();
        LoseGame();
        if (Input.GetKeyDown(KeyCode.A))
        {
            foreach (var hex in fieldManager.hexagons)
            {
                if (hex.Value.IsFree())
                {
                    hex.Value.IsVisible(true);
                }
            }
        }
    }

    private void MainLogic()
    {
        if (MouseManager.MouseTwoPressed())
        {
            CleanSelection();
            if (abilityManager.bigCard.transform.childCount != 0)
            {
                Destroy(abilityManager.bigCard.transform.GetChild(0).gameObject);
                abilityManager.StopAllCoroutines();
            }
            Utils.cardUseAbility = null;
            Utils.cardInUse = null;
        }
    }

    //Public
    public void ClearHexagon(Vector2 matrixPos)
    {
        fieldManager.hexagons[matrixPos].SetFree(true);
    }

    public void CleanSelection()
    {
        if (SelectionManager.SelectedPlayer != null)
        {
            StopCoroutine(teamManager.AttackCoroutine());
            SelectionManager.SelectedPlayer.selectionController.HideSelected();
        }

        //Clear all the visible hexagons after walking
        fieldManager.hexagonControl.ClearAllVisibility();
        SelectionManager.ClearSelectedPlayer();
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();
        SelectionManager.ClearSelectedAnimatable();
        SelectionManager.ClearSelectedTargetable();

        SelectionManager.EnableReturn();
    }

    private void WinGame()
    {
        if (enemyManager.enemySpawnControl.ListAmount() == 0 || resourceManager.resources >= 20000)
        {
            //Change to Win Screen
            SceneManager.LoadScene(8);
            int points = resourceManager.resources + 10000 * (3 - enemyManager.enemySpawnControl.ListAmount()) + 500 * deadEnemies;
            Points.points = points;
        }
    }

    private void LoseGame()
    {
        if (teamManager.eliteSquad.Count == 0)
        {
            //Change to Lose screen
            SceneManager.LoadScene(7);
            int points = resourceManager.resources + 100 * (3 - enemyManager.enemySpawnControl.ListAmount()) + 250 * deadEnemies;
            Points.points = points;
        }
    }
    
    #endregion
}