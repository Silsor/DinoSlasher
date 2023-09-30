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
    public TileBase obstacleTile;

    public int mapWidth = 20;  // Adjust as needed
    public int mapHeight = 20; // Adjust as needed

    public int RoomSize = 3;  // room size
    public int corridorWidth = 1; // Width of corridors

    [SerializeField] private int numRooms;

    private List<Rect> rooms;
    private TileBase[,] grid;

    void Start()
    {
        GenerateMap();
        //GenerateRoom();
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

        rooms = new List<Rect>();

        for (int i = 0; i < numRooms; i++)
        {
            int roomWidth, roomHeight, posx, posy;
            Rect newRoom = GenerateRoom(out roomWidth, out roomHeight, out posx, out posy);
            //Debug.Log("mapWidth: " + x + " " + y);

            rooms.Add(newRoom);

            // Place the floor tiles for the room
            //SetRoomTiles(grid, roomWidth, roomHeight, posx, posy);

            if (i > 0)
            {
                int prevX = (int)newRoom.center.x;
                int prevY = (int)newRoom.center.y;

                //// Create horizontal corridor
                //for (int cx = Mathf.Min(prevX, posx); cx <= Mathf.Max(prevX, posx + roomWidth); cx++)
                //{
                //    for (int cy = prevY - corridorWidth / 2; cy <= prevY + corridorWidth / 2; cy++)
                //    {
                //        grid[cx, cy] = floorTile;
                //    }
                //}

                //// Create vertical corridor
                //for (int cy = Mathf.Min(prevY, y); cy <= Mathf.Max(prevY, y + roomHeight); cy++)
                //{
                //    for (int cx = prevX - corridorWidth / 2; cx <= prevX + corridorWidth / 2; cx++)
                //    {
                //        grid[cx, cy] = floorTile;
                //    }
                //}
            }

            SetRoomTiles(grid, roomWidth, roomHeight, posx, posy);
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

    private void SetRoomTiles(TileBase[,] grid, int roomWidth, int roomHeight, int posx, int posy)
    {
        int maxObstacles = 100;

        for (int rx = posx; rx < posx + roomWidth; rx++)
        {
            for (int ry = posy; ry < posy + roomHeight; ry++)
            {
                if (maxObstacles > 0)
                {
                    bool setObstacle = Random.Range(0, 3) == 1 ? true : false;

                    if (setObstacle)
                    {
                        if (!(rx + ry == 0))
                        {
                            if (grid[rx - 1, ry] == obstacleTile || grid[rx, ry - 1] == obstacleTile || grid[rx - 1, ry - 1] == obstacleTile || grid[rx - 1, ry + 1] == obstacleTile) setObstacle = false;
                        }

                        grid[rx, ry] = setObstacle ? obstacleTile : floorTile;
                        if (setObstacle) maxObstacles--;
                    }
                    else
                        grid[rx, ry] = floorTile;
                }
                else
                    grid[rx, ry] = floorTile;
            }
        }
        Debug.Log("mamy przeszkod: " + (100 - maxObstacles));
    }

    private Rect GenerateRoom(out int roomWidth, out int roomHeight, out int posx, out int posy)
    {
        bool bigRoom = Random.Range(0, 2) == 1 ? true : false;
        roomWidth = RoomSize;//bigRoom ? 5 : 3;
        roomHeight = RoomSize;//bigRoom ? 5 : 3;
        posx = Random.Range(1, mapWidth - roomWidth - 1);
        posy = Random.Range(1, mapHeight - roomHeight - 1);
        Rect newRoom = new Rect(posx, posy, roomWidth, roomHeight);


        // Check if the new room overlaps with existing rooms
        foreach (Rect room in rooms)
        {
            if (newRoom.Overlaps(room))
                newRoom = GenerateRoom(out roomWidth, out roomHeight, out posx, out posy);
        }

        //for (int rx = posx; rx < posx + roomWidth; rx++)
        //{
        //    for (int ry = posy; ry < posy + roomHeight; ry++)
        //    {
        //        Debug.Log(grid is object ? "tak" : "nie");
        //        grid[rx, ry] = floorTile;
        //    }
        //}

        return newRoom;
    }


    private void GenerateRoom()
    {
        TileBase[,] grid = new TileBase[mapWidth, mapHeight];

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                grid[x, y] = x == 0 || x == mapWidth - 1 || y == 0 || y == mapHeight - 1 ? wallTile : floorTile;
            }
        }

        //Place tiles on the Tilemap based on the generated grid
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
