using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Nomnom : MonoBehaviour, IEnemy, ITargetable
{
    #region Variables

    //Public
    public Selector behaviourNode => _topNode;
    public List<TeamCharacter> possibleTargets { get; set; }
    public TeamCharacter currentTarget { get; set; }
    public Vector2 hexID { get; set; }
    public int currentHealth { get; set; }
    public int maxHealth { get; set; }
    public int power { get; set; }
    public int range { get; set; }
    public int speed { get; set; }
    public GameObject floatingDamage;
    public GameObject nomnomExplosionP;
    public GameObject nomnomAttack;

    //Private
    private Selector _topNode;
    private Enemy _enemy;
    private int _explosionRange;
    private int _explosionPower;
    #endregion

    #region Methods

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        CreateBehaviourTree();
        StartVariables();
    }

    private void Start()
    {
        GetPossibleTargets();
    }

    private void CreateBehaviourTree()
    {
        HasTargetNode hasTarget = new HasTargetNode(_enemy);
        GetTargetNode getTarget = new GetTargetNode(_enemy);
        HasRangeNode hasRange = new HasRangeNode(_enemy);
        ClearShootNode clearShoot = new ClearShootNode(_enemy);
        AttackNode attack = new AttackNode(_enemy);
        HasStepsNode hasSteps = new HasStepsNode(_enemy);
        HasPathNode hasPath = new HasPathNode(_enemy);
        ExecuteMoveNode executeMove = new ExecuteMoveNode(_enemy);

        Selector targetSelector = new Selector(new List<Node> {hasTarget, getTarget});
        Sequence attackSequence = new Sequence(new List<Node> {hasRange, clearShoot, attack});
        Sequence moveSequence = new Sequence(new List<Node> {hasSteps, hasPath, executeMove, attackSequence});
        Inverter targetSelectorInverter = new Inverter(targetSelector);
        _topNode = new Selector(new List<Node> {targetSelectorInverter, attackSequence, moveSequence});
    }

    public void StartVariables()
    {
        currentHealth = maxHealth = 4200;
        speed = 8;
        power = 350;
        range = 2;
        _explosionRange = 2;
        _explosionPower = 380;
    }

    public void GetPossibleTargets()
    {
        possibleTargets = _enemy.gameManager.teamManager.eliteSquad;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        _enemy.gameManager.audioManager.PlayAudio(("nomnomHit"));
        GameObject damageText = Instantiate(floatingDamage, transform.position, Quaternion.identity);
        damageText.GetComponent<TextMeshPro>().text = damage.ToString();
        if (currentHealth <= 0)
        {
            StartCoroutine(DieCoroutine());
        }
    }

    public void PlayAttackSound()
    {
        _enemy.gameManager.audioManager.PlayAudio("nomnomAtack");
    }

    public IEnumerator PlayAttackAnimationCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        _enemy.AttackTrigger();
        yield return new WaitForSeconds(0.3f);
        GameObject nomnomAttackClone = Instantiate(nomnomAttack, transform.position, Quaternion.identity);
        Destroy(nomnomAttackClone,1f);
        PlayAttackSound();
        currentTarget.TakeDamage(power);
    }
    
    public void StartAttackAnimation()
    {
        StartCoroutine(PlayAttackAnimationCoroutine());
    }

    public Vector3 ReturnPos()
    {
        return transform.position;
    }

    public Selector GetBehaviourNode()
    {
        return behaviourNode;
    }

    public ConditionDT GetConditionNode()
    {
        throw new NotImplementedException();
    }

    private IEnumerator DieCoroutine()
    {
        List<Vector2> temp = Utils.CreateRangeList(_enemy.gameManager.fieldManager.graph, hexID, _explosionRange, ListType.ATTACK);
        foreach (var target in possibleTargets)
        {
            if (temp.Contains(target.hexID))
            {
                target.TakeDamage(_explosionPower);
            }
        }
        _enemy.gameManager.enemyManager.enemyListComponent.RemoveEnemyFromList(_enemy);
        _enemy.gameManager.deadEnemies++;

        //Run sounds and animations
        Instantiate(nomnomExplosionP, transform.position, Quaternion.identity);
        _enemy.gameManager.audioManager.PlayAudio(("nomnomExplosion"));
        _enemy.gameManager.fieldManager.hexagons[hexID].SetFree(true);
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }

    public bool TargetingBehavior()
    {
        float tempDistance = 9999999999999999f;

        foreach (var target in possibleTargets)
        {
            float tempDistance2 = Vector3.Distance(_enemy.transform.position, target.transform.position);

            if (tempDistance2 < tempDistance)
            {
                tempDistance = tempDistance2;
                currentTarget = target;
            }
        }
        return true;
    }
    public void Rotation()
    {
        Vector3 tempvector = currentTarget.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(tempvector);
    }
    #endregion
}