using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShot : MonoBehaviour
{
    public IEnumerator Execute(TeamCharacter teamCharacter, GameObject myBigCard, int cardRange, FieldManager fieldManager, AudioManager sniperHeadShot, GameObject headshotEffect)
    {
        List<Vector2> tempRange = Utils.CreateRangeList(fieldManager.graph, teamCharacter.hexID, cardRange, ListType.ATTACK);
        Enemy enemy = null;
        SelectionManager.ClearSelectedEnemy();

        fieldManager.hexagonControl.WalkableAreaVisible(teamCharacter, cardRange, ListType.ATTACK);

        yield return new WaitUntil(() => SelectionManager.SelectedEnemy != enemy);
        enemy = SelectionManager.SelectedEnemy;
        
        
        
        if (tempRange.Contains(enemy.iTargetable.hexID))
        {
            GameObject headshotEffectClone = Instantiate(headshotEffect, SelectionManager.SelectedPlayer.transform.position, Quaternion.identity);
            StartCoroutine(headshotEffectClone.GetComponent<Particle>().MoveParticle(SelectionManager.SelectedEnemy.transform.position, 50f));
            sniperHeadShot.PlayAudio("sniperHeadShot");
            yield return new WaitForSeconds(0.2f);
            enemy.iTargetable.TakeDamage(9001);
        }
        fieldManager.hexagonControl.ClearAllVisibility();

        yield return new WaitForSeconds(2);

        Transform temp = myBigCard.transform.GetChild(0);
        Destroy(temp.gameObject);
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();
        SelectionManager.ClearSelectedTargetable();
        Utils.cardUseAbility = null;
        Utils.cardInUse = null;
    }
}
