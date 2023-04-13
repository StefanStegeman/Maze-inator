using System.Collections.Generic;
using System.Linq;

namespace Mazinator
{
    [System.Serializable]
    public class MazeGrid
    {
        private int width;
        private int height;
        private Dictionary<(int, int), Dictionary<string, bool>> grid;
        private Dictionary<string, bool> nodeData;

        public MazeGrid(int width, int height)
        {
            this.width = width;
            this.height = height;
            nodeData = new Dictionary<string, bool>()
            {
                { "Visited", false },
                { "North", false },
                { "East", false },
                { "South", false },
                { "West", false }
            };
            InitializeGrid();
        }

        /// <summary>
        /// Resets the nodeData of all coordinates.
        /// </summary>
        public void ResetGrid()
        {
            grid.Clear();
            InitializeGrid();
        }

        /// <summary>
        /// Resizes the grid.
        /// </summary>
        /// <param name="width">New width of the grid</param>
        /// <param name="height">New height of the grid</param>
        public void ResizeGrid(int width, int height)
        {
            ResetGrid();
            this.width = width;
            this.height = height;
            InitializeGrid();
        }

        /// <summary>
        /// Change the nodeData of specific element.
        /// </summary>
        /// <param name="coordinate">Coordinates for the requested element</param>
        /// <param name="key">Key of the nodeData to be changed</param>
        /// <param name="newValue">New value for the corresponding key</param>
        public void ChangeNodeData((int, int) coordinate, string key, bool newValue)
        {
            grid[coordinate][key] = newValue;
        }

        /// <summary>
        /// Get the grid dictionary.
        /// </summary>
        /// <returns>The grid dictionary</returns>
        public Dictionary<(int, int), Dictionary<string, bool>> GetGrid()
        {
            return grid;
        }

        /// <summary>
        /// Gets nodeData from a specific element in the grid dictionary.
        /// </summary>
        /// <param name="coordinates">Coordinates of the requested element.</param>
        /// <returns>nodeData dictionary of requested element</returns>
        public Dictionary<string, bool> GetNodeData((int, int) coordinates)
        {
            return grid[coordinates];
        }

        /// <summary>
        /// Initialize the grid dictionary with all coordinates and the corresponding (default) nodeData.
        /// The nodeData will be cloned to prevent assigning it by reference.
        /// </summary>
        private void InitializeGrid()
        {
            grid = new Dictionary<(int, int), Dictionary<string, bool>>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid.Add((x, y), nodeData.ToDictionary(element => element.Key, element => element.Value));
                }
            }
        }
    }
}