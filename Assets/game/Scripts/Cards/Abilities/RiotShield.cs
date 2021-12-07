using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiotShield : MonoBehaviour
{
    public IEnumerator Execute(TeamManager team, GameObject myBigCard, int cardEffectValue, AudioManager juggernautRiotShield, GameObject riotShieldEffect)
    {
        foreach (TeamCharacter character in team.eliteSquad)
        {
            character.shield += cardEffectValue;
            Instantiate(riotShieldEffect, character.transform.position, Quaternion.identity);
        }
        juggernautRiotShield.PlayAudio("juggernautRiotShield");

        yield return new WaitForSeconds(2);
        Transform temp = myBigCard.transform.GetChild(0);
        Destroy(temp.gameObject);
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();
        Utils.cardUseAbility = null;
        Utils.cardInUse = null;
    }
}
