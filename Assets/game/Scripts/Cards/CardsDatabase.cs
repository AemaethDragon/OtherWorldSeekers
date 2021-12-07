using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    CANNONJUMP,
    FLING,
    GRENADELAUNCHER,
    SHIELD,
    RIOTSHIELD,
    AIRSUPPORT,
    BURSTFIRE,
    HARPOONJUMP,
    REGENBULLET,
    WARCRY,
    BEARTRAP,
    LASTSHOT,
    SECONDWIND,
    LONGSHOT,
    HEADSHOT,
    BLINK,
    ENERGYCHAIN,
    MAGNETICIMPACT,
    HOLYWISH,
    HEALINGSERINGE

}

public class CardsDatabase : MonoBehaviour
{
    public static List<Card> cardList = new List<Card>(); 

    /// <summary>
    /// Creates he list of all cards
    /// </summary>
    private void Awake()
    {
        GameManager temp = transform.GetComponentInParent<GameManager>();
        //jugernaut

        cardList.Add(new Card(0, Lang.Fields["cannonJump_N"], 50, 4, 300, 5, Lang.Fields["cannonJump_D"], Resources.Load<Sprite>("Cards Images/CannonJump"), CardType.CANNONJUMP, TheASquad.JUGGERNAUT, temp));
        cardList.Add(new Card(1, Lang.Fields["fling_N"], 40, 1, 200, 8, Lang.Fields["fling_D"], Resources.Load<Sprite>("Cards Images/Fling"), CardType.FLING, TheASquad.JUGGERNAUT, temp));
        cardList.Add(new Card(2, Lang.Fields["grenadeLauncher_N"], 60, 12, 700, 5, Lang.Fields["grenadeLauncher_D"], Resources.Load<Sprite>("Cards Images/GrennadeLauncher"), CardType.GRENADELAUNCHER, TheASquad.JUGGERNAUT, temp));
        cardList.Add(new Card(3, Lang.Fields["shield_N"], 50, 0, 1200, 0, Lang.Fields["shield_D"], Resources.Load<Sprite>("Cards Images/Shield"), CardType.SHIELD, TheASquad.JUGGERNAUT, temp));
        cardList.Add(new Card(4, Lang.Fields["riotShield_N"], 100, 0, 900, 0, Lang.Fields["riotShield_D"], Resources.Load<Sprite>("Cards Images/RiotShield"), CardType.RIOTSHIELD, TheASquad.JUGGERNAUT, temp));

        //soldier

        cardList.Add(new Card(5, Lang.Fields["airSupport_N"], 60, 4, 500, 0, Lang.Fields["airSupport_D"], Resources.Load<Sprite>("Cards Images/AirSupport"), CardType.AIRSUPPORT, TheASquad.SOLDIER, temp));
        cardList.Add(new Card(6, Lang.Fields["burstFire_N"], 50, 15, 300, 3, Lang.Fields["burstFire_D"], Resources.Load<Sprite>("Cards Images/BurstFire"), CardType.BURSTFIRE, TheASquad.SOLDIER, temp));
        cardList.Add(new Card(7, Lang.Fields["harpoonJump_N"], 20, 5, 0, 0, Lang.Fields["harpoonJump_D"], Resources.Load<Sprite>("Cards Images/HarpoonJump"), CardType.HARPOONJUMP, TheASquad.SOLDIER, temp));
        cardList.Add(new Card(8, Lang.Fields["regenBullet_N"], 80, 10, 700, 0, Lang.Fields["regenBullet_D"], Resources.Load<Sprite>("Cards Images/RegenBullet"), CardType.REGENBULLET, TheASquad.SOLDIER, temp));
        cardList.Add(new Card(9, Lang.Fields["warCry_N"], 100, 0, 8, 0, Lang.Fields["warCry_D"], Resources.Load<Sprite>("Cards Images/WarCry"), CardType.WARCRY, TheASquad.SOLDIER, temp));

        //sniper

        cardList.Add(new Card(10, Lang.Fields["bearTrap_N"], 20, 1, 200, 0, Lang.Fields["bearTrap_D"], Resources.Load<Sprite>("Cards Images/BearTrap"), CardType.BEARTRAP, TheASquad.SNIPER, temp));
        cardList.Add(new Card(11, Lang.Fields["lastShot_N"], 0, 15, 5, 0, Lang.Fields["lastShot_D"], Resources.Load<Sprite>("Cards Images/LastShot"), CardType.LASTSHOT, TheASquad.SNIPER, temp));
        cardList.Add(new Card(12, Lang.Fields["secondWind_N"], 60, 0, 0, 0, Lang.Fields["secondWind_D"], Resources.Load<Sprite>("Cards Images/SecondWind"), CardType.SECONDWIND, TheASquad.SNIPER, temp));
        cardList.Add(new Card(13, Lang.Fields["longShot_N"], 70, 0, 500, 0, Lang.Fields["longShot_D"], Resources.Load<Sprite>("Cards Images/LongShot"), CardType.LONGSHOT, TheASquad.SNIPER, temp));
        cardList.Add(new Card(14, Lang.Fields["headShot_N"], 100, 12, 0, 0, Lang.Fields["headShot_D"], Resources.Load<Sprite>("Cards Images/HeadShot"), CardType.HEADSHOT, TheASquad.SNIPER, temp));

        //mage

        cardList.Add(new Card(15, Lang.Fields["blink_N"], 20, 4, 0, 0, Lang.Fields["blink_D"], Resources.Load<Sprite>("Cards Images/Teleport"), CardType.BLINK, TheASquad.MAGE, temp));
        cardList.Add(new Card(16, Lang.Fields["energyChain_N"], 60, 5, 400, 100, Lang.Fields["energyChain_D"], Resources.Load<Sprite>("Cards Images/EnergyChain"), CardType.ENERGYCHAIN, TheASquad.MAGE, temp));
        cardList.Add(new Card(17, Lang.Fields["magneticImpact_N"], 80, 15, 700, 5, Lang.Fields["magneticImpact_D"], Resources.Load<Sprite>("Cards Images/MagneticImpact"), CardType.MAGNETICIMPACT, TheASquad.MAGE, temp));
        cardList.Add(new Card(18, Lang.Fields["holyWish_N"], 100, 0, 1000, 0, Lang.Fields["holyWish_D"], Resources.Load<Sprite>("Cards Images/holyWish"), CardType.HOLYWISH, TheASquad.MAGE, temp));
        cardList.Add(new Card(19, Lang.Fields["healingSyringe_N"], 40, 1, 500, 0, Lang.Fields["healingSyringe_D"], Resources.Load<Sprite>("Cards Images/HealingSeringe"), CardType.HEALINGSERINGE, TheASquad.MAGE, temp));
    }
}