using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    private List<Rect> rooms;

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        TileBase[,] grid = new TileBase[mapWidth, mapHeight];

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                grid[x, y] = wallTile;
            }
        }

        int numRooms = 15;//Random.Range(5, 10);

        rooms = new List<Rect>();

        for (int i = 0; i < numRooms; i++)
        {
            int roomWidth, roomHeight, x, y;
            Rect newRoom = GenerateRoom(out roomWidth, out roomHeight, out x, out y);
            //Debug.Log("mapWidth: " + x + " " + y);

            rooms.Add(newRoom);

            // Place the floor tiles for the room
            for (int rx = x; rx < x + roomWidth; rx++)
            {
                for (int ry = y; ry < y + roomHeight; ry++)
                {
                    grid[rx, ry] = floorTile;
                }
            }

            if (i > 0)
            {
                int prevX = (int)newRoom.center.x;
                int prevY = (int)newRoom.center.y;

                // Create horizontal corridor
                for (int cx = Mathf.Min(prevX, x); cx <= Mathf.Max(prevX, x + roomWidth); cx++)
                {
                    for (int cy = prevY - corridorWidth / 2; cy <= prevY + corridorWidth / 2; cy++)
                    {
                        grid[cx, cy] = floorTile;
                    }
                }

                // Create vertical corridor
                for (int cy = Mathf.Min(prevY, y); cy <= Mathf.Max(prevY, y + roomHeight); cy++)
                {
                    for (int cx = prevX - corridorWidth / 2; cx <= prevX + corridorWidth / 2; cx++)
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

    private Rect GenerateRoom(out int roomWidth, out int roomHeight, out int x, out int y)
    {
        roomWidth = 2;
        roomHeight = 3;
        x = Random.Range(1, mapWidth - roomWidth - 1);
        y = Random.Range(1, mapHeight - roomHeight - 1);
        Rect newRoom = new Rect(x, y, roomWidth, roomHeight);


        // Check if the new room overlaps with existing rooms
        foreach (Rect room in rooms)
        {
            if (newRoom.Overlaps(room))
                newRoom = GenerateRoom(out roomWidth, out roomHeight, out x, out y);
        }

        return newRoom;
    }
}
