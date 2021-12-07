using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarCry : MonoBehaviour
{
    public IEnumerator Execute(TeamManager team, GameObject myBigCard, int cardEffectValue, AudioManager soldierWarCry, GameObject warCryEffect)
    {
        foreach (TeamCharacter character in team.eliteSquad)
        {
            Instantiate(warCryEffect, character.transform.position, Quaternion.identity);
            character.AddSpeed(cardEffectValue);
        }
        soldierWarCry.PlayAudio("soldierWarCry");

        yield return new WaitForSeconds(2);

        Transform temp = myBigCard.transform.GetChild(0);
        Destroy(temp.gameObject);
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();
        Utils.cardUseAbility = null;
        Utils.cardInUse = null;
    }
}
