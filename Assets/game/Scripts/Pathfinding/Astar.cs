using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Astar : MonoBehaviour
{
    public class NodeRecordStar
    {
        public Vector2 node;
        public Connection connection;
        public float costSoFar;
        public float estimatedTotalCost;
    }

    public class Heuristic
    {
        private Vector2 source;
        public Heuristic(Vector2 sourceNode)
        {
            source = sourceNode;
        }

        public float Estimate(Vector2 targetNode)
        {
            return Vector2.Distance(source, targetNode);
        }
    }

    public class NodeRecordList : List<NodeRecordStar>
    {
        public bool MyContains(Vector2 node)
        {
            return Find(node) != null;
        }

        public NodeRecordStar Cheapest()
        {
            return this.OrderBy(record => record.estimatedTotalCost).First();
        }

        public NodeRecordStar Find(Vector2 node)
        {
            NodeRecordStar rec = null;

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

    public static List<Vector2> Pathfind(Graph g, Vector2 from, Vector2 to)
    {
        Heuristic heuristic = new Heuristic(to);

        NodeRecordStar current = null;
        NodeRecordStar startRec = new NodeRecordStar()
        {
            costSoFar = 0f,
            node = from,
            connection = null,
            estimatedTotalCost = heuristic.Estimate(from)
        };

        NodeRecordList closed = new NodeRecordList();
        NodeRecordList open = new NodeRecordList();

        open.Add(startRec);

        while (open.Count > 0)
        {
            current = open.Cheapest();

            if (current.node == to) break;

            var aux = g.GetConnections(current.node);

            if (aux != null)
            {
                foreach (var connection in aux)
                {
                    Vector2 endNode = connection.ToNode;
                    float endNodeCost = current.costSoFar + connection.Cost;
                    float endNodeHeuristic = 0f;

                    NodeRecordStar endNodeRecord = closed.Find(endNode);
                    if (endNodeRecord != null)
                    {
                        if (endNodeRecord.costSoFar <= endNodeCost) continue;
                        closed.Remove(endNodeRecord);
                        open.Add(endNodeRecord);
                        endNodeHeuristic = endNodeRecord.estimatedTotalCost - endNodeRecord.costSoFar;
                    }
                    else
                    {
                        endNodeRecord = open.Find(endNode);
                        if (endNodeRecord != null)
                        {
                            if (endNodeRecord.costSoFar <= endNodeCost) continue;
                            endNodeHeuristic = endNodeRecord.estimatedTotalCost - endNodeRecord.costSoFar;
                        }
                        else
                        {
                            endNodeRecord = new NodeRecordStar();
                            endNodeRecord.node = endNode;
                            endNodeHeuristic = heuristic.Estimate(endNode);
                            open.Add(endNodeRecord);
                        }
                    }
                    endNodeRecord.connection = connection;
                    endNodeRecord.costSoFar = endNodeCost;
                    endNodeRecord.estimatedTotalCost = endNodeCost + endNodeHeuristic;
                }
            }

            open.Remove(current);
            closed.Add(current);
        }
        if (current == null || current.node != to) return null;

        List<Vector2> path = new List<Vector2>();
        path.Add(to);

        while (current.connection != null)
        {
            Vector2 fromNode = current.connection.FromNode;
            path.Add(fromNode);
            current = closed.Find(fromNode);
        }

        path.Reverse();

        return path;
    }
}
