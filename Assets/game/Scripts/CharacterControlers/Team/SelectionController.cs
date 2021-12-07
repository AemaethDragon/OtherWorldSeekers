using System;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    #region Variables
    //Public
    public GameObject selectedObject;
    public GameObject hoverSprite;

    //Private
    private TeamCharacter _teamCharacter;
    private GameManager _gameManager;

    #endregion

    #region Methods

    private void Awake()
    {
        _teamCharacter = GetComponent<TeamCharacter>();
        _gameManager = FindObjectOfType<GameManager>();
        
    }
    public void ShowMouseOver()
    {
        hoverSprite.SetActive(true);
    }

    public void HideMouseOver()
    {
        hoverSprite.SetActive(false);
    }

    public void ShowSelected()
    {
        foreach (var character in _gameManager.teamManager.eliteSquad)
        {
            character.selectionController.HideSelected();
            SetSelected(false);
        }
        selectedObject.SetActive(true);
        SetSelected(true);
    }

    public void HideSelected()
    {
        _gameManager.fieldManager.hexagonControl.ClearAllVisibility();
        _gameManager.fieldManager.drawMovementLine.ClearDraw();
        selectedObject.SetActive(false);
        SetSelected(false);
    }

    public void SetSelected(bool active)
    {
        if (_gameManager.turnManager.turn == Turn.PLAYER)
        {
            if (active)
            {
                _teamCharacter.myPlayerDeck.AddSelectePlayerCardsToTheView();
                _gameManager.fieldManager.hexagonControl.CheckObstacles();
                _gameManager.cameraControl.CenterCamera(transform);
                SelectionManager.SelectPlayer(_teamCharacter);
                SelectionManager.SelectAnimatable(_teamCharacter);
            }
            else
            {
                _teamCharacter.myPlayerDeck.DestroyCardsFromTheView();
                SelectionManager.ClearSelectedPlayer();
                SelectionManager.ClearSelectedHexagon();
                SelectionManager.ClearSelectedAnimatable();
            }
        }
        else
        {
            _gameManager.fieldManager.hexagonControl.CheckObstacles();
        }
    }

    #endregion
}
