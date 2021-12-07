using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyChain : MonoBehaviour
{
    public IEnumerator Execute(TeamCharacter teamCharacter, GameObject myBigCard, int cardRange, int cardEffectValue, int posDamage, FieldManager fieldManager, List<Enemy> enemies, AudioManager kineticmageEnergyChain, GameObject energyChainEffect, GameObject energyChainEffect2)
    {
        List<Vector2> tempRange = Utils.CreateRangeList(fieldManager.graph, teamCharacter.hexID, cardRange, ListType.ATTACK);
        Enemy enemy = null;
        SelectionManager.ClearSelectedEnemy();
        fieldManager.hexagonControl.WalkableAreaVisible(teamCharacter, cardRange, ListType.ATTACK);
        yield return new WaitUntil(() => SelectionManager.SelectedEnemy != enemy);
        enemy = SelectionManager.SelectedEnemy;

        if (tempRange.Contains(enemy.iTargetable.hexID))
        {
            Vector3 enemyTransf = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 10f, enemy.transform.position.z);
            GameObject energyChainEffectClone = Instantiate(energyChainEffect, enemyTransf, Quaternion.identity);
            List<Vector2> spreadChain = Utils.CreateRangeList(fieldManager.graph, enemy.iTargetable.hexID, cardRange, ListType.ATTACK);
            enemy.iTargetable.TakeDamage(cardEffectValue - posDamage);
            Debug.Log("EnergyChain 2 added");

            foreach (Enemy tempEnemy in enemies)
            {
                if (tempEnemy.eChain) continue;
                if (!spreadChain.Contains(tempEnemy.iTargetable.hexID)) continue;
                kineticmageEnergyChain.PlayAudio("kineticmageEnergyChain");
                EnergyChainAbility tempEchain = enemy.gameObject.AddComponent<EnergyChainAbility>();
                tempEchain.MyStart(teamCharacter, fieldManager, myBigCard, enemies, tempEnemy, cardEffectValue, cardRange, posDamage, energyChainEffect2);
                tempEchain.StartAbility();
            }

            fieldManager.hexagonControl.ClearAllVisibility();
            Destroy(energyChainEffectClone, 1f);
        }
        yield return new WaitForSeconds(2);
        Transform temp = myBigCard.transform.GetChild(0);
        Destroy(temp.gameObject);
        SelectionManager.ClearSelectedTargetable();
        SelectionManager.ClearSelectedHexagon();
        SelectionManager.ClearSelectedEnemy();
        Utils.cardUseAbility = null;
        Utils.cardInUse = null;
    }
}