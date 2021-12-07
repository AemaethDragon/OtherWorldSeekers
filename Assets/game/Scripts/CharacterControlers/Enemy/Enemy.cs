using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Enemy : MonoBehaviour, IAnimatable
{
    #region variables
    //Public
    [HideInInspector] public List<Vector2> rangeList;
    [SerializeField] public HealthBar healthBar;
    public TheyAreComing enemyType;
    public GameManager gameManager;
    public List<Vector2> latestPath;
    public ITargetable iTargetable;
    public Collider collider;
    public IEnemy iEnemy;
    public bool eChain;
    public Animator animator { get; set; }

    //Private
    [SerializeField] private Animator _animator;
    
    #endregion

    #region Methods

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        iEnemy = GetComponent<IEnemy>();
        iTargetable = GetComponent<ITargetable>();
        gameManager.enemyManager.enemyListComponent.AddEnemyToTheList(this);
        eChain = false;
    }

    private void Start()
    {
        transform.SetParent(gameManager.enemyManager.parent.transform);
        healthBar.SetMaxHealth(iTargetable.maxHealth);
        animator = _animator;
    }

    public void ResetEchain()
    {
        eChain = false;
    }
    
    public void Update()
    {
        healthBar.SetHealth(iTargetable.currentHealth);
    }

    public void DoCoroutine()
    {
        StartCoroutine(MovementManager.MoveCoroutine(latestPath, gameObject));
    }
    

    public void MouseDown()
    {
        SelectionManager.SelectTargetable(iTargetable);
        SelectionManager.SelectEnemy(this);
    }

    #endregion
    
    #region Animations

    public void PlayWalkSound(bool playSound)
    {
        if (playSound)
        {
            gameManager.audioManager.PlayAudio("monsterStep");
        }
        else
        {
            gameManager.audioManager.StopAudio("monsterStep");
        }
    }
    
    public void WalkBool(bool walking)
    {
        PlayWalkSound(walking);
        animator.SetBool("WalkBool", walking);
    }

    public void AttackTrigger()
    {
        animator.SetTrigger("AttackTrigger");
    }

    public void DieTrigger()
    {
        animator.SetTrigger("DieTrigger");
    }

    #endregion
}