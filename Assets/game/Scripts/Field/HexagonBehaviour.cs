using System;
using UnityEngine;

public class HexagonBehaviour : MonoBehaviour
{
    public Hexagon hexagon;
    public DrawMovementLine drawMovement;

    public void MouseEnter()
    {
        if (!hexagon.IsFree() || !hexagon.isVisible) return;
        if (SelectionManager.SelectedPlayer != null)
        {
            drawMovement.CreatPath(hexagon);
        }
    }
    public void MouseOver()
    {
        if (!hexagon.IsFree() || !hexagon.isVisible) return;
        if (SelectionManager.SelectedPlayer != null)
        {
            drawMovement.CreatPath(hexagon);
        }
        else 
        {
            drawMovement.ClearDraw();
        }
    }
    public void MouseExit()
    {
        if (!hexagon.IsFree() || !hexagon.isVisible) return;
    }

    public void MouseClick()
    {
        if (!hexagon.IsFree() || !hexagon.isVisible) return;
        if (drawMovement.draw) drawMovement.SelectPath();
        SelectionManager.SelectHexagon(hexagon);
    }
}
