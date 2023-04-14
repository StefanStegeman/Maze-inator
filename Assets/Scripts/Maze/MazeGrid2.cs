using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mazinator
{
    public class MazeGrid2 : MonoBehaviour
    {
        [SerializeField] private GameObject cell;
        MazeCell[,] grid;
        public int Width { get; private set; }
        public int Height { get; private set; }


        private void Start()
        {
            Width = 5;
            Height = 5;
            InitializeGrid();
            RunAlgorithm();
        }

        private void InitializeGrid()
        {
            grid = new MazeCell[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    GameObject prefab = GameObject.Instantiate(cell, new Vector3Int(x, y, 0), Quaternion.identity, transform);
                    MazeCell newCell = prefab.GetComponent<MazeCell>();
                    newCell.Coordinates = (x, y);
                    grid[x, y] = newCell;
                }
            }
        }

        private bool IsVisited(int x, int y)
        {
            try
            {
                return grid[x, y].Visited;
            }
            catch
            {
                return true;
            }
        }

        private List<string> GetUnvisitedNeighbours(int x, int y)
        {
            List<string> unvisitedNeighbours = new List<string>();
            // Check north neighbour
            if (!IsVisited(x, y + 1))
            {
                unvisitedNeighbours.Add("north");
            }
            // Check eastern Neighbour
            if (!IsVisited(x + 1, y))
            {
                unvisitedNeighbours.Add("east");
            }
            // Check southern Neighbour
            if (!IsVisited(x, y - 1))
            {
                unvisitedNeighbours.Add("south");
            }
            // Check west Neighbour
            if (!IsVisited(x - 1, y))
            {
                unvisitedNeighbours.Add("west");
            }
            return unvisitedNeighbours;
        }

        private (int, int) DirectionToCoordinates(string direction, int x, int y)
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
                    return (x + 1, y + 0);
                default:
                    Debug.Log("Coordinates out of the grid.");
                    return (-1, -1);
            }
        }

        private string FlipDirection(string direction)
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

        private async Task Algorithm(MazeCell cell, int x, int y)
        {
            cell.Visited = true;
            List<string> unvisitedNeighbours = GetUnvisitedNeighbours(x, y);
            while (unvisitedNeighbours.Count > 0)
            {
                string direction = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
                unvisitedNeighbours.Remove(direction);
                (int, int) coordinates = DirectionToCoordinates(direction, x, y);
                try
                {
                    MazeCell move = grid[coordinates.Item1, coordinates.Item2];
                    await Task.Delay(500);
                    cell.DisableWall(direction);
                    move.DisableWall(FlipDirection(direction));
                    await Algorithm(move, move.Coordinates.Item1, move.Coordinates.Item2);
                }
                catch
                {
                    Debug.Log(coordinates);
                }
            }
        }

        private async void RunAlgorithm()
        {
            await Algorithm(grid[0, 0], 0, 0);
        }
    }
}