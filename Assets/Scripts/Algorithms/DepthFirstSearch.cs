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
            cell.Visited = true;
            List<string> unvisitedNeighbours = grid.GetUnvisitedNeighbours(x, y);
            while (unvisitedNeighbours.Count > 0)
            {
                string direction = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
                unvisitedNeighbours.Remove(direction);
                (int, int) coordinates = grid.DirectionToCoordinates(direction, x, y);
                try
                {
                    MazeCell move = grid.grid[coordinates.Item1, coordinates.Item2];
                    await Task.Delay(500);
                    cell.DisableWall(direction);
                    move.DisableWall(grid.FlipDirection(direction));
                    await DepthFirstAlgorithm(grid, move, move.Coordinates.Item1, move.Coordinates.Item2);
                }
                catch
                {
                    Debug.Log(coordinates);
                }
            }
        }

        /// <summary>
        /// Run the selected algorithm.
        /// </summary>
        public async void RunAlgorithm(MazeGrid grid, int x, int y)
        {
            await DepthFirstAlgorithm(grid, grid.grid[x, y], x, y);
        }
    }
}