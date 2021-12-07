using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinck : MonoBehaviour
{
    public IEnumerator Execute(TeamCharacter teamCharacter, GameObject myBigCard, int cardRange, FieldManager fieldManager, AudioManager kineticmageBlink, GameObject blinkEffect)
    {
        List<Vector2> tempRange = Utils.CreateRangeList(fieldManager.graph, teamCharacter.hexID, cardRange, ListType.ATTACK);
        Hexagon hexagon = null;
        SelectionManager.ClearSelectedHexagon();

        fieldManager.hexagonControl.WalkableAreaVisible(teamCharacter, cardRange, ListType.ATTACK);

        yield return new WaitUntil(() => SelectionManager.SelectedHexagon != hexagon);

        hexagon = SelectionManager.SelectedHexagon;
        if (tempRange.Contains(hexagon.matrixPos))
        {
            Instantiate(blinkEffect, SelectionManager.SelectedPlayer.transform.position, Quaternion.identity);
            Vector3 newPos = new Vector3(hexagon.worldPos.x, hexagon.worldPos.y + 1, hexagon.worldPos.z);
            Instantiate(blinkEffect, newPos, Quaternion.identity);
            kineticmageBlink.PlayAudio("kineticmageBlink");

            teamCharacter.transform.position = newPos;
            teamCharacter.hexID = hexagon.matrixPos;
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
