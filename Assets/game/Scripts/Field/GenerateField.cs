using System;
using System.Collections.Generic;
using UnityEngine;

public class GenerateField : MonoBehaviour
{
    #region Variables
    //Public
    public GameObject hexagon;
    public GameObject parent;
    
    //Private
    private FieldManager _fieldManager;
    private float xOffset = 1.503f;
    private float yOffset = 1.735f;

    #endregion

    #region Methods

    private void Awake()
    {
        _fieldManager = GetComponent<FieldManager>();
    }

    //Public

    //Creates a vector2 to serve as Matrix with the positions and instantiates an hexagon for each one
    public void GenerateMatrix(Dictionary<Vector2, Hexagon> hexagons, int mapWidth, int mapHeight)
    {
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                hexagons.Add(new Vector2(i, j), InstantiateHexagon());
            }
        }
    }
    
    //Gives the correct position to the instances of the dictionary of hexagons
    public void GenerateFieldM(Dictionary<Vector2, Hexagon> hexagons)
    {
        foreach (var hex in hexagons)
        {
            //Copy the matrix position to the class for ease of use in other operations
            hex.Value.matrixPos = hex.Key;
            var tempTransform = hex.Value.transform;
            
            //Offset on Y axis so that the hexagons can fit correctly
            if (hex.Key.x % 2 != 0)
            {
                hex.Value.worldPos = new Vector3(hex.Key.x * xOffset, 0, hex.Key.y * yOffset + yOffset / 2);
                tempTransform.position = hex.Value.worldPos;
                tempTransform.name = $"X: {hex.Key.x} , Z: {hex.Key.y}";
            }
            else
            {
                hex.Value.worldPos = new Vector3(hex.Key.x * xOffset, 0, hex.Key.y * yOffset);
                tempTransform.position = hex.Value.worldPos;
                tempTransform.name = $"X: {hex.Key.x} , Z: {hex.Key.y}";
            }
        }
    }
    
    //Generate a graph with all the positions
    public void GenerateGraph(ref Graph g, ref Dictionary<Vector2, Hexagon> hexagons)
    {
        foreach (var node in hexagons)
        {
            g.AddConnection(node.Key, new Vector2(node.Key.x, node.Key.y - 1), 1f);
            g.AddConnection(node.Key, new Vector2(node.Key.x, node.Key.y + 1), 1f);

            if (node.Key.x % 2 == 0)
            {
                g.AddConnection(node.Key, new Vector2(node.Key.x - 1, node.Key.y), 1f);
                g.AddConnection(node.Key, new Vector2(node.Key.x - 1, node.Key.y - 1), 1f);
                g.AddConnection(node.Key, new Vector2(node.Key.x + 1, node.Key.y), 1f);
                g.AddConnection(node.Key, new Vector2(node.Key.x + 1, node.Key.y - 1), 1f);
            }
            else
            {
                g.AddConnection(node.Key, new Vector2(node.Key.x - 1, node.Key.y), 1f);
                g.AddConnection(node.Key, new Vector2(node.Key.x - 1, node.Key.y + 1), 1f);
                g.AddConnection(node.Key, new Vector2(node.Key.x + 1, node.Key.y), 1f);
                g.AddConnection(node.Key, new Vector2(node.Key.x + 1, node.Key.y + 1), 1f);
            }
        }
    }

    //Private

    //This method instantiates an hexagon and gives it an Hexagon class. Returns the Hexagon class
    private Hexagon InstantiateHexagon()
    {
        GameObject go = Instantiate(hexagon, parent.transform, true);
        Hexagon h = go.AddComponent<Hexagon>();
        h.fieldManager = _fieldManager;
        //h.IsVisible(false);
        return h;
    }
    #endregion
}
