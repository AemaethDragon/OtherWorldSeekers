using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirSupport : MonoBehaviour
{
    public IEnumerator Execute(TeamCharacter teamCharacter, GameObject myBigCard, int cardRange, int damage, FieldManager fieldManager, List<Enemy> enemies, GameObject missil, AudioManager soldierAirSupport, GameObject airSupportEffect)
    {
        GameObject gameObject;
        Enemy enemy = null;
        SelectionManager.ClearSelectedEnemy();

        yield return new WaitForSeconds(0.1f);

        CleanSelection(fieldManager);

        yield return new WaitUntil(() => SelectionManager.SelectedEnemy != enemy);

        SelectionManager.SelectPlayer(teamCharacter);
        SelectionManager.SelectedPlayer.selectionController.ShowSelected();

        enemy = SelectionManager.SelectedEnemy;

        
        List<Vector2> tempRange = Utils.CreateRangeList(fieldManager.graph, enemy.iTargetable.hexID, cardRange, ListType.ATTACK);
        Vector3 tempV = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 15f, enemy.transform.position.z);
        Vector3 tempV2 = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 1f, enemy.transform.position.z);
        gameObject = Instantiate(missil, tempV, Quaternion.identity);
        gameObject.GetComponent<Missil>().worldPos = tempV;
        gameObject.GetComponent<Missil>().enemy = enemy;
        
        soldierAirSupport.PlayAudio("soldierAirSupport");
        
        yield return new WaitForSeconds(2f);
        foreach (Enemy tempEnemy in enemies)
        {
            if (tempRange.Contains(tempEnemy.iTargetable.hexID))
            {
                Instantiate(airSupportEffect, tempV2, Quaternion.identity);
                tempEnemy.iTargetable.TakeDamage(damage);
            }
        }
        
        yield return new WaitForSeconds(2f);

        Transform temp = myBigCard.transform.GetChild(0);
        Destroy(temp.gameObject);
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();
        SelectionManager.ClearSelectedTargetable();
        Utils.cardUseAbility = null;
        Utils.cardInUse = null;
    }
    public void CleanSelection(FieldManager fieldManager)
    {
        if (SelectionManager.SelectedPlayer != null)
        {
            SelectionManager.SelectedPlayer.selectionController.HideSelected();
        }

        //Clear all the visible hexagons after walking
        fieldManager.hexagonControl.ClearAllVisibility();
        SelectionManager.ClearSelectedPlayer();
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();

        SelectionManager.EnableReturn();
    }
}
