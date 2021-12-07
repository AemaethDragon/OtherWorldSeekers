using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastShot : MonoBehaviour
{
    public IEnumerator Execute(TeamCharacter teamCharacter,GameObject myBigCard, int cardRange, int cardeffectvalue,FieldManager fieldManager, AudioManager sniperLastShot, GameObject lastShotEffect)
    {
        List<Vector2> tempRange = Utils.CreateRangeList(fieldManager.graph, teamCharacter.hexID, cardRange, ListType.ATTACK);
        Enemy enemy = null;
        SelectionManager.ClearSelectedEnemy();

        fieldManager.hexagonControl.WalkableAreaVisible(teamCharacter, cardRange, ListType.ATTACK);
        yield return new WaitUntil(() => SelectionManager.SelectedEnemy != enemy);
        enemy = SelectionManager.SelectedEnemy;
        
        if (tempRange.Contains(enemy.iTargetable.hexID))
        {
            sniperLastShot.PlayAudio("sniperLastShot");
            GameObject lastShotEffectClone = Instantiate(lastShotEffect, SelectionManager.SelectedPlayer.transform.position, Quaternion.identity);
            StartCoroutine(lastShotEffectClone.GetComponent<Particle>().MoveParticle(SelectionManager.SelectedEnemy.transform.position, 50f));
            int tempdamge = teamCharacter.energy * cardeffectvalue;
            enemy.iTargetable.TakeDamage(tempdamge);
            teamCharacter.energy = 0;
        }
        fieldManager.hexagonControl.ClearAllVisibility();

        yield return new WaitForSeconds(2);

        Transform temp = myBigCard.transform.GetChild(0);
        Destroy(temp.gameObject);
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();
        Utils.cardUseAbility = null;
        Utils.cardInUse = null;
    }
}
