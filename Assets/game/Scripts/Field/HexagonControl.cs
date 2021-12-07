using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonControl : MonoBehaviour
{
    private FieldManager _fieldManager;

    private void Awake()
    {
        _fieldManager = GetComponent<FieldManager>();
    }
    public void WalkableAreaVisible(TeamCharacter teamCharacter, int area, ListType type)
    {
        if (teamCharacter == null) return;
        _fieldManager.drawMovementLine.draw = type == ListType.MOVE;
        
        List<Vector2> temp = Utils.CreateRangeList(_fieldManager.graph, teamCharacter.hexID, area, type);

        foreach (var hex in _fieldManager.hexagons)
        {
            if (temp.Contains(hex.Key))
            {
                hex.Value.IsVisible(true);
            }
        }
    }

    public void ClearAllVisibility()
    {
        foreach (var hex in _fieldManager.hexagons)
        {
            hex.Value.IsVisible(false);
        }
    }

    public void CheckObstacles()
    {
        foreach (var hex in _fieldManager.hexagons)
        {
            if (!hex.Value.IsFree())
            {
                if (_fieldManager.occupiedHexagons.Count == 0 || !_fieldManager.occupiedHexagons.Contains(hex.Key))
                {
                    _fieldManager.occupiedHexagons.Add(hex.Key);
                }
            }
            else
            {
                if (_fieldManager.occupiedHexagons.Contains(hex.Key))
                {
                    _fieldManager.occupiedHexagons.Remove(hex.Key);
                }
            }
        }
        _fieldManager.graph.UpdateConnection(_fieldManager.occupiedHexagons);
    }
}
