using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamCharacter : MonoBehaviour, IAnimatable
{
    #region variables
    //public
    [HideInInspector] public GameObject target = null;
    [SerializeField] public int energy, maxEnergy, maxSpeed; // correspond the mana of the character
    public TheASquad aSquad;
    public Slider healthSlider, manaSlider, speedSlider, shieldSlider;
    public SelectionController selectionController;
    public PlayerDeck myPlayerDeck;
    public GameObject floatingDamage;
    public GameObject soldierLaser;
    public GameObject juggernautLaser;
    public GameObject sniperLaser;
    public GameObject mageAttack;
    public GameObject shieldParticle;
    public Animator animator { get; set; }

    //Stats
    public Vector2 hexID; //the ID of the hexagon that the character is
    public bool hasAttacked; //check if the player has attacked in the current turn
    public int currentHealth; //correspond to the life of the character
    public int maxHealth;
    public int shield;
    public int speed; //correspond to the steps that the character can walk
    public int power; //correspond to the attack of the character
    public int range; //correspond to the range of the character

    //private
    [SerializeField] private Animator _animator;
    private List<Vector2> rangePositions;
    private GameManager _gameManager;
    private EnemyManager manager;
    private Camera _camera;

    #endregion
    
    #region Methods

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _camera = Camera.main;
    }

    private void Start()
    {
        SetMaxBar();
        maxEnergy = energy;
        maxSpeed = speed;
        hasAttacked = false;
        animator = _animator;
    }

    private void Update()
    {
        GetTarget();
        SetBar();
    }

    //Check if the target is in the range of the player
    public bool CheckIfTargetInRange()
    {
        foreach (var tar in rangePositions)
        {
            if (target.transform.GetComponent<Enemy>().iTargetable.hexID == tar) return true; //returns if the enemy is on the hexagon inside the rangePosition
        }
        return false;
    }

    //will get the target
    public void GetTarget()
    {
        if (Input.GetButtonDown("Fire1")) //if we press the left mouse button
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;

            if (Physics.Raycast(ray, out rayHit))
            {
                if (rayHit.transform.tag == "Enemy") //if where we press the button is a gameObject where the tag is enemy
                {
                    target = rayHit.transform.gameObject; //we said that our target is where we pressed
                }
            }
        }
    }

    public void ResetValues()
    {
        speed = maxSpeed;
        energy = maxEnergy;
        hasAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        shield -= damage;
        if (shield >= 0) return;
        currentHealth += shield;
        shield = 0;
        GameObject damageText = Instantiate(floatingDamage, transform.position, Quaternion.identity);
        damageText.GetComponent<TextMeshPro>().text = damage.ToString();

        if (aSquad == TheASquad.SNIPER || aSquad == TheASquad.SOLDIER)
        {
            _gameManager.audioManager.PlayAudio(("maleDamage"));
        }
        else if (aSquad == TheASquad.MAGE || aSquad == TheASquad.JUGGERNAUT)
        {
            _gameManager.audioManager.PlayAudio(("femaleDamage"));
        }

        if (currentHealth <= 0)
        {
            StartCoroutine(DieCoroutine());
            
            if (aSquad == TheASquad.SOLDIER)
            {
                _gameManager.audioManager.PlayAudio(("soldierDeath"));
            }
            else if (aSquad == TheASquad.JUGGERNAUT)
            {
                _gameManager.audioManager.PlayAudio(("juggernautDeath"));
            }
            else if (aSquad == TheASquad.MAGE)
            {
                _gameManager.audioManager.PlayAudio(("kineticmageDeath"));
            }
            else if (aSquad == TheASquad.SNIPER)
            {
                _gameManager.audioManager.PlayAudio(("sniperDeath"));
            }
        }
    }

    public void PlayAttackSound()
    {
        if (aSquad == TheASquad.SOLDIER)
        {
            _gameManager.audioManager.PlayAudio(("soldierAtack"));
        }
        else if (aSquad == TheASquad.JUGGERNAUT)
        {
            _gameManager.audioManager.PlayAudio(("juggernautAtack"));
        }
        else if (aSquad == TheASquad.MAGE)
        {
            _gameManager.audioManager.PlayAudio(("kineticmageAtack"));
        }
        else if (aSquad == TheASquad.SNIPER)
        {
            _gameManager.audioManager.PlayAudio(("sniperAtack"));
        }
    }

    public void PlayWeaponEffect(Vector3 enemyPos)
    {
        
        if (aSquad == TheASquad.SOLDIER)
        {
            GameObject soldierLaserClone = Instantiate(soldierLaser, transform.position, Quaternion.identity);
            StartCoroutine(soldierLaserClone.GetComponent<Particle>().MoveParticle(enemyPos,50f));
        }
        else if (aSquad == TheASquad.JUGGERNAUT)
        {GameObject juggernautLaserClone = Instantiate(juggernautLaser, transform.position, Quaternion.identity);
            StartCoroutine(juggernautLaserClone.GetComponent<Particle>().MoveParticle(enemyPos,50f));
            
        }
        else if (aSquad == TheASquad.MAGE)
        {
            GameObject mageAttackClone = Instantiate(mageAttack, transform.position, Quaternion.identity);
            StartCoroutine(mageAttackClone.GetComponent<Particle>().MoveParticle(enemyPos,20f));
        }
        else if (aSquad == TheASquad.SNIPER)
        {
            GameObject sniperLaserClone = Instantiate(sniperLaser, transform.position, Quaternion.identity);
            StartCoroutine(sniperLaserClone.GetComponent<Particle>().MoveParticle(enemyPos,50f));
        }
    }
    public void PlayDamageSound()
    {

    }
    public void Heal(int health)
    {
        currentHealth += health;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    public void AddSpeed(int speed)
    {
         this.speed += speed;
        if (this.speed > maxSpeed) this.speed = maxSpeed;
    }
    private IEnumerator DieCoroutine()
    {
        _gameManager.teamManager.eliteSquad.Remove(this);
        
        //Run sounds and animations
        DieTrigger();
        yield return new WaitForSeconds(5);
        _gameManager.fieldManager.hexagons[hexID].SetFree(true);
        Destroy(gameObject);
    }
    
    public void SetMaxBar()
    {
        manaSlider.maxValue = energy;
        manaSlider.value = energy;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

        speedSlider.maxValue = speed;
        speedSlider.value = speed;

        shieldSlider.maxValue = 10000;
        shieldSlider.value = 0;
    }

    public void SetBar()
    {
        manaSlider.value = energy;
        healthSlider.value = currentHealth;
        speedSlider.value = speed;
        shieldSlider.value = shield;
        if (shield > 0)
        {
            shieldParticle.SetActive(true);
        }
        else
        {
            shieldParticle.SetActive(false);
        }
    }
    #endregion
    
    #region Animations

    
    public void PlayWalkSound(bool playSound)
    {
        if (playSound)
        {
            _gameManager.audioManager.PlayAudio("characterWalk");
        }
        else
        {
            _gameManager.audioManager.StopAudio("characterWalk");
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
