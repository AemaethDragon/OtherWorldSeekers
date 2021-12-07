using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum ListType
{
    ATTACK,
    MOVE
}

public static class Utils
{
    public static Card cardInUse;
    public static Card cardUseAbility;
    public static List<Vector2> tempList;
    public static List<Vector2> auxList;


    /// <summary>
    /// This method returns a List of Vector2 whith al the positions around the target given a Range.
    /// </summary>
    /// <param name="position">Position from which the range will be crated.</param>
    /// <param name="range">Range of the character.</param>
    /// <returns></returns>
    public static List<Vector2> CreateRangeList(Graph g, Vector2 position, int range, ListType listType)
    {
        if (listType == ListType.MOVE)
        {
            tempList = Dijkstra.Pathfind(g, position, range);
        }
        else
        {
            tempList = new List<Vector2>(); //inicialize the list
            int tempRange = range;

            while (tempRange > 0)
            {
                if (tempRange == range) // if we are on the first range
                {
                    tempList.Add(new Vector2(position.x, position.y));
                    tempList.Add(new Vector2(position.x, position.y - 1));
                    tempList.Add(new Vector2(position.x, position.y + 1));

                    if (position.x % 2 == 0)
                    {
                        tempList.Add(new Vector2(position.x - 1, position.y));
                        tempList.Add(new Vector2(position.x - 1, position.y - 1));
                        tempList.Add(new Vector2(position.x + 1, position.y));
                        tempList.Add(new Vector2(position.x + 1, position.y - 1));
                    }
                    else
                    {
                        tempList.Add(new Vector2(position.x - 1, position.y));
                        tempList.Add(new Vector2(position.x - 1, position.y + 1));
                        tempList.Add(new Vector2(position.x + 1, position.y));
                        tempList.Add(new Vector2(position.x + 1, position.y + 1));
                    }
                }
                else
                {
                    foreach (Vector2 tempPos in tempList.ToArray())
                    {
                        if (!tempList.Contains(new Vector2(tempPos.x, tempPos.y - 1))) tempList.Add(new Vector2(tempPos.x, tempPos.y - 1));
                        if (!tempList.Contains(new Vector2(tempPos.x, tempPos.y + 1))) tempList.Add(new Vector2(tempPos.x, tempPos.y + 1));

                        if (tempPos.x % 2 == 0)
                        {
                            if (!tempList.Contains(new Vector2(tempPos.x - 1, tempPos.y))) tempList.Add(new Vector2(tempPos.x - 1, tempPos.y));
                            if (!tempList.Contains(new Vector2(tempPos.x - 1, tempPos.y - 1))) tempList.Add(new Vector2(tempPos.x - 1, tempPos.y - 1));
                            if (!tempList.Contains(new Vector2(tempPos.x + 1, tempPos.y))) tempList.Add(new Vector2(tempPos.x + 1, tempPos.y));
                            if (!tempList.Contains(new Vector2(tempPos.x + 1, tempPos.y - 1))) tempList.Add(new Vector2(tempPos.x + 1, tempPos.y - 1));
                        }
                        else
                        {
                            if (!tempList.Contains(new Vector2(tempPos.x - 1, tempPos.y))) tempList.Add(new Vector2(tempPos.x - 1, tempPos.y));
                            if (!tempList.Contains(new Vector2(tempPos.x - 1, tempPos.y + 1))) tempList.Add(new Vector2(tempPos.x - 1, tempPos.y + 1));
                            if (!tempList.Contains(new Vector2(tempPos.x + 1, tempPos.y))) tempList.Add(new Vector2(tempPos.x + 1, tempPos.y));
                            if (!tempList.Contains(new Vector2(tempPos.x + 1, tempPos.y + 1))) tempList.Add(new Vector2(tempPos.x + 1, tempPos.y + 1));
                        }
                    }
                }
                tempRange--;
            }
        }
        return tempList;
    }

    public static List<Vector2> GetBestRoute(Vector2 target, Vector2 myPos, ref Graph g, int speed)
    {
        List<Vector2> walkablePositions = Utils.CreateRangeList(g, myPos, speed, ListType.MOVE);
        float diffBetweenPoints;
        float lastDiffBetweenPoints = 0.0f;
        Vector2 finalTargetPos = walkablePositions[0];

        foreach (Vector2 currentPos in walkablePositions.ToArray())
        {
            if (currentPos.x < 0 || currentPos.y < 0)
            {
                walkablePositions.Remove(currentPos);
                continue;
            }

            diffBetweenPoints = Vector2.Distance(target, currentPos);

            if (lastDiffBetweenPoints == 0f || diffBetweenPoints <= lastDiffBetweenPoints)
            {
                lastDiffBetweenPoints = diffBetweenPoints;
                finalTargetPos = currentPos;
            }
        }
        return Astar.Pathfind(g, myPos, finalTargetPos);
    }

    public static Vector3 ConverHexPosToOthers(Hexagon hexagon)
    {
        return new Vector3(hexagon.worldPos.x, hexagon.worldPos.y + 1, hexagon.worldPos.z);
    }
}
