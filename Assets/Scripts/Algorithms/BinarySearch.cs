using System.Collections;
using UnityEngine;

namespace Mazinator
{
    public class BinarySearch : Algorithm
    {
        /// <summary>
        /// Binary search maze generator.
        /// </summary>
        /// <param name="grid">the grid of the maze</param>
        /// <param name="cell">the current cell</param>
        /// <param name="directions">two possible directions to choose from</param>
        private IEnumerator Algorithm(MazeGrid grid, Vector3Int cell, string[] directions)
        {
            int index = Random.Range(0, 2);
            string direction = directions[index];
            Vector3Int coordinates = grid.DirectionToCoordinates(direction, cell);
            if (grid.IsVisited(coordinates) == -1)
            {
                Debug.Log("bestaat niet");
                direction = direction == directions[0] ? directions[1] : directions[0];
                coordinates = grid.DirectionToCoordinates(direction, cell);
                if (grid.IsVisited(coordinates) != -1)
                {
                    grid.DisableWalls(direction, cell, coordinates);
                }
            }
            else
            {
                Debug.Log("bestaat wel");
                grid.DisableWalls(direction, cell, coordinates);
            }
            yield return new WaitForSeconds(miliseconds);
        }

        /// <summary>
        /// Run the selected algorithm.
        /// </summary>
        public override void Run(MazeGrid grid)
        {
            string[] directions = new string[2] {"north", "east"};
            for (int y = 0; y < grid.height; y++)
            {
                for (int x = 0; x < grid.width; x++)
                {
                    StartCoroutine(Algorithm(grid, new Vector3Int(x, y, 0), directions));
                }
            }
        }
    }
}