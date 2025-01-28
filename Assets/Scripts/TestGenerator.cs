using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject wallPrefab;
    public int width = 100;
    public int height = 100;

    private int[,] maze;

    private const float floorSize = 4f; // Size of the floor prefab (4x4)
    private const float wallWidth = 4f; // Width of the wall prefab (4)
    private const float wallHeight = 3f; // Height of the wall prefab (3)

    void Start()
    {
        GenerateMaze();
        BuildMaze();
    }

    void GenerateMaze()
    {
        maze = new int[width, height];

        // Initialize the maze 
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = 1; // 
            }
        }

        // Start carving the maze from a random position
        int startX = 1;
        int startY = 1;

        Carve(startX, startY);
    }

    void Carve(int x, int y)
    {
        // Mark the current cell as a floor (0)
        maze[x, y] = 0;

        // Shuffle the directions to make the maze random
        List<Vector2Int> directions = new List<Vector2Int>
        {
            new Vector2Int(1, 0),  // Right
            new Vector2Int(-1, 0), // Left
            new Vector2Int(0, 1),  // Up
            new Vector2Int(0, -1)  // Down
        };
        directions.Sort((a, b) => Random.Range(-1, 2));

        foreach (var dir in directions)
        {
            int nx = x + dir.x * 2;
            int ny = y + dir.y * 2;

            if (nx > 0 && ny > 0 && nx < width - 1 && ny < height - 1 && maze[nx, ny] == 1)
            {
                // Carve through the wall between cells
                maze[x + dir.x, y + dir.y] = 0;
                Carve(nx, ny);
            }
        }
    }

    void BuildMaze()
{
    // First, create the floors at Y = 0
    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            Vector3 floorPosition = new Vector3(x * floorSize, 0, y * floorSize);
            if (maze[x, y] == 0)
            {
                // Place floor at Y = 0
                Instantiate(floorPrefab, floorPosition, Quaternion.identity, transform);
            }
        }
    }

    // Then, create the walls at Y = 0 (aligned with the floor)
    for (int x = 0; x < width; x++)
    {
        for (int y = 0; y < height; y++)
        {
            // Only create walls for cells marked as walls (1)
            if (maze[x, y] == 1)
            {
                 // Create left wall if left neighbor is not a floor and right neighbor is a floor
                if ((x == 0 || maze[x - 1, y] == 0) && (x < width - 1 && maze[x + 1, y] == 0)) // Left not floor, Right is floor
                {
                    Vector3 leftWallPosition = new Vector3(x , 0, y * floorSize); 
                    Instantiate(wallPrefab, leftWallPosition, Quaternion.Euler(0, 90, 0), transform); // Rotate to face left
                }

                // Create right wall if right neighbor is not a floor and left neighbor is a floor
                if ((x == width - 1 || maze[x + 1, y] == 0) && (x > 0 && maze[x - 1, y] == 0)) // Right not floor, Left is floor
                {
                    Vector3 rightWallPosition = new Vector3((x * floorSize)-4, 0, y * floorSize + 4);
                    Instantiate(wallPrefab, rightWallPosition, Quaternion.Euler(0, -90, 0), transform); // Rotate to face right
                }

           /*     // Front wall (facing forward/up)
                if (y == height - 1 || maze[x, y + 1] == 0) // Only create if upward neighbor is a floor
                {
                    Vector3 frontWallPosition = new Vector3(x * floorSize, 0, y * floorSize + (floorSize / 2));
                    Instantiate(wallPrefab, frontWallPosition, Quaternion.Euler(0, 0, 0), transform); // No rotation for front wall
                }

                // Back wall (facing downward)
                if (y == 0 || maze[x, y - 1] == 0) // Only create if downward neighbor is a floor
                {
                    Vector3 backWallPosition = new Vector3(x * floorSize, 0, y * floorSize - (floorSize / 2));
                    Instantiate(wallPrefab, backWallPosition, Quaternion.Euler(0, 0, 0), transform); // No rotation for back wall
                }*/
            }
        }
    }
}


}
