using UnityEngine;
using System.Collections.Generic;

namespace Mazinator
{
    public class DepthFirstSearch
    {
        public DepthFirstSearch()
        {

        }

        public void Run(MazeGrid grid)
        {
            List<int> unvisitedNeighbours = GetUnvisitedNeighbours(grid, 1, 1);
            unvisitedNeighbours.ForEach(neighbour => Debug.Log(neighbour));
        }

        private bool IsVisited(MazeGrid grid, int x, int y)
        {
            try
            {
                return grid.GetNodeData((x, y)).visited;
            }
            catch
            {
                return true;
            }
        }

        private List<int> GetUnvisitedNeighbours(MazeGrid grid, int x, int y)
        {
            List<int> unvisitedNeighbours = new List<int>();
            
            // Northern Neighbour
            if (!IsVisited(grid, x, y - 1))
            {
                unvisitedNeighbours.Add(0);
            }
            // Eastern Neighbour
            if (!IsVisited(grid, x + 1, y))
            {
                unvisitedNeighbours.Add(1);
            }
            // Southern Neighbour
            if (!IsVisited(grid, x, y + 1))
            {
                unvisitedNeighbours.Add(2);
            }
            // West Neighbour
            if (!IsVisited(grid, x - 1, y))
            {
                unvisitedNeighbours.Add(3);
            }
            return unvisitedNeighbours;
        }

        // private List<int> MakeMove(MazeGrid grid, List<int> unvisitedNeighbours)
        // {
        //     if (unvisitedNeighbours.Count == 0)
        //     {
        //         return new List<int>();
        //     } 
        //     int direction = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];
        //     // CachedAssetBundle 
        // }
    }
}