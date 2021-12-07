using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Boonog : MonoBehaviour, IEnemy, ITargetable
{
    #region Variables

    //Public
    public List<TeamCharacter> possibleTargets { get; set; }
    public ConditionDT decisionNode => _topNode;
    public TeamCharacter currentTarget { get; set; }
    public Enemy enemy;
    public Nest spawn;
    public Vector2 hexID { get; set; }
    public int currentHealth { get; set; }
    public int maxHealth { get; set; }
    public int power { get; set; }
    public int range { get; set; }
    public int speed { get; set; }
    public bool canAttack;
    public bool harden;
    public GameObject floatingDamage;
    //public GameObject floatingArrow;
    public GameObject boonogAttack;
    public GameObject ShieldParticle;


    //Private
    private BoonogDecision _boonogDecision;
    private ConditionDT _topNode;

    #endregion

    #region Methods

    private void Awake()
    {
        _boonogDecision = GetComponent<BoonogDecision>();
        enemy = GetComponent<Enemy>();
        StartVariables();
    }

    private void Start()
    {
        ShieldParticle.SetActive(false);
        GetPossibleTargets();
        CreateDecisionTree();
    }

    private void CreateDecisionTree()
    {
        //Actions
        ActionDT _scream = new ActionDT(() => _boonogDecision.Scream()); //Needs sound  DONE
        ActionDT _closePatrol = new ActionDT(() => _boonogDecision.ClosePatrol());
        ActionDT _defend = new ActionDT(() => _boonogDecision.Defend());
        ActionDT _harden = new ActionDT(() => _boonogDecision.Harden()); //DONE
        ActionDT _shoot = new ActionDT(() => _boonogDecision.Shoot()); 
        ActionDT _farPatrol = new ActionDT(() => _boonogDecision.FarPatrol());
        
        //Conditions
        ConditionDT _myHealth = new ConditionDT(() => _boonogDecision.MyHealth(), _closePatrol, _scream);
        ConditionDT _distanceSpawn = new ConditionDT(() => _boonogDecision.DistanceToSpawn(), _harden, _defend);
        ConditionDT _clearShot = new ConditionDT(() => _boonogDecision.ClearShot(), _shoot, _harden);
        ConditionDT _spawnHealth = new ConditionDT(() => _boonogDecision.SpawnHealth(), _farPatrol, _myHealth);
        ConditionDT _canAttack = new ConditionDT(() => _boonogDecision.CanAttack(), _clearShot, _distanceSpawn);
        _topNode = new ConditionDT(() => _boonogDecision.CanSee(), _canAttack, _spawnHealth);
    }

    public void StartVariables()
    {
        currentHealth = maxHealth = 4000;
        speed = 4;
        power = 500;
        range = 6;
    }

    public void GetPossibleTargets()
    {
        possibleTargets = enemy.gameManager.teamManager.eliteSquad;
    }

    public void TakeDamage(int damage)
    {
        if (harden)
        {
            ShieldParticle.SetActive(true);
            currentHealth -= damage / 2;
            int halfDamage = damage / 2;
            enemy.gameManager.audioManager.PlayAudio(("boonogHit"));
            GameObject damageText = Instantiate(floatingDamage, transform.position, Quaternion.identity);
            damageText.GetComponent<TextMeshPro>().text = halfDamage.ToString();
        }
        else
        {
            ShieldParticle.SetActive(false);
            currentHealth -= damage;
            enemy.gameManager.audioManager.PlayAudio(("boonogHit"));
            GameObject damageText = Instantiate(floatingDamage, transform.position, Quaternion.identity);
            damageText.GetComponent<TextMeshPro>().text = damage.ToString();
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(DieCoroutine());
        }
    }

    public void PlayAttackSound()
    {
        enemy.gameManager.audioManager.PlayAudio("boonogAtack");
    }

    public IEnumerator PlayAttackAnimationCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        enemy.AttackTrigger();
        yield return new WaitForSeconds(0.3f);
        GameObject boonogAttackClone = Instantiate(boonogAttack, transform.position, Quaternion.identity);
        Destroy(boonogAttackClone,1f);
        PlayAttackSound();
        currentTarget.TakeDamage(power);
    }
    
    public void StartAttackAnimation()
    {
        StartCoroutine(PlayAttackAnimationCoroutine());
    }

    public Selector GetBehaviourNode()
    {
        return null;
    }

    public ConditionDT GetConditionNode()
    {
        decisionNode.Execute();
        return decisionNode;
    }

    public Vector3 ReturnPos()
    {
        return transform.position;
    }

    private IEnumerator DieCoroutine()
    {
        spawn.boonog = null;
        enemy.gameManager.enemyManager.enemyListComponent.RemoveEnemyFromList(enemy);
        enemy.gameManager.deadEnemies++;

        //Run sounds and animations
        enemy.gameManager.audioManager.PlayAudio(("enemiesDeath"));
        enemy.DieTrigger();
        yield return new WaitForSeconds(5);
        enemy.gameManager.fieldManager.hexagons[hexID].SetFree(true);
        Destroy(gameObject);
    }

    public bool TargetingBehavior()
    {
        return true;
    }
    public void Rotation()
    {
        if (currentTarget == null) return;
        Vector3 tempvector = currentTarget.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(tempvector);
    }

    #endregion
}