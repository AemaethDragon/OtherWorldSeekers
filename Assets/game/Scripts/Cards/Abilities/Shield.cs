using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public IEnumerator Execute(TeamCharacter teamCharacter, GameObject myBigCard, int cardEffectValue, AudioManager juggernautShield, GameObject shieldEffect)
    {
        teamCharacter.shield += cardEffectValue;
        juggernautShield.PlayAudio("juggernautShield");
        Instantiate(shieldEffect, SelectionManager.SelectedPlayer.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);

        Transform temp = myBigCard.transform.GetChild(0);
        Destroy(temp.gameObject);
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();
        Utils.cardInUse = null;
        Utils.cardUseAbility = null;
    }
}
