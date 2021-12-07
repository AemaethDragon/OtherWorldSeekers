using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWish : MonoBehaviour
{
    public IEnumerator Execute(TeamManager team, GameObject myBigCard, int effectValue, AudioManager kineticmageHolyWish, GameObject healingEffect)
    {
        foreach (TeamCharacter teamCharacter in team.eliteSquad)
        {
            teamCharacter.Heal(effectValue);
            Instantiate(healingEffect, teamCharacter.transform.position, Quaternion.identity);
        }
        kineticmageHolyWish.PlayAudio("kineticmageHolyWish");

        yield return new WaitForSeconds(2);

        Transform temp = myBigCard.transform.GetChild(0);
        Destroy(temp.gameObject);
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();
        Utils.cardUseAbility = null;
        Utils.cardInUse = null;
    }
}
