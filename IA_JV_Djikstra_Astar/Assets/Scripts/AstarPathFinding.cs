using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding
{
    private Dictionary<Vector2, Tile> tiles;

    public AStarPathfinding(Dictionary<Vector2, Tile> tileMap)
    {
        tiles = tileMap;
    }

    public List<Vector2> FindPath(Vector2 start, Vector2 target)
    {
        List<Vector2> openList = new List<Vector2>();
        HashSet<Vector2> closedList = new HashSet<Vector2>();

        Dictionary<Vector2, float> gCosts = new Dictionary<Vector2, float>();
        Dictionary<Vector2, float> fCosts = new Dictionary<Vector2, float>();
        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>();

        openList.Add(start);
        gCosts[start] = 0;
        fCosts[start] = GetHeuristicCost(start, target);

        while (openList.Count > 0)
        {
            Vector2 currentNode = GetLowestFCostNode(openList, fCosts);

            if (currentNode == target)
            {
                return ReconstructPath(cameFrom, currentNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (Vector2 neighbor in GetNeighbors(currentNode))
            {
                if (closedList.Contains(neighbor) || !tiles.ContainsKey(neighbor))
                {
                    continue;
                }

                float tentativeGCost = gCosts[currentNode] + GetDistance(currentNode, neighbor);

                if (!openList.Contains(neighbor))
                {
                    openList.Add(neighbor);
                }
                else if (tentativeGCost >= gCosts[neighbor])
                {
                    continue;
                }

                cameFrom[neighbor] = currentNode;
                gCosts[neighbor] = tentativeGCost;
                fCosts[neighbor] = gCosts[neighbor] + GetHeuristicCost(neighbor, target);
            }
        }

        return null; // No path found
    }

    private Vector2 GetLowestFCostNode(List<Vector2> openList, Dictionary<Vector2, float> fCosts)
    {
        Vector2 lowest = openList[0];
        float lowestFCost = fCosts[lowest];

        foreach (Vector2 node in openList)
        {
            if (fCosts[node] < lowestFCost)
            {
                lowest = node;
                lowestFCost = fCosts[node];
            }
        }

        return lowest;
    }

    private List<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 currentNode)
    {
        List<Vector2> path = new List<Vector2>();
        path.Add(currentNode);

        while (cameFrom.ContainsKey(currentNode))
        {
            currentNode = cameFrom[currentNode];
            path.Add(currentNode);
        }

        path.Reverse();
        return path;
    }

    private float GetHeuristicCost(Vector2 a, Vector2 b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y); // Manhattan distance
    }

    private float GetDistance(Vector2 a, Vector2 b)
    {
        return Vector2.Distance(a, b);
    }

    private List<Vector2> GetNeighbors(Vector2 position)
    {
        List<Vector2> neighbors = new List<Vector2>
        {
            new Vector2(position.x + 1, position.y),
            new Vector2(position.x - 1, position.y),
            new Vector2(position.x, position.y + 1),
            new Vector2(position.x, position.y - 1)
        };

        return neighbors;
    }
}
