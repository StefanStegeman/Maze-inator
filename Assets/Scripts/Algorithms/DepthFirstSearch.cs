using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Mazinator
{
    public class DepthFirstSearch : Algorithm 
    {
        private IEnumerator DFSA(MazeGrid grid, Vector3Int cell)
        {
            grid.visited.Add(cell);
            List<string> neighbours = grid.GetUnvisitedNeighbours(cell.x, cell.y);
            while (neighbours.Count > 0)
            {
                string direction = neighbours[Random.Range(0, neighbours.Count)];
                neighbours.Remove(direction);
                Vector3Int coordinates = grid.DirectionToCoordinates(direction, cell);
                if (!grid.visited.Contains(coordinates))
                {
                    yield return new WaitForSeconds(miliseconds * 0.001f);
                    grid.DisableWalls(direction, cell, coordinates);
                    yield return StartCoroutine(DFSA(grid, coordinates));
                }
            }
        }

        /// <summary>
        /// Run the selected algorithm.
        /// </summary>
        public void RunAlgorithm(ref MazeGrid grid)
        {
            StartCoroutine(DFSA(grid, new Vector3Int(0, 0, 0)));
        }
    }
}