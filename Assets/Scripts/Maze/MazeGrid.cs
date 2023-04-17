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

        [HideInInspector] public int width { private set; get; }
        [HideInInspector] public int height { private set; get; }
        private float startX;
        private float startY;
        public MazeTile[, ] Grid {private set; get;}
        public HashSet<Vector3Int> visited;

        private Dictionary<(int, int), GameObject> allCells;

        [SerializeField] private Tilemap tilemap;
        [SerializeField] private Tile emptyTile;
        [SerializeField] private Tile startingTile;

        [SerializeField] Tiles[] tileArray;
        private Dictionary<string, Tile> tileDictionary;
        [SerializeField] private Color startingColor;
        [SerializeField] private Color finalColor;

        private void Start()
        {
            visited = new HashSet<Vector3Int>();
            InitializeTileDictionary();
        }

        /// <summary>
        /// Initialize the tile dictionary.
        /// </summary>
        private void InitializeTileDictionary()
        {
            tileDictionary = new Dictionary<string, Tile>();
            foreach (Tiles tile in tileArray)
            {
                tileDictionary.Add(tile.name, tile.tile);
            }
        }

        /// <summary>
        /// Reset the tilemap.
        /// </summary>
        public void ResetTileMap()
        {
            visited.Clear();
            tilemap.ClearAllTiles();
        }

        /// <summary>
        /// Create tilemap.
        /// </summary>
        /// <param name="width">width of map</param>
        /// <param name="height">height of map</param>
        public void CreateTileMap(int width, int height)
        {
            this.width = width;
            this.height = height;
            Grid = new MazeTile[width, height];
            InitializeTiles();
        }

        /// <summary>
        /// Initialize the grid with all tiles.
        /// </summary>
        public void InitializeTiles()
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Vector3Int coordinates = new Vector3Int(x, y, 0);
                    Grid[x, y] = new MazeTile(coordinates, startingColor, finalColor, tileDictionary);
                    tilemap.SetTile(coordinates, startingTile);
                }
            }
        }

        /// <summary>
        /// Check if cell has been visited.
        /// </summary>
        /// <param name="coordinate">coordinates of cell</param>
        /// <returns>whether the cell has been visited or not. if it does not exist it will return -1</returns>
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

        /// <summary>
        /// Disable walls of two cells.
        /// </summary>
        /// <param name="direction">the direction of the wall</param>
        /// <param name="cell1">coordinates of the first cell</param>
        /// <param name="cell2">coordinates of the second cell</param>
        public void DisableWalls(string direction, Vector3Int cell1, Vector3Int cell2)
        {
            Grid[cell1.x, cell1.y].DisableWall(direction, tilemap);
            Grid[cell2.x, cell2.y].DisableWall(FlipDirection(direction), tilemap);
        }

        /// <summary>
        /// Get unvisited neighbours.
        /// </summary>
        /// <param name="x">x coordinate</param>
        /// <param name="y">y coordinate</param>
        /// <returns>list with all unvisited neighbours</returns>
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

        /// <summary>
        /// Convert direction string to coordinates.
        /// </summary>
        /// <param name="direction">string which will be converted</param>
        /// <param name="startingCoordinates">starting coordinates</param>
        /// <returns>coordinates which are the starting coordinates with the new direction added</returns>
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