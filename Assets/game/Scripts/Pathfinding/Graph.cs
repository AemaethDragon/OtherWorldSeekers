using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph
{
    private Dictionary<Vector2, List<Connection>> connections;

    public Graph()
    {
        connections = new Dictionary<Vector2, List<Connection>>();
    }

    public List<Connection> GetConnections(Vector2 node)
    {
        List<Connection> temp = new List<Connection>();

        foreach (var connection in connections)
        {
            if (connection.Key == node)
            {
                foreach (var connect in connection.Value)
                {
                    if (connect.Free)
                    {
                        temp.Add(connect);
                    }
                }
                return temp;
            }
        }
        return null;
    }

    public void AddConnection(Vector2 from, Vector2 to, float cost)
    {
        if (!connections.ContainsKey(from))
        {
            connections[from] = new List<Connection>();
        }
        connections[from].Add(new Connection(from, to, cost));
    }

    public void UpdateConnection(List<Vector2> occupiedHexagons)
    {
        foreach (var connection in connections)
        {
            foreach (var connect in connection.Value)
            {
                if (occupiedHexagons.Contains(connect.ToNode)) connect.SetFree(false);
                else connect.SetFree(true);
            }
        }
    }
}
