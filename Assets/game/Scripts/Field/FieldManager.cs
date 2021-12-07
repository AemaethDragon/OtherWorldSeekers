using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    #region Variables
    //Public
    [HideInInspector] public HexagonControl hexagonControl;
    [HideInInspector] public GenerateField generateField;
    public Dictionary<Vector2, Hexagon> hexagons;
    public DrawMovementLine drawMovementLine;
    public List<Vector2> occupiedHexagons;
    public LineRenderer lineRenderer;
    public Graph graph;
    public int mapHeight;
    public int mapWidth;

    //Private


    #endregion
    
    #region Methods

    private void Awake()
    {
        generateField = GetComponent<GenerateField>();
        hexagonControl = GetComponent<HexagonControl>();
        lineRenderer = GetComponent<LineRenderer>();
        hexagons = new Dictionary<Vector2, Hexagon>();
        occupiedHexagons = new List<Vector2>();
        graph = new Graph();
    }

    void Start()
    {
        generateField.GenerateMatrix(hexagons, mapWidth, mapHeight);
        generateField.GenerateFieldM(hexagons);
        generateField.GenerateGraph(ref graph, ref hexagons); 
        hexagonControl.ClearAllVisibility();
    }
    
    #endregion
}
