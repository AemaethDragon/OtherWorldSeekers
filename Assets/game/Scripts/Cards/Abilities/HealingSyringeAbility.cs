using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingSyringeAbility : MonoBehaviour
{
    public IEnumerator Execute(TeamCharacter teamCharacter, GameObject myBigCard, int cardRange, int cardEffectValue, FieldManager fieldManager, GameObject healingSering, GameObject healingEffect)
    {
        GameObject temp;
        List<Vector2> tempRange = Utils.CreateRangeList(fieldManager.graph, teamCharacter.hexID, cardRange, ListType.ATTACK);
        Hexagon hexagon = null;
        SelectionManager.ClearSelectedHexagon();

        fieldManager.hexagonControl.WalkableAreaVisible(teamCharacter, cardRange, ListType.ATTACK);
        yield return new WaitUntil(() => SelectionManager.SelectedHexagon != hexagon);
        hexagon = SelectionManager.SelectedHexagon;
        if (tempRange.Contains(hexagon.matrixPos))
        {
            Vector3 tempV = new Vector3(hexagon.worldPos.x, hexagon.worldPos.y + 0.63f, hexagon.worldPos.z);
            temp = Instantiate(healingSering, tempV, Quaternion.identity);
            temp.GetComponent<HealingSyringe>().healingAmount = cardEffectValue;
            temp.GetComponent<HealingSyringe>().healingEffect = healingEffect;

        }
        fieldManager.hexagonControl.ClearAllVisibility();
        
        yield return new WaitForSeconds(2);

        Transform temp2 = myBigCard.transform.GetChild(0);
        Destroy(temp2.gameObject);
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();
        Utils.cardUseAbility = null;
        Utils.cardInUse = null;
    }
}
