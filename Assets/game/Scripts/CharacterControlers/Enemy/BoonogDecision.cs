using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonogDecision : MonoBehaviour
{
    private GameManager _gameManager;
    private Boonog _boonog;
    private Graph _graph;

    private void Awake()
    {
        _boonog = GetComponent<Boonog>();
        _gameManager = FindObjectOfType<GameManager>();
        _graph = _gameManager.fieldManager.graph;
    }

    public void Scream()
    {
        //Run scream sound
        foreach (var enemy in _gameManager.enemyManager.enemyListComponent.enemyList)
        {
            if (enemy.enemyType == TheyAreComing.GNOME)
            {
                enemy.iEnemy.currentTarget = _boonog.currentTarget;
            }
        }
    }

    public void FarPatrol()
    {
        List<Vector2> temp = Utils.CreateRangeList(_graph, _boonog.hexID, 2, ListType.ATTACK);
        List<Vector2> finalTemp = new List<Vector2>();
        finalTemp.AddRange(_boonog.spawn.farRange);

        foreach (var vector2 in temp)
        {
            if (finalTemp.Contains(vector2))
            {
                finalTemp.Remove(vector2);
            }
        }

        int decision = UnityEngine.Random.Range(0, finalTemp.Count);
        List<Vector2> path = Utils.GetBestRoute(finalTemp[decision], _boonog.hexID, ref _graph, _boonog.speed);
        Move(path);
    }

    public void ClosePatrol()
    {
        List<Vector2> temp = Utils.CreateRangeList(_graph, _boonog.hexID, 2, ListType.ATTACK);
        List<Vector2> finalTemp = new List<Vector2>();
        finalTemp.AddRange(_boonog.spawn.closeRange);

        foreach (var vector2 in temp)
        {
            if (finalTemp.Contains(vector2))
            {
                finalTemp.Remove(vector2);
            }
        }

        int decision = UnityEngine.Random.Range(0, finalTemp.Count);
        List<Vector2> path = Utils.GetBestRoute(finalTemp[decision], _boonog.hexID, ref _graph, _boonog.speed);
        Move(path);
    }

    public void Defend()
    {
        TeamCharacter temp = null;
        Vector2 finalSelectedPos = Vector2.zero;
        Vector2 teamSelectedPos = Vector2.zero;
        float lastDistance = Single.PositiveInfinity;

        //Find which enemy is closer
        foreach (var team in _gameManager.teamManager.eliteSquad)
        {
            if (temp == null) { temp = team; }

            var distance = Vector2.Distance(_boonog.spawn.hexID, temp.hexID);
            if (!(distance < lastDistance)) continue;
            temp = team;
            lastDistance = distance;
            teamSelectedPos = team.hexID;
            //Instantiate(_boonog.floatingArrow, teamSelectedPos, Quaternion.identity);
        }

        //Find the hexagon that is closest to the enemy
        lastDistance = Single.PositiveInfinity;
        foreach (var hex in _boonog.spawn.closeRange)
        {
            if (finalSelectedPos == Vector2.zero) { finalSelectedPos = hex; }

            var distance = Vector2.Distance(hex, teamSelectedPos);
            if (!(distance < lastDistance)) continue;
            lastDistance = distance;
            finalSelectedPos = hex;
        }
        List<Vector2> path = Utils.GetBestRoute(finalSelectedPos, _boonog.hexID, ref _graph, _boonog.speed);
        Move(path);
    }

    public void Harden()
    {
        _boonog.harden = true;
    }

    public void Shoot()
    {
        _boonog.currentTarget.TakeDamage(_boonog.power);
        _boonog.PlayAttackSound();
    }

    public bool ClearShot()
    {
        Vector3 direction = _boonog.currentTarget.transform.position - _boonog.transform.position;
        if (!Physics.Raycast(_boonog.transform.position, direction, out var hit)) return false;
        return hit.transform.CompareTag(_boonog.currentTarget.tag);
    }

    public bool SpawnHealth()
    {
        return _boonog.spawn.currentHealth > _boonog.spawn.maxHealth / 2;
    }

    public bool CanAttack()
    {
        List<Vector2> temp = Utils.CreateRangeList(_graph, _boonog.hexID, _boonog.range, ListType.ATTACK);
        foreach (var team in _gameManager.teamManager.eliteSquad)
        {
            if (!temp.Contains(team.hexID)) continue;
            _boonog.currentTarget = team;
            _boonog.canAttack = true;
            return true;
        }
        return false;
    }

    public bool CanSee()
    {
        List<Vector2> temp = Utils.CreateRangeList(_graph, _boonog.hexID, _boonog.range * 3, ListType.MOVE);
        foreach (var team in _gameManager.teamManager.eliteSquad)
        {
            if (!temp.Contains(team.hexID)) continue;
            _boonog.currentTarget = team;
            _boonog.canAttack = false;
            return true;
        }
        return false;
    }

    public bool MyHealth()
    {
        return _boonog.currentHealth > _boonog.maxHealth * 0.4;
    }

    public bool DistanceToSpawn()
    {
        List<Vector2> temp = Utils.CreateRangeList(_graph, _boonog.hexID, 5, ListType.MOVE);
        return temp.Contains(_boonog.spawn.hexID);
    }

    private void Move(List<Vector2> path)
    {
        StartCoroutine(HexCheckCorroutine());
        StartCoroutine(MovementManager.MoveCoroutine(path, _boonog.gameObject));
        _boonog.hexID = path[path.Count - 1];
    }
    private IEnumerator HexCheckCorroutine()
    {
        _gameManager.fieldManager.hexagonControl.CheckObstacles();
        yield return new WaitForSeconds(0.1f);
    }
    private void PlayWalkSound()
    {
        _gameManager.audioManager.PlayAudio("monsterStep");
    }
}
