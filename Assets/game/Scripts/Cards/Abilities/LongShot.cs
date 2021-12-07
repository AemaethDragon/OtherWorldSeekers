using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongShot : MonoBehaviour
{
    public IEnumerator Execute( GameObject myBigCard, int damage,FieldManager fieldManager,TeamCharacter teamCharacter, AudioManager sniperLongShot, GameObject longShotEffect)
    {
        Enemy enemy = null;
        SelectionManager.ClearSelectedEnemy();

        yield return new WaitForSeconds(0.1f);
        CleanSelection(fieldManager);

        yield return new WaitUntil(() => SelectionManager.SelectedEnemy != enemy);
        enemy = SelectionManager.SelectedEnemy;

        SelectionManager.SelectPlayer(teamCharacter);
        SelectionManager.SelectedPlayer.selectionController.ShowSelected();

        enemy.iTargetable.TakeDamage(damage);
        GameObject longShotEffectClone = Instantiate(longShotEffect, SelectionManager.SelectedPlayer.transform.position, Quaternion.identity);
        StartCoroutine(longShotEffectClone.GetComponent<Particle>().MoveParticle(SelectionManager.SelectedEnemy.transform.position, 25f));
        sniperLongShot.PlayAudio("sniperLongShot");

        yield return new WaitForSeconds(2);
        Transform temp = myBigCard.transform.GetChild(0);
        Destroy(temp.gameObject);
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();
        Utils.cardUseAbility = null;
        Utils.cardInUse = null;
    }
    public void CleanSelection(FieldManager fieldManager)
    {
        if (SelectionManager.SelectedPlayer != null)
        {
            SelectionManager.SelectedPlayer.selectionController.HideSelected();
        }

        //Clear all the visible hexagons after walking
        fieldManager.hexagonControl.ClearAllVisibility();
        SelectionManager.ClearSelectedPlayer();
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();

        SelectionManager.EnableReturn();
    }
}
