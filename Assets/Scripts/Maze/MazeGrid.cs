using UnityEngine;
using System.Collections.Generic;

namespace Mazinator
{
    public class MazeGrid : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        public MazeCell[,] Grid { private set; get; }
        [HideInInspector] public int width { private set; get; }
        [HideInInspector] public int height { private set; get; }

        public List<string> path;
        public void CreateGrid(int width, int height)
        {
            this.width = width;
            this.height = height;
            InitializeGrid();
            path = new List<string>();
        }

        /// <summary>
        /// Reset and re-initialize the grid.
        /// </summary>
        public void ResetGrid()
        {
            InitializeGrid();
        }

        /// <summary>
        /// Initialize the grid according to the preferred size.
        /// </summary>
        private void InitializeGrid()
        {
            Grid = new MazeCell[width, height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    GameObject cell = GameObject.Instantiate(prefab, new Vector3Int(x, y, 0), Quaternion.identity, transform);
                    cell.name = string.Format("Cell ({0}, {1})", x, y);
                    MazeCell newCell = cell.GetComponent<MazeCell>();
                    newCell.Coordinates = (x, y);
                    Grid[x, y] = newCell;
                }
            }
        }

        /// <summary>
        /// Check whether the cell has been visited.
        /// </summary>
        /// <param name="x">x coordinate of cell</param>
        /// <param name="y">y coordinate of cell</param>
        /// <returns>whether the cell has been visited.</returns>
        public int IsVisited(int x, int y)
        {
            try
            {
                return Grid[x, y].Visited ? 1 : 0;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Calculate unvisited neighbours of the passed cell coordinates.
        /// </summary>
        /// <param name="x">x coordinate of cell</param>
        /// <param name="y">y coordinate of cell</param>
        /// <returns>list with the directions of all unvisited neighbours.</returns>
        public List<string> GetUnvisitedNeighbours(int x, int y)
        {
            List<string> unvisitedNeighbours = new List<string>();
            if (IsVisited(x, y + 1) == 0)
            {
                unvisitedNeighbours.Add("north");
            }
            if (IsVisited(x + 1, y) == 0)
            {
                unvisitedNeighbours.Add("east");
            }
            if (IsVisited(x, y - 1) == 0)
            {
                unvisitedNeighbours.Add("south");
            }
            if (IsVisited(x - 1, y) == 0)
            {
                unvisitedNeighbours.Add("west");
            }
            return unvisitedNeighbours;
        }

        /// <summary>
        /// Convert direction into cell coordinates
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="x">x coordinate of cell</param>
        /// <param name="y">y coordinate of cell</param>
        /// <returns>coordinates of the direction + passed cell coordinates.</returns>
        public (int, int) DirectionToCoordinates(string direction, int x, int y)
        {
            switch(direction)
            {
                case "north":
                    return (x + 0, y + 1);
                case "east":
                    return (x + 1, y + 0);
                case "south":
                    return (x + 0, y - 1);
                case "west":
                    return (x - 1, y + 0);
                default:
                    Debug.Log("Coordinates out of the grid.");
                    return (-1, -1);
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