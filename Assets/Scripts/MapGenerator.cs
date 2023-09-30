using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public TileBase wallTile;
    public TileBase floorTile;

    public int mapWidth = 20;  // Adjust as needed
    public int mapHeight = 20; // Adjust as needed

    public int minRoomSize = 3;  // Minimum room size
    public int maxRoomSize = 8;  // Maximum room size
    public int corridorWidth = 1; // Width of corridors

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        // Create a 2D grid to represent your map
        TileBase[,] grid = new TileBase[mapWidth, mapHeight];

        // Fill the grid with wall tiles
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                grid[x, y] = wallTile;
            }
        }

        // Generate rooms and corridors
        int numRooms = Random.Range(5, 10); // Adjust the range as needed

        for (int i = 0; i < numRooms; i++)
        {
            int roomWidth = Random.Range(minRoomSize, maxRoomSize);
            int roomHeight = Random.Range(minRoomSize, maxRoomSize);

            int x = Random.Range(1, mapWidth - roomWidth - 1);
            int y = Random.Range(1, mapHeight - roomHeight - 1);

            // Place the floor tiles for the room
            for (int rx = x; rx < x + roomWidth; rx++)
            {
                for (int ry = y; ry < y + roomHeight; ry++)
                {
                    grid[rx, ry] = floorTile;
                }
            }

            // Connect the rooms with corridors
            if (i > 0)
            {
                int prevX = Random.Range(1, mapWidth - 1);
                int prevY = Random.Range(1, mapHeight - 1);

                // Create a horizontal corridor
                for (int cx = Mathf.Min(prevX, x); cx <= Mathf.Max(prevX, x + roomWidth); cx++)
                {
                    for (int cy = prevY; cy <= y + roomHeight; cy++)
                    {
                        grid[cx, cy] = floorTile;
                    }
                }

                // Create a vertical corridor
                for (int cy = Mathf.Min(prevY, y); cy <= Mathf.Max(prevY, y + roomHeight); cy++)
                {
                    for (int cx = prevX; cx <= x + roomWidth; cx++)
                    {
                        grid[cx, cy] = floorTile;
                    }
                }
            }
        }

        // Place tiles on the Tilemap based on the generated grid
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                tilemap.SetTile(tilePosition, grid[x, y]);
            }
        }
    }
}
