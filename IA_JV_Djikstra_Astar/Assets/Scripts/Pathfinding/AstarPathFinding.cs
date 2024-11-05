using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding
{
    private Dictionary<Vector2, Tile> tiles; // Tile map

    public AStarPathfinding(Dictionary<Vector2, Tile> tileMap) // Constructor
    {
        tiles = tileMap; // Initialize tile map
    }

    public List<Vector2> FindPath(Vector2 start, Vector2 target) // Find path from start to target
    {
        List<Vector2> openList = new List<Vector2>(); // Nodes to be evaluated
        HashSet<Vector2> closedList = new HashSet<Vector2>(); // Nodes already evaluated

        Dictionary<Vector2, float> gCosts = new Dictionary<Vector2, float>(); // Cost from start to current node
        Dictionary<Vector2, float> fCosts = new Dictionary<Vector2, float>(); // Cost from start to target through current node
        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>(); // Previous node in optimal path

        openList.Add(start); // Add start node to open list
        gCosts[start] = 0; // Cost from start to start is 0
        fCosts[start] = GetHeuristicCost(start, target); // Cost from start to target through start

        while (openList.Count > 0) // While there are nodes to evaluate
        {
            Vector2 currentNode = GetLowestFCostNode(openList, fCosts); // Get node with lowest f cost

            if (currentNode == target) // If target reached
            {
                return ReconstructPath(cameFrom, currentNode); // Return path
            }

            openList.Remove(currentNode); // Remove current node from open list
            closedList.Add(currentNode); // Add current node to closed list

            foreach (Vector2 neighbor in GetNeighbors(currentNode)) // For each neighbor of current node
            {
                // Check if neighbor is already evaluated, is not in the map, or is not walkable
                if (closedList.Contains(neighbor) || !tiles.ContainsKey(neighbor) || !tiles[neighbor].IsWalkable)
                {
                    continue; // Skip this neighbor
                }

                float tentativeGCost = gCosts[currentNode] + GetDistance(currentNode, neighbor); // Cost from start to neighbor through current node

                if (!openList.Contains(neighbor)) // If neighbor is not in open list
                {
                    openList.Add(neighbor); // Add neighbor to open list
                }
                else if (tentativeGCost >= gCosts[neighbor]) // If neighbor is already in open list and new path is not better
                {
                    continue;
                }

                cameFrom[neighbor] = currentNode; // Set previous node of neighbor to current node
                gCosts[neighbor] = tentativeGCost; // Update cost from start to neighbor
                fCosts[neighbor] = gCosts[neighbor] + GetHeuristicCost(neighbor, target); // Update cost from start to target through neighbor
            }
        }

        return null; // No path found
    }

    private Vector2 GetLowestFCostNode(List<Vector2> openList, Dictionary<Vector2, float> fCosts) // Get node with lowest f cost
    {
        Vector2 lowest = openList[0]; // Initialize lowest node to first node in open list
        float lowestFCost = fCosts[lowest]; // Initialize lowest f cost to f cost of lowest node

        foreach (Vector2 node in openList) // For each node in open list
        {
            if (fCosts[node] < lowestFCost) // If f cost of node is lower than lowest f cost
            {
                lowest = node;
                lowestFCost = fCosts[node]; // Update lowest node and lowest f cost
            }
        }

        return lowest; // Return node with lowest f cost
    }

    private List<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 currentNode) // Reconstruct path from start to target
    {
        List<Vector2> path = new List<Vector2>(); // Initialize path
        path.Add(currentNode); // Add target node to path

        while (cameFrom.ContainsKey(currentNode)) // While there are nodes to reconstruct
        {
            currentNode = cameFrom[currentNode]; // Set current node to previous node
            path.Add(currentNode); // Add current node to path
        }

        path.Reverse(); // Reverse path to get start to target
        return path; // Return path
    }

    private float GetHeuristicCost(Vector2 a, Vector2 b) // Get heuristic cost from a to b
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y); // Manhattan distance heuristic
    }

    private float GetDistance(Vector2 a, Vector2 b) // Get distance from a to b
    {
        // Use the tile's crossing cost for more accurate pathfinding
        if (tiles.ContainsKey(b))
        {
            return tiles[b].CrossingCost;
        }

        // Default cost if no specific tile is found
        return 1f;
    }

    private List<Vector2> GetNeighbors(Vector2 position) // Get neighbors of position
    {
        List<Vector2> neighbors = new List<Vector2> // Initialize neighbors
        {
            //another way to get neighbors compared to djikstra but same result/purpose
            new Vector2(position.x + 1, position.y),
            new Vector2(position.x - 1, position.y),
            new Vector2(position.x, position.y + 1),
            new Vector2(position.x, position.y - 1)
        };

        return neighbors;
    }
}
