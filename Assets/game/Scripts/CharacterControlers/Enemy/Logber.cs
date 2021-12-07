using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Logber : MonoBehaviour, IEnemy, ITargetable
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
    public GameObject logberAttack;

    //Private
    private Selector _topNode;
    private Enemy _enemy;

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

        Selector targetSelector = new Selector(new List<Node> { hasTarget, getTarget });
        Sequence attackSequence = new Sequence(new List<Node> { hasRange, clearShoot, attack });
        Sequence moveSequence = new Sequence(new List<Node> { hasSteps, hasPath, executeMove, attackSequence });
        Inverter targetSelectorInverter = new Inverter(targetSelector);
        _topNode = new Selector(new List<Node> { targetSelectorInverter, attackSequence, moveSequence });
    }

    public void StartVariables()
    {
        currentHealth = maxHealth = 2500;
        speed = 6;
        power = 400;
        range = 8;
    }

    public void GetPossibleTargets()
    {
        possibleTargets = _enemy.gameManager.teamManager.eliteSquad;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        _enemy.gameManager.audioManager.PlayAudio(("logberHit"));
        GameObject damageText = Instantiate(floatingDamage, transform.position, Quaternion.identity);
        damageText.GetComponent<TextMeshPro>().text = damage.ToString();
        if (currentHealth <= 0)
        {
            StartCoroutine(DieCoroutine());
        }
    }

    public Vector3 ReturnPos()
    {
        return transform.position;
    }

    public void PlayAttackSound()
    {
        _enemy.gameManager.audioManager.PlayAudio("logberAtack");
    }

    public IEnumerator PlayAttackAnimationCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        _enemy.AttackTrigger();
        yield return new WaitForSeconds(0.3f);
        logberAttack.GetComponent<LogberAttack>().end = new Vector3(currentTarget.transform.position.x, currentTarget.transform.position.y - 1, currentTarget.transform.position.z);
        Instantiate(logberAttack, transform.position, Quaternion.identity);
        PlayAttackSound();
        currentTarget.TakeDamage(power);
    }

    public void StartAttackAnimation()
    {
        StartCoroutine(PlayAttackAnimationCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        _enemy.gameManager.enemyManager.enemyListComponent.RemoveEnemyFromList(_enemy);
        _enemy.gameManager.deadEnemies++;

        //Run sounds and animations
        _enemy.gameManager.audioManager.PlayAudio(("enemiesDeath"));
        _enemy.DieTrigger();
        yield return new WaitForSeconds(5);
        _enemy.gameManager.fieldManager.hexagons[hexID].SetFree(true);
        Destroy(gameObject);
    }

    public Selector GetBehaviourNode()
    {
        return behaviourNode;
    }

    public ConditionDT GetConditionNode()
    {
        throw new System.NotImplementedException();
    }

    public bool TargetingBehavior()
    {
        float teamCharacterHealth = 4000f;
        foreach (var targuet in possibleTargets)
        {
            if (teamCharacterHealth > targuet.currentHealth)
            {
                currentTarget = targuet;
                teamCharacterHealth = targuet.currentHealth;
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