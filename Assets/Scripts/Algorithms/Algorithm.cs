using UnityEngine;
using System.Collections.Generic;

namespace Mazinator
{
    public class Algorithm
    {
        protected Dictionary<(int, int), bool> visited;

        public Algorithm()
        {
            visited = new Dictionary<(int, int), bool>();
        }

        protected void Reset()
        {
            visited.Clear();
        }

        protected List<string> GetUnvisitedNeighbours(MazeGrid grid, int x, int y)
        {
            List<string> unvisitedNeighbours = new List<string>();
            if (IsVisited(grid, x, y + 1) == 0)
            {
                unvisitedNeighbours.Add("north");
            }
            if (IsVisited(grid, x + 1, y) == 0)
            {
                unvisitedNeighbours.Add("east");
            }
            if (IsVisited(grid, x, y - 1) == 0)
            {
                unvisitedNeighbours.Add("south");
            }
            if (IsVisited(grid, x - 1, y) == 0)
            {
                unvisitedNeighbours.Add("west");
            }
            return unvisitedNeighbours;
        }

        protected void DisableWalls(MazeGrid grid, string direction, MazeCell cell1, MazeCell cell2)
        {
            cell1.DisableWall(direction);
            cell2.DisableWall(grid.FlipDirection(direction));
            cell1.GetComponent<SpriteRenderer>().color = Color.white;
            cell2.GetComponent<SpriteRenderer>().color = Color.white;
        }

        protected int IsVisited(MazeGrid grid, int x, int y)
        {
            try
            {
                bool exists = grid.Grid[x, y].Visited;
            }
            catch
            {
                return - 1;
            }
            return visited.ContainsKey((x, y)) ? 1 : 0;
        }
    }
}