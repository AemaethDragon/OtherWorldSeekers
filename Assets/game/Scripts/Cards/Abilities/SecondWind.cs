using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondWind : MonoBehaviour
{
    public IEnumerator Execute(TeamCharacter teamCharacter, GameObject myBigCard, AudioManager sniperSecondWind, GameObject secondWindEffect)
    {
        teamCharacter.AddSpeed(9999);
        sniperSecondWind.PlayAudio("sniperSecondWind");
        Instantiate(secondWindEffect, SelectionManager.SelectedPlayer.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);

        Transform temp = myBigCard.transform.GetChild(0);
        Destroy(temp.gameObject);
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();
        Utils.cardInUse = null;
        Utils.cardUseAbility = null;
    }
}
