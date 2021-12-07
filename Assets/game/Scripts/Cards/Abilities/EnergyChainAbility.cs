using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyChainAbility : MonoBehaviour
{
    private TeamCharacter _teamCharacter;
    private FieldManager _fieldManager;
    private GameObject _myBigCard;
    private GameObject _energyChainEffect;
    private List<Enemy> _enemies;
    private Enemy _enemy;
    private int _cardEffectValue;
    private int _cardRange;
    private int _posDamage;

    public void MyStart(TeamCharacter teamCharacter, FieldManager fieldManager, GameObject myBigCard,
        List<Enemy> enemies, Enemy enemy, int cardEffectValue, int cardRange, int posDamage,
        GameObject energyChainEffect)
    {
        _teamCharacter = teamCharacter;
        _fieldManager = fieldManager;
        _myBigCard = myBigCard;
        _enemies = enemies;
        _enemy = enemy;
        _cardEffectValue = cardEffectValue;
        _cardRange = cardRange;
        _posDamage = posDamage;
        _energyChainEffect = energyChainEffect;
        _enemy.eChain = true;
    }

    public void StartAbility()
    {
        StartCoroutine(Execute());
    }

    public IEnumerator Execute()
    {
        List<Vector2> spreadChain = Utils.CreateRangeList(_fieldManager.graph, _enemy.iTargetable.hexID, _cardRange, ListType.ATTACK);

        yield return new WaitForSeconds(0.5f);

        _enemy.iTargetable.TakeDamage(_posDamage);
        GameObject _energyChainEffectClone = Instantiate(_energyChainEffect, _enemy.transform.position, Quaternion.identity);

        yield return new WaitForSeconds(2f);
        Destroy(_energyChainEffectClone, 1f);
        foreach (Enemy tempEnemy in _enemies)
        {
            if (tempEnemy.eChain) continue;
            if (!spreadChain.Contains(tempEnemy.iTargetable.hexID)) continue;
            _enemy.gameManager.audioManager.PlayAudio("kineticmageEnergyChain");
            EnergyChainAbility tempEchain = tempEnemy.gameObject.AddComponent<EnergyChainAbility>();
            tempEchain.MyStart(_teamCharacter, _fieldManager, _myBigCard, _enemies, tempEnemy, _cardEffectValue, _cardRange, _posDamage, _energyChainEffect);
            tempEchain.StartAbility();
        }
        Destroy(this);
    }
}