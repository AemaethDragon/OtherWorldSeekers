using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedBehaviour : MonoBehaviour
{
    private GameManager _gameManager;
    private TeamCharacter _me;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _me = GetComponent<TeamCharacter>();
    }

    public void WantToWalk()
    {
        if (!Selected()) return;
        _gameManager.fieldManager.hexagonControl.WalkableAreaVisible(SelectionManager.SelectedPlayer, SelectionManager.SelectedPlayer.speed, ListType.MOVE);
    }

    public void ExecuteMove()
    {
        
        if (!Selected()) return;
        Move();
        _gameManager.fieldManager.hexagonControl.ClearAllVisibility();
        _gameManager.fieldManager.drawMovementLine.ClearDraw();
    }

    private void Move()
    {
        StartCoroutine(HexCheckCorroutine());
        List<Vector2> path = _gameManager.fieldManager.drawMovementLine.selectedPath;
        StartCoroutine(MovementManager.MoveCoroutine(path, SelectionManager.SelectedPlayer.gameObject));
        SelectionManager.SelectedPlayer.hexID = path[path.Count - 1];
        SelectionManager.SelectedPlayer.speed -= path.Count - 1;
        SelectionManager.ClearSelectedHexagon();
    }
    private IEnumerator HexCheckCorroutine()
    {
        _gameManager.fieldManager.hexagonControl.CheckObstacles();
        yield return new WaitForSeconds(0.1f);
    }
    private bool Selected()
    {
        return SelectionManager.SelectedPlayer == _me;
    }
}
