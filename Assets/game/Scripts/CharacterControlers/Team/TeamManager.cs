using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum TheASquad
{
    JUGGERNAUT,
    SOLDIER,
    SNIPER,
    MAGE
};

public class TeamManager : MonoBehaviour
{
    #region variables
    //Public
    public List<TeamCharacter> eliteSquad;
    public GameObject juggernaut;
    public GameObject mage;
    public GameObject sniper;
    public GameObject soldier;
    public GameObject player;
    public Vector3 jugPosition;
    public Vector3 mgPosition;
    public Vector3 snpPosition;
    public Vector3 solPosition;

    //Private
    private GameManager _gameManager;
    #endregion

    #region Methods

    private void Awake()
    {
        GameObject temp1 = Instantiate(juggernaut, jugPosition, Quaternion.identity);
        temp1.transform.SetParent(player.transform);
        GameObject temp2 = Instantiate(mage, mgPosition, Quaternion.identity);
        temp2.transform.SetParent(player.transform);
        GameObject temp3 = Instantiate(sniper, snpPosition, Quaternion.identity);
        temp3.transform.SetParent(player.transform);
        GameObject temp4 = Instantiate(soldier, solPosition, Quaternion.identity);
        temp4.transform.SetParent(player.transform);
        eliteSquad = FindObjectsOfType<TeamCharacter>().ToList();
    }

    private void Start()
    {
        _gameManager = GetComponentInParent<GameManager>();
    }

    //This method is to be called when a enemy die then remove that enemy in the list
    public void RemoveCharactherFromList(TeamCharacter characters)
    {
        eliteSquad.Remove(characters);
    }

    public void StartAttack()
    {
        StartCoroutine(AttackCoroutine());
    }

    public IEnumerator AttackCoroutine()
    {
        List<Vector2> myAttackRange = Utils.CreateRangeList(_gameManager.fieldManager.graph, SelectionManager.SelectedPlayer.hexID, SelectionManager.SelectedPlayer.range, ListType.ATTACK);
        bool exit = false;
        while (!exit)
        {
            _gameManager.fieldManager.hexagonControl.WalkableAreaVisible(SelectionManager.SelectedPlayer, SelectionManager.SelectedPlayer.range, ListType.ATTACK);
            yield return new WaitUntil(() => WaitingTargetable() != null);
            ITargetable tempTarget = SelectionManager.SelectedTargetable;
            if (myAttackRange.Contains(tempTarget.hexID))
            {
                Vector3 tempvector = SelectionManager.SelectedTargetable.ReturnPos() - SelectionManager.SelectedPlayer.transform.position;
                SelectionManager.SelectedPlayer.transform.rotation = Quaternion.LookRotation(tempvector);
                SelectionManager.SelectedPlayer.AttackTrigger();
                yield return new WaitForSeconds(0.9f);
                SelectionManager.SelectedPlayer.PlayAttackSound();
                SelectionManager.SelectedPlayer.PlayWeaponEffect(SelectionManager.SelectedTargetable.ReturnPos());
                tempTarget.TakeDamage(SelectionManager.SelectedPlayer.power);
                _gameManager.fieldManager.hexagonControl.ClearAllVisibility();
                SelectionManager.SelectedPlayer.hasAttacked = true;
                exit = true;
            }
            SelectionManager.ClearSelectedTargetable();
            SelectionManager.ClearSelectedEnemy();
        }
    }

    private static ITargetable WaitingTargetable()
    {
        if (SelectionManager.SelectedTargetable == null) return null;
        return SelectionManager.SelectedTargetable;
    }

    public void WantToWalk()
    {
        //if (!Selected()) return;
        _gameManager.fieldManager.hexagonControl.WalkableAreaVisible(SelectionManager.SelectedPlayer, SelectionManager.SelectedPlayer.speed, ListType.MOVE);
    }

    public void ExecuteMove()
    {
        //if (!Selected()) return;
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
    #endregion
}
