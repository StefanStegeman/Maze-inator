using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mazinator
{
    public class DepthFirstSearch 
    {
        /// <summary>
        /// Run depth-first search algorithm to create a maze.
        /// </summary>
        /// <param name="cell">starting cell</param>
        /// <param name="x">x coordinate of starting cell</param>
        /// <param name="y">y coordinate of starting cell</param>
        private async Task DepthFirstAlgorithm(MazeGrid grid, MazeCell cell, int x, int y)
        {
            grid.Grid[x, y].Visited = true;
            List<string> unvisitedNeighbours = grid.GetUnvisitedNeighbours(x, y);
            string n = "";
            foreach (string direction in unvisitedNeighbours)
            {
                n += string.Format("({0}, {1}) ", direction, grid.DirectionToCoordinates(direction, x, y));
            }
            grid.path.Add(string.Format("({0}, {1}), {2}, with neighbours: {3}", x, y, grid.Grid[x, y].Visited, n));
            while (unvisitedNeighbours.Count > 0)
            {
                string direction = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
                unvisitedNeighbours.Remove(direction);
                (int, int) coordinates = grid.DirectionToCoordinates(direction, x, y);
                MazeCell move = cell;
                try
                {
                    move = grid.Grid[coordinates.Item1, coordinates.Item2];
                }
                catch
                {
                    Debug.Log(string.Format("coordinates from catch {0}", coordinates));
                    continue;
                }
                await Task.Delay(1000);
                cell.DisableWall(direction);
                move.DisableWall(grid.FlipDirection(direction));
                await DepthFirstAlgorithm(grid, move, move.Coordinates.Item1, move.Coordinates.Item2);
            }
        }

        /// <summary>
        /// Run the selected algorithm.
        /// </summary>
        public async void RunAlgorithm(MazeGrid grid)
        {
            await DepthFirstAlgorithm(grid, grid.Grid[0, 0], 0, 0);
        }
    }
}