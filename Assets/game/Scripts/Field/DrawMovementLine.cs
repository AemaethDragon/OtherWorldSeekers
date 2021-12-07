using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMovementLine : MonoBehaviour
{
    public FieldManager _fieldManager;
    public List<Vector2> selectedPath;
    public bool draw;
    
    private List<Vector2> path;


    private void Awake()
    {
        _fieldManager = GetComponent<FieldManager>();
        draw = true;
    }

    public void CreatPath(Hexagon currentHex)
    {
        if (!draw) return;
        if (!currentHex.isVisible || SelectionManager.SelectedHexagon != null) return;
        path = new List<Vector2>();

        path = Astar.Pathfind(_fieldManager.graph, SelectionManager.SelectedPlayer.hexID, currentHex.matrixPos);

        DrawPathLineLogic(path);
    }

    public void SelectPath()
    {
        selectedPath = path;
        ClearDraw();
        DrawPathLineLogic(selectedPath);
        path = null;
    }
    
    private void DrawPathLineLogic(List<Vector2> newPath)
    {
        if (path.Count > 0)
        {
            Vector3[] gList = new Vector3[newPath.Count];
            for (int i = 0; i < newPath.Count; i++)
            {
                gList[i] = _fieldManager.hexagons[newPath[i]].worldPos;
                gList[i].y = 0.2f;
            }
            DrawPathLine.DrawLine(_fieldManager.lineRenderer, gList);
        }
    }
    
    public void ClearDraw()
    {
        _fieldManager.lineRenderer.positionCount = 0;
    }
}
