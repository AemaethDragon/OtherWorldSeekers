using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstFire : MonoBehaviour
{
    public IEnumerator Execute(TeamCharacter teamCharacter, GameObject myBigCard, int cardRange, int damage, int numberOfTimes, FieldManager fieldManager, AudioManager soldierBurstFire, GameObject burstFireEffect)
    {
        List<Vector2> tempRange = Utils.CreateRangeList(fieldManager.graph, teamCharacter.hexID, cardRange, ListType.ATTACK);
        Enemy enemy = null;
        SelectionManager.ClearSelectedEnemy();

        fieldManager.hexagonControl.WalkableAreaVisible(teamCharacter, cardRange, ListType.ATTACK);
        yield return new WaitUntil(() => SelectionManager.SelectedEnemy != enemy);
        enemy = SelectionManager.SelectedEnemy;
        

        if (tempRange.Contains(enemy.iTargetable.hexID))
        {
            soldierBurstFire.PlayAudio("soldierBurstFire");
            enemy = SelectionManager.SelectedEnemy;
            for (int i = 1; i <= numberOfTimes; i++)
            {
                GameObject burstFireEffectClone = Instantiate(burstFireEffect, SelectionManager.SelectedPlayer.transform.position, Quaternion.identity);
                StartCoroutine(burstFireEffectClone.GetComponent<Particle>().MoveParticle(SelectionManager.SelectedEnemy.transform.position, 50f));
                enemy.iTargetable.TakeDamage(damage * i);
                yield return new WaitForSeconds(0.1f);
            }
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
