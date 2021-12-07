using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonJump : MonoBehaviour
{
    private void StartHex(FieldManager fieldManager)
    {
        StartCoroutine(StartHexCoroutine(fieldManager));
    }
    private IEnumerator StartHexCoroutine(FieldManager fieldManager)
    {
        fieldManager.hexagonControl.CheckObstacles();
        yield return new WaitForSeconds(0.1f);
    }
    public IEnumerator Execute(TeamCharacter teamCharacter, int cardRange, int damage, int aoeRange, GameObject myBigCard, List<Enemy> enemies, FieldManager fieldManager, AudioManager juggernautCannonJump, GameObject cannonJumpEffect)
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
            foreach (var enemy in enemies)
            {
                if (tempRange2.Contains(enemy.iTargetable.hexID))
                {
                    enemy.iTargetable.TakeDamage(damage);
                }
            }

            Vector3 tempVec = Utils.ConverHexPosToOthers(hexagon);
            SelectionManager.SelectedPlayer.animator.SetTrigger("CannonJumpTrigger");
            yield return new WaitForSeconds(0.5f);
            juggernautCannonJump.PlayAudio("juggernautCannonJump");
            yield return new WaitUntil(() => MovementManager.AbilityMovement(teamCharacter.transform.gameObject, 10, teamCharacter.transform.position, tempVec) == tempVec);
            teamCharacter.hexID = hexagon.matrixPos;
            juggernautCannonJump.PlayAudio("juggernautCannonJump");            
        }
        Instantiate(cannonJumpEffect, SelectionManager.SelectedPlayer.transform.position, Quaternion.identity);


        fieldManager.hexagonControl.ClearAllVisibility();
        yield return new WaitForSeconds(2);

        Transform temp = myBigCard.transform.GetChild(0);
        Destroy(temp.gameObject);
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();
        SelectionManager.ClearSelectedTargetable();
        Utils.cardUseAbility = null;
        Utils.cardInUse = null;
        StartHex(fieldManager);
    }
    
}
