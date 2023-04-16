using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

namespace Mazinator
{
    [System.Serializable]
    public class Tiles
    {
        public string name;
        public Tile tile;
    }

    public class MazeGrid : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        // public MazeCell[,] Grid { private set; get; }
        // public Tile[, ] newGrid { private set; get; }

        [HideInInspector] public int width { private set; get; }
        [HideInInspector] public int height { private set; get; }
        private float startX;
        private float startY;
        // public Dictionary<Vector3Int, MazeTile> tiles;
        public MazeTile[, ] Grid {private set; get;}
        public HashSet<Vector3Int> visited;

        private Dictionary<(int, int), GameObject> allCells;
        [SerializeField] private int maxSize = 250;

        [SerializeField] private Tilemap tilemap;
        [SerializeField] private Tile emptyTile;
        [SerializeField] private Tile startingTile;

        [SerializeField] Tiles[] tileArray;
        private Dictionary<string, Tile> tileDictionary;
        private Dictionary<Vector3Int, string> walls;

        private void Start()
        {
            visited = new HashSet<Vector3Int>();
            // tiles = new Dictionary<Vector3Int, MazeTile>();
            InitializeTileDictionary();
        }

        private void InitializeTileDictionary()
        {
            tileDictionary = new Dictionary<string, Tile>();
            foreach (Tiles tile in tileArray)
            {
                tileDictionary.Add(tile.name, tile.tile);
            }
        }

        public void ResetTileMap()
        {
            visited.Clear();
            // tiles.Clear();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), emptyTile);
                }
            }
        }

        public void CreateTileMap(int width, int height)
        {
            this.width = width;
            this.height = height;
            InitializeTiles();
        }

        public void InitializeTiles()
        {
            Grid = new MazeTile[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < height; x++)
                {
                    Vector3Int coordinates = new Vector3Int(x, y, 0);
                    Grid[x, y] = new MazeTile(coordinates, Color.black, Color.white, tileDictionary);
                    // tiles.Add(coordinates, new MazeTile(coordinates, Color.black, Color.white, tileDictionary));
                    tilemap.SetTile(coordinates, startingTile);
                }
            }
        }

        public int IsVisited(Vector3Int coordinate)
        {
            try
            {
                return Grid[coordinate.x, coordinate.y].visited ? 1 : 0;
            }
            catch
            {
                return -1;
            }
        }

        public void DisableWalls(string direction, Vector3Int cell1, Vector3Int cell2)
        {
            Grid[cell1.x, cell1.y].DisableWall(direction, tilemap);
            Grid[cell2.x, cell2.y].DisableWall(FlipDirection(direction), tilemap);
        }

        public List<string> GetUnvisitedNeighbours(int x, int y)
        {
            List<string> unvisitedNeighbours = new List<string>();
            if (IsVisited(new Vector3Int(x, y + 1, 0)) == 0)
            {
                unvisitedNeighbours.Add("north");
            }
            if (IsVisited(new Vector3Int(x + 1, y, 0)) == 0)
            {
                unvisitedNeighbours.Add("east");
            }
            if (IsVisited(new Vector3Int(x, y - 1, 0)) == 0)
            {
                unvisitedNeighbours.Add("south");
            }
            if (IsVisited(new Vector3Int(x - 1, y, 0)) == 0)
            {
                unvisitedNeighbours.Add("west");
            }
            return unvisitedNeighbours;
        }

        public Vector3Int DirectionToCoordinates(string direction, Vector3Int startingCoordinates)
        {
            switch(direction)
            {
                case "north":
                    return new Vector3Int(startingCoordinates.x, startingCoordinates.y + 1, 0);
                case "east":
                    return new Vector3Int(startingCoordinates.x + 1, startingCoordinates.y, 0);
                case "south":
                    return new Vector3Int(startingCoordinates.x, startingCoordinates.y - 1, 0);
                case "west":
                    return new Vector3Int(startingCoordinates.x - 1, startingCoordinates.y, 0);
                default:
                    return new Vector3Int(-1, -1, -1);
            }
        }

        /// <summary>
        /// Get the opposite of the passed direction.
        /// </summary>
        /// <param name="direction">the direction which will be flipped.</param>
        /// <returns>the flipped direction.</returns>
        public string FlipDirection(string direction)
        {
            switch (direction)
            {
                case "north":
                    return "south";
                case "south":
                    return "north";    
                case "east":
                    return "west";
                case "west":
                    return "east";
                default:
                    Debug.Log(string.Format("The direction {0} does not exist", direction));
                    return "error";
            }
        }
    }
}