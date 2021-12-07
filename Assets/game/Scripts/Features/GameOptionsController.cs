using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionsController : MonoBehaviour
{
    #region Variables
    //Public
    public Button attackButton;
    public Button searchButton;
    public Button moveButton;
    public Button changeTurnButton;
    private GameManager _gameManager;

    #endregion

    #region Methods

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        UIPanelControl();
    }
    
    private void UIPanelControl()
    {
        if (SelectionManager.SelectedPlayer != null)
        {
            EnableButtons();
            if (SelectionManager.SelectedPlayer.hasAttacked)
            {
                attackButton.interactable = false;
            }
            else
            {
                attackButton.interactable = true;
            }

            if (SelectionManager.SelectedPlayer.speed <= 0)
            {
                searchButton.interactable = false;
                moveButton.interactable = false;
            }
            else
            {
                searchButton.interactable = true;
                moveButton.interactable = true;
            }
        }
        else DisableButtons();

        if (_gameManager.turnManager.turn == Turn.PLAYER)
        {
            EnableButtonsChange();
        }
        else
        {
            DisabledButtonsChange();
        }
    }

    private void DisableButtons()
    {
        attackButton.gameObject.SetActive(false);
        searchButton.gameObject.SetActive(false);
        moveButton.gameObject.SetActive(false);
    }
    
    private void EnableButtons()
    {
        attackButton.gameObject.SetActive(true);
        searchButton.gameObject.SetActive(true);
        moveButton.gameObject.SetActive(true);
    }
    private void  EnableButtonsChange()
    {
        changeTurnButton.gameObject.SetActive(true);
    }
    private void DisabledButtonsChange()
    {
        changeTurnButton.gameObject.SetActive(false);
    }
    #endregion
}
