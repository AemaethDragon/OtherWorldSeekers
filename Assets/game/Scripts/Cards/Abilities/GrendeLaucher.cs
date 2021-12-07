using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrendeLaucher : MonoBehaviour
{
    public IEnumerator Execute(TeamCharacter teamCharacter, int cardRange, int aoeRange, int damage, List<Enemy> enemies, GameObject myBigCard, FieldManager fieldManager,AudioManager juggernautGrenadeLauncher, GameObject grenadeLauncherEffect)
    {
        List<Vector2> tempRange = Utils.CreateRangeList(fieldManager.graph, teamCharacter.hexID, cardRange, ListType.ATTACK);
        Hexagon hexagon = null;
        SelectionManager.ClearSelectedHexagon();

        fieldManager.hexagonControl.WalkableAreaVisible(teamCharacter, cardRange, ListType.ATTACK);

        yield return new WaitUntil(() => SelectionManager.SelectedHexagon != hexagon);


        hexagon = SelectionManager.SelectedHexagon;
        if (tempRange.Contains(hexagon.matrixPos))
        {
            List<Vector2> tempRange2 = Utils.CreateRangeList(fieldManager.graph, hexagon.matrixPos, aoeRange, ListType.ATTACK);
            juggernautGrenadeLauncher.PlayAudio("juggernautGrenadeLauncher");
            Instantiate(grenadeLauncherEffect, SelectionManager.SelectedHexagon.transform.position, Quaternion.identity);
            foreach (var enemy in enemies)
            {
                if (tempRange2.Contains(enemy.iTargetable.hexID))
                {
                    if (enemy.iTargetable.hexID == hexagon.matrixPos)
                    {
                        enemy.iTargetable.TakeDamage(damage);
                    }
                    else
                    {
                        enemy.iTargetable.TakeDamage(damage - 50);
                    }
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
