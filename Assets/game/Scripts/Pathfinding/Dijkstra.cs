using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dijkstra : MonoBehaviour
{
    public class NodeRecord
    {
        public Vector2 node;
        public Connection connection;
        public float costSoFar;
    }

    public class NodeRecordList : List<NodeRecord>
    {
        public bool MyContains(Vector2 node)
        {
            return Find(node) != null;
        }

        public NodeRecord Cheapest()
        {
            return this.OrderBy(record => record.costSoFar).First();
        }

        public NodeRecord Find(Vector2 node)
        {
            NodeRecord rec = null;

            try
            {
                rec = Find(record => record.node == node);
            }
            catch (ArgumentNullException)
            {
                // Just catch it
            }
            return rec;
        }
    }



    public static List<Vector2> Pathfind(Graph g, Vector2 from, int range)
    {

        NodeRecord current = null;
        NodeRecord startRec = new NodeRecord()
        {
            costSoFar = 0f,
            node = from,
            connection = null,
        };

        NodeRecordList closed = new NodeRecordList();
        NodeRecordList open = new NodeRecordList();

        open.Add(startRec);

        while (open.Count > 0)
        {
            current = open.Cheapest();
            if (current.costSoFar <= range)
            {
                var aux = g.GetConnections(current.node);

                if (aux != null)
                {
                    foreach (var connection in aux)
                    {
                        Vector2 endNode = connection.ToNode;
                        float endNodeCost = current.costSoFar + connection.Cost;

                        NodeRecord endNodeRecord = closed.Find(endNode);
                        if (endNodeRecord != null) continue;

                        endNodeRecord = open.Find(endNode);
                        if (endNodeRecord != null)
                        {
                            if (endNodeRecord.costSoFar <= endNodeCost) continue;
                        }
                        else
                        {
                            endNodeRecord = new NodeRecord();
                            endNodeRecord.node = endNode;
                            open.Add(endNodeRecord);
                        }
                        endNodeRecord.connection = connection;
                        endNodeRecord.costSoFar = endNodeCost;
                    }
                }
            }
            open.Remove(current);
            closed.Add(current);
        }
        //if (to == null)
        //{
            return closed.Select<NodeRecord,Vector2>(a => a.node).ToList();
        //}
        //if (current == null || current.node != to) return null;

        //List<Vector2> path = new List<Vector2>();
        //path.Add(to);

        //while (current.connection != null)
        //{
        //    Vector2 fromNode = current.connection.FromNode;
        //    path.Add(fromNode);
        //    current = closed.Find(fromNode);
        //}

        //path.Reverse();

        //return path;
    }
}
