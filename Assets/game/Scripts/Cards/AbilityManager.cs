using UnityEngine;



public class AbilityManager : MonoBehaviour
{
    #region variables

    [SerializeField] private GameObject bearTrap;
    [SerializeField] private GameObject healingSering;
    [SerializeField] private GameObject missil;

    #region Abilities Variables

    private HealingSyringeAbility _healingSyringeAbility;
    private BearTrapAbility _bearTrapAbility;
    private MagneticImpact _magneticImpact;
    private GrendeLaucher _grendeLaucher;
    private HarpoonJump _harpoonJump;
    private RegenBullet _regenBullet;
    private EnergyChain _energyChain;
    private CannonJump _cannonJump;
    private RiotShield _riotShield;
    private AirSupport _airSupport;
    private SecondWind _secondWind;
    private BurstFire _burstFire;
    private LastShot _lastShot;
    private LongShot _longShot;
    private HeadShot _headShot;
    private HolyWish _holyWish;
    private WarCry _warCry;
    private Shield _shield;
    private Blinck _blinck;
    private Fling _fling;
    #endregion

    
    //Public
    public GameObject bigCard;
    public GameObject grenadeLauncherAttack;
    public GameObject shieldJuggernaut;
    public GameObject burstFireAnimation;
    public GameObject healingEffect;
    public GameObject recoverSpeed;
    public GameObject lastShot;
    public GameObject longShot;
    public GameObject headShot;
    public GameObject blink;
    public GameObject energyChain;
    public GameObject lightning;
    public GameObject mageAttack;
    public GameObject magneticImpact;
    public GameObject cannonJump;
    public GameObject HarpoonParticle;

    //Private
    private GameManager gameManager;

    #endregion

    #region Methos

    private void Awake()
    {
        gameManager = GetComponentInParent<GameManager>();

        #region Abilities GetComponents

        _healingSyringeAbility = GetComponent<HealingSyringeAbility>();
        _bearTrapAbility = GetComponent<BearTrapAbility>();
        _magneticImpact = GetComponent<MagneticImpact>();
        _grendeLaucher = GetComponent<GrendeLaucher>();
        _harpoonJump = GetComponent<HarpoonJump>();
        _regenBullet = GetComponent<RegenBullet>();
        _energyChain = GetComponent<EnergyChain>();
        _cannonJump = GetComponent<CannonJump>();
        _riotShield = GetComponent<RiotShield>();
        _airSupport = GetComponent<AirSupport>();
        _secondWind = GetComponent<SecondWind>();
        _burstFire = GetComponent<BurstFire>();
        _lastShot = GetComponent<LastShot>();
        _longShot = GetComponent<LongShot>();
        _headShot = GetComponent<HeadShot>();
        _holyWish = GetComponent<HolyWish>();
        _warCry = GetComponent<WarCry>();
        _shield = GetComponent<Shield>();
        _blinck = GetComponent<Blinck>();
        _fling = GetComponent<Fling>();

        #endregion
    }

    public void DoAbility(CardType cardType, Card currentCard)
    {
        SelectionManager.EnableReturn();
        switch (cardType)
        {
            case CardType.CANNONJUMP:
                StartCoroutine(_cannonJump.Execute(SelectionManager.SelectedPlayer, currentCard.range, currentCard.effectValue, currentCard.aoeRange, bigCard, gameManager.enemyManager.enemyListComponent.enemyList, gameManager.fieldManager, gameManager.audioManager, cannonJump));
                break;
            case CardType.FLING:
                StartCoroutine(_fling.Execute(SelectionManager.SelectedPlayer,currentCard.range, currentCard.aoeRange, bigCard, gameManager.fieldManager, gameManager.audioManager));
                break;
            case CardType.GRENADELAUNCHER:
                StartCoroutine(_grendeLaucher.Execute(SelectionManager.SelectedPlayer, currentCard.range, currentCard.aoeRange, currentCard.effectValue, gameManager.enemyManager.enemyListComponent.enemyList, bigCard, gameManager.fieldManager, gameManager.audioManager, grenadeLauncherAttack));
                break;
            case CardType.SHIELD:
                StartCoroutine(_shield.Execute(SelectionManager.SelectedPlayer, bigCard, currentCard.effectValue, gameManager.audioManager, shieldJuggernaut));
                break;
            case CardType.RIOTSHIELD:
                StartCoroutine(_riotShield.Execute(gameManager.teamManager, bigCard, currentCard.effectValue, gameManager.audioManager, shieldJuggernaut));
                break;
            case CardType.AIRSUPPORT:
                StartCoroutine(_airSupport.Execute(SelectionManager.SelectedPlayer, bigCard,currentCard.range, currentCard.effectValue,gameManager.fieldManager, gameManager.enemyManager.enemyListComponent.enemyList,missil, gameManager.audioManager, grenadeLauncherAttack));
                break;
            case CardType.BURSTFIRE:
                StartCoroutine(_burstFire.Execute(SelectionManager.SelectedPlayer, bigCard, currentCard.range, currentCard.effectValue, currentCard.aoeRange, gameManager.fieldManager, gameManager.audioManager, burstFireAnimation));
                break;
            case CardType.HARPOONJUMP:
                StartCoroutine(_harpoonJump.Execute(SelectionManager.SelectedPlayer, bigCard, currentCard.range, gameManager.fieldManager, gameManager.audioManager,HarpoonParticle));
                break;
            case CardType.REGENBULLET:
                StartCoroutine(_regenBullet.Execute(SelectionManager.SelectedPlayer, bigCard, currentCard.range, currentCard.effectValue, gameManager.fieldManager, gameManager.audioManager, healingEffect, burstFireAnimation));
                break;
            case CardType.WARCRY:
                StartCoroutine(_warCry.Execute(gameManager.teamManager, bigCard, currentCard.effectValue, gameManager.audioManager, recoverSpeed));
                break;
            case CardType.BEARTRAP:
                StartCoroutine(_bearTrapAbility.Execute(SelectionManager.SelectedPlayer, bigCard, currentCard.range, currentCard.effectValue, bearTrap, gameManager.fieldManager,gameManager.audioManager));
                break;
            case CardType.LASTSHOT:
                StartCoroutine(_lastShot.Execute(SelectionManager.SelectedPlayer, bigCard, currentCard.range, currentCard.effectValue, gameManager.fieldManager, gameManager.audioManager, lastShot));
                break;
            case CardType.SECONDWIND:
                StartCoroutine(_secondWind.Execute(SelectionManager.SelectedPlayer, bigCard, gameManager.audioManager, recoverSpeed));
                break;
            case CardType.LONGSHOT:
                StartCoroutine(_longShot.Execute(bigCard, currentCard.effectValue,gameManager.fieldManager,SelectionManager.SelectedPlayer, gameManager.audioManager, longShot));
                break;
            case CardType.HEADSHOT:
                StartCoroutine(_headShot.Execute(SelectionManager.SelectedPlayer, bigCard, currentCard.range, gameManager.fieldManager, gameManager.audioManager, headShot));
                break;
            case CardType.BLINK:
                StartCoroutine(_blinck.Execute(SelectionManager.SelectedPlayer, bigCard, currentCard.range, gameManager.fieldManager, gameManager.audioManager, blink));
                break;
            case CardType.ENERGYCHAIN:
                StartCoroutine(_energyChain.Execute(SelectionManager.SelectedPlayer, bigCard, currentCard.range, currentCard.effectValue, currentCard.aoeRange, gameManager.fieldManager, gameManager.enemyManager.enemyListComponent.enemyList, gameManager.audioManager, energyChain, lightning));
                break;
            case CardType.MAGNETICIMPACT:
                StartCoroutine(_magneticImpact.Execute(SelectionManager.SelectedPlayer, bigCard, currentCard.effectValue, currentCard.range, currentCard.aoeRange, gameManager.enemyManager.enemyListComponent.enemyList, gameManager.teamManager, gameManager.fieldManager, gameManager.audioManager, mageAttack,magneticImpact));
                break;
            case CardType.HOLYWISH:
                StartCoroutine(_holyWish.Execute(gameManager.teamManager, bigCard, currentCard.effectValue, gameManager.audioManager, healingEffect));
                break;
            case CardType.HEALINGSERINGE:
                StartCoroutine(_healingSyringeAbility.Execute(SelectionManager.SelectedPlayer, bigCard, currentCard.range, currentCard.effectValue, gameManager.fieldManager, healingSering, healingEffect));
                break;
        }
    }
    #endregion
}

