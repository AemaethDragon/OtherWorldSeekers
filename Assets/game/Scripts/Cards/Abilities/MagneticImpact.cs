using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticImpact : MonoBehaviour
{
    public IEnumerator Execute(TeamCharacter teamCharacter, GameObject myBigCard, int effectValue, int cardRange, int aoeRange, List<Enemy> enemies, TeamManager team, FieldManager fieldManager, AudioManager kineticmageMagneticImpact, GameObject mageAttackEffect, GameObject magneticImpactEffect)
    {
        List<Vector2> tempRange = Utils.CreateRangeList(fieldManager.graph, teamCharacter.hexID, cardRange, ListType.ATTACK);
        Hexagon hexagon = null;
        SelectionManager.ClearSelectedHexagon();

        fieldManager.hexagonControl.WalkableAreaVisible(teamCharacter, cardRange, ListType.ATTACK);

        yield return new WaitUntil(() => SelectionManager.SelectedHexagon != hexagon);

        hexagon = SelectionManager.SelectedHexagon;
        GameObject mageAttackEffectClone = Instantiate(mageAttackEffect, SelectionManager.SelectedPlayer.transform.position, Quaternion.identity);
        StartCoroutine(mageAttackEffectClone.GetComponent<Particle>().MoveParticle(hexagon.worldPos, 20f));
        yield return new WaitForSeconds(0.5f);
        if (tempRange.Contains(hexagon.matrixPos))
        {
            List<Vector2> tempRange2 = Utils.CreateRangeList(fieldManager.graph, hexagon.matrixPos, aoeRange, ListType.ATTACK);
            kineticmageMagneticImpact.PlayAudio("kineticmageMagneticImpact");
            Instantiate(magneticImpactEffect, SelectionManager.SelectedHexagon.transform.position, Quaternion.identity);
            if (enemies != null)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (tempRange2.Contains(enemy.iTargetable.hexID))
                    {
                        enemy.iTargetable.TakeDamage(effectValue);
                    }
                }
            }

            foreach (var player in team.eliteSquad)
            {
                if (tempRange2.Contains(player.hexID))
                {
                    player.Heal(effectValue);
                }
            }
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
