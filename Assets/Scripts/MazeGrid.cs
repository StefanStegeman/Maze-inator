using System.Collections.Generic;

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
                { "West", false },
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
        /// Initialize the grid dictionary with all coordinates and the corresponding (default) nodeData.
        /// </summary>
        private void InitializeGrid()
        {
            grid = new Dictionary<(int, int), Dictionary<string, bool>>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid.Add((x, y), nodeData);
                }
            }
        }
    }
}