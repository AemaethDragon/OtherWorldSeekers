using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonJump : MonoBehaviour
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
    public IEnumerator Execute(TeamCharacter teamCharacter, GameObject myBigCard, int cardRange, FieldManager fieldManager, AudioManager soldierHarpoonJump, GameObject HarpoonParticle)
    {
        List<Vector2> tempRange = Utils.CreateRangeList(fieldManager.graph, teamCharacter.hexID, cardRange, ListType.ATTACK);
        Hexagon hexagon = null;
        SelectionManager.ClearSelectedHexagon();

        fieldManager.hexagonControl.WalkableAreaVisible(teamCharacter, cardRange, ListType.ATTACK);

        yield return new WaitUntil(() => SelectionManager.SelectedHexagon!= hexagon);

        hexagon = SelectionManager.SelectedHexagon;
        if (tempRange.Contains(hexagon.matrixPos))
        {
            soldierHarpoonJump.PlayAudio("soldierHarpoonJump");
            GameObject harponTem = Instantiate(HarpoonParticle, SelectionManager.SelectedPlayer.transform.position, Quaternion.identity);
            Vector3 tempVec = Utils.ConverHexPosToOthers(hexagon);
            yield return new WaitUntil(() => MovementManager.AbilityMovement(teamCharacter.transform.gameObject, 10, teamCharacter.transform.position, tempVec) == tempVec);
            teamCharacter.hexID = hexagon.matrixPos;
            soldierHarpoonJump.PlayAudio("soldierHarpoonJump");
            Destroy(harponTem, 1f);
            StartHex(fieldManager);
        }
        fieldManager.hexagonControl.ClearAllVisibility();

        yield return new WaitForSeconds(2);

        Transform temp = myBigCard.transform.GetChild(0);
        Destroy(temp.gameObject);
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();
        Utils.cardUseAbility = null;
        Utils.cardInUse = null;
        StartHex(fieldManager);
    }
}
