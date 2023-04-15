using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mazinator
{
    public class DepthFirstSearch : Algorithm
    {
        private async Task DepthFirstAlgorithm(MazeGrid grid, MazeCell cell, int x, int y)
        {
            visited.Add((x, y), true);
            List<string> unvisitedNeighbours = GetUnvisitedNeighbours(grid, x, y);
            while (unvisitedNeighbours.Count > 0)
            {
                string direction = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
                unvisitedNeighbours.Remove(direction);
                (int, int) coordinates = grid.DirectionToCoordinates(direction, x, y);
                if (!visited.ContainsKey((coordinates.Item1, coordinates.Item2)))
                {
                    MazeCell move = grid.Grid[coordinates.Item1, coordinates.Item2];
                    await Task.Delay(miliseconds);
                    DisableWalls(grid, direction, cell, move);
                    await DepthFirstAlgorithm(grid, move, move.Coordinates.Item1, move.Coordinates.Item2);
                }
            }
        }

        /// <summary>
        /// Run the selected algorithm.
        /// </summary>
        public async void RunAlgorithm(MazeGrid grid)
        {
            await DepthFirstAlgorithm(grid, grid.Grid[0, 0], 0, 0);
            visited.Clear();
        }
    }
}