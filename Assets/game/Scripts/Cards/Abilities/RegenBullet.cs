using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegenBullet : MonoBehaviour
{
    public IEnumerator Execute(TeamCharacter teamCharacter, GameObject myBigCard, int cardRange, int damage, FieldManager fieldManager, AudioManager soldierRegenBullet, GameObject healingEffect, GameObject bulletEffect)
    {
        List<Vector2> tempRange = Utils.CreateRangeList(fieldManager.graph, teamCharacter.hexID, cardRange, ListType.ATTACK);
        Enemy enemy = null;
        SelectionManager.ClearSelectedEnemy();

        fieldManager.hexagonControl.WalkableAreaVisible(teamCharacter, cardRange, ListType.ATTACK);

        yield return new WaitUntil(() => SelectionManager.SelectedEnemy != enemy);
        enemy = SelectionManager.SelectedEnemy;

        if (tempRange.Contains(enemy.iTargetable.hexID))
        {
            GameObject bulletEffectClone = Instantiate(bulletEffect, SelectionManager.SelectedPlayer.transform.position, Quaternion.identity);
            StartCoroutine(bulletEffectClone.GetComponent<Particle>().MoveParticle(SelectionManager.SelectedEnemy.transform.position, 50f));
            soldierRegenBullet.PlayAudio("soldierRegenBullet");
            yield return new WaitForSeconds(0.5f);
            Instantiate(healingEffect, SelectionManager.SelectedPlayer.transform.position, Quaternion.identity);
            enemy.iTargetable.TakeDamage(damage);
            teamCharacter.Heal(damage / 2);
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
