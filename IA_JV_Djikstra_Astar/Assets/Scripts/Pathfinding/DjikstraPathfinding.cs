using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraPathfinding
{
    private Dictionary<Vector2, Tile> tiles; // Tile map

    public DijkstraPathfinding(Dictionary<Vector2, Tile> tileMap) // Constructor
    {
        tiles = tileMap; // Initialize tile map
    }

    public List<Vector2> FindPath(Vector2 start, Vector2 target) // Find path from start to target
    {
        List<Vector2> openList = new List<Vector2>(); // Nodes to be evaluated
        HashSet<Vector2> closedList = new HashSet<Vector2>(); // Nodes already evaluated

        Dictionary<Vector2, float> gCosts = new Dictionary<Vector2, float>(); // Cost from start to current node
        Dictionary<Vector2, Vector2> cameFrom = new Dictionary<Vector2, Vector2>(); // Previous node in optimal path

        openList.Add(start); // Add start node to open list
        gCosts[start] = 0; // Cost from start to start is zero

        while (openList.Count > 0)
        {
            // Get the node with the smallest gCost
            Vector2 currentNode = openList[0];
            foreach (Vector2 node in openList)
            {
                if (gCosts[node] < gCosts[currentNode])
                {
                    currentNode = node;
                }
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            // If the target node is reached, reconstruct the path
            if (currentNode == target)
            {
                return ReconstructPath(cameFrom, currentNode);
            }

            // Check all neighbors of the current node
            foreach (Vector2 neighbor in GetNeighbors(currentNode))
            {
                if (closedList.Contains(neighbor)) // Skip already evaluated nodes
                {
                    continue;
                }

                // Calculate tentative gCost which is the cost from start to the neighbor through the current node
                float tentativeGCost = gCosts[currentNode] + GetDistance(currentNode, neighbor);
                // If the tentative gCost is lower than the current gCost, update the values
                if (!gCosts.ContainsKey(neighbor) || tentativeGCost < gCosts[neighbor])
                {
                    gCosts[neighbor] = tentativeGCost;
                    cameFrom[neighbor] = currentNode;

                    if (!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor); // Add the neighbor to the open list
                    }
                }
            }
        }

        // Return empty path if no path is found
        return new List<Vector2>();
    }

    private List<Vector2> ReconstructPath(Dictionary<Vector2, Vector2> cameFrom, Vector2 currentNode)
    {
        // Reconstruct the path from the cameFrom dictionary
        List<Vector2> path = new List<Vector2>();
        while (cameFrom.ContainsKey(currentNode)) // Loop until the start node is reached
        {
            path.Add(currentNode); // Add the current node to the path
            currentNode = cameFrom[currentNode]; // Move to the previous node
        }
        path.Reverse(); // Reverse the path to get from start to target
        return path;
    }

    private IEnumerable<Vector2> GetNeighbors(Vector2 node)
    {
        // Neighbors based on a 4-directional grid (up, down, left, right) as I use square tiles
        Vector2[] directions = new Vector2[]
        {
            new Vector2(0, 1),  // Up
            new Vector2(0, -1), // Down
            new Vector2(-1, 0), // Left
            new Vector2(1, 0)   // Right
        };

        List<Vector2> neighbors = new List<Vector2>();

        foreach (Vector2 direction in directions)
        {
            Vector2 neighborPos = node + direction; // Calculate the position of the neighbor
            // Check if the tile exists and is walkable (not an obstacle)
            if (tiles.ContainsKey(neighborPos) && tiles[neighborPos].IsWalkable)
            {
                neighbors.Add(neighborPos); // Add the neighbor to the list
            }
        }

        return neighbors; 
    }

    private float GetDistance(Vector2 a, Vector2 b)
    {
        // Returns the cost of moving from tile a to tile b based on tile crossing cost
        if (tiles.ContainsKey(b))
        {
            return tiles[b].CrossingCost; // Return the cost of crossing the tile
        }

        // Default cost if no specific tile is found just in case but should not happen
        return 1f; 
    }
}
