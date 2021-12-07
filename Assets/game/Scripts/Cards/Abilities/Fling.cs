using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fling : MonoBehaviour
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
    public IEnumerator Execute(TeamCharacter teamCharacter, int cardRange, int trowRange, GameObject myBigCard, FieldManager fieldManager, AudioManager juggernautFling)
    {
        List<Vector2> tempRange = Utils.CreateRangeList(fieldManager.graph, teamCharacter.hexID, cardRange, ListType.ATTACK);
        Enemy enemy = null;
        SelectionManager.ClearSelectedEnemy();

        fieldManager.hexagonControl.WalkableAreaVisible(teamCharacter, cardRange, ListType.ATTACK);

        yield return new WaitUntil(() => SelectionManager.SelectedEnemy != enemy);

        enemy = SelectionManager.SelectedEnemy;
        if (tempRange.Contains(enemy.iTargetable.hexID))
        {
            tempRange = Utils.CreateRangeList(fieldManager.graph, teamCharacter.hexID, trowRange, ListType.ATTACK);
        }
        
        fieldManager.hexagonControl.WalkableAreaVisible(teamCharacter, trowRange, ListType.ATTACK);
        

        Hexagon hexagon = null;
        SelectionManager.ClearSelectedHexagon();
        yield return new WaitUntil(() => SelectionManager.SelectedHexagon != hexagon);

        hexagon = SelectionManager.SelectedHexagon;
        if (tempRange.Contains(hexagon.matrixPos))
        {
            juggernautFling.PlayAudio("juggernautFling");
            Vector3 tempVec = Utils.ConverHexPosToOthers(hexagon);
            yield return new WaitUntil(() => MovementManager.AbilityMovement(enemy.transform.gameObject, 10, enemy.transform.position, tempVec) == tempVec);
            juggernautFling.PlayAudio("juggernautFling");
            enemy.iTargetable.hexID = hexagon.matrixPos;
            
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
        StartHex(fieldManager);
    }
}
