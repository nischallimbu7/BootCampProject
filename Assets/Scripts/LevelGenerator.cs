using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject wallPrefab;      // Wall prefab
    public GameObject pathPrefab;      // Path prefab

    [Header("Maze Settings")]
    public int width = 10;             // Width of the maze (must be odd)
    public int height = 10;            // Height of the maze (must be odd)
    public float tileSize = 1f;        // Size of each tile

    private Transform mazeParent;       // Parent object for organizing the generated maze
    private bool[,] maze;               // 2D array to store maze data

    private void Start()
    {
        GenerateMaze();
    }

    private void GenerateMaze()
    {
        mazeParent = new GameObject("GeneratedMaze").transform;

        // Initialize maze array
        maze = new bool[width, height];

        // Generate maze using Recursive Backtracking
        for (int x = 0; x < width; x++)
            for (int z = 0; z < height; z++)
                maze[x, z] = false; // Set all cells as walls

        // Start the maze generation from a random position
        int startX = Random.Range(0, width / 2) * 2 + 1;
        int startZ = Random.Range(0, height / 2) * 2 + 1;
        GenerateMazeRecursive(startX, startZ);

        // Create the maze in the scene
        CreateMaze();
    }

    private void GenerateMazeRecursive(int x, int z)
    {
        // Mark the current cell as a path
        maze[x, z] = true;

        // Create a list of directions (right, left, down, up)
        List<Vector2Int> directions = new List<Vector2Int>
        {
            new Vector2Int(2, 0), // Right
            new Vector2Int(-2, 0), // Left
            new Vector2Int(0, 2), // Down
            new Vector2Int(0, -2) // Up
        };

        // Shuffle directions
        for (int i = 0; i < directions.Count; i++)
        {
            Vector2Int temp = directions[i];
            int randomIndex = Random.Range(i, directions.Count);
            directions[i] = directions[randomIndex];
            directions[randomIndex] = temp;
        }

        // Visit each direction
        foreach (Vector2Int direction in directions)
        {
            int newX = x + direction.x;
            int newZ = z + direction.y;

            // Check if the new position is within bounds
            if (IsInBounds(newX, newZ) && !maze[newX, newZ])
            {
                // Remove wall between the current and new cell
                maze[x + direction.x / 2, z + direction.y / 2] = true;
                GenerateMazeRecursive(newX, newZ);
            }
        }
    }

    private bool IsInBounds(int x, int z)
    {
        return x > 0 && x < width && z > 0 && z < height;
    }

    private void CreateMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 position = new Vector3(x * tileSize, 0, z * tileSize);
                if (maze[x, z])
                {
                    // Create path
                    Instantiate(pathPrefab, position, Quaternion.identity, mazeParent);
                }
                else
                {
                    // Create wall
                    Instantiate(wallPrefab, position, Quaternion.identity, mazeParent);
                }
            }
        }
    }
}
