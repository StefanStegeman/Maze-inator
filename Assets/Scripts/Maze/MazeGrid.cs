using System.Collections.Generic;

namespace Mazinator
{
    public struct NodeData
    {
        public (int, int) parent;
        public bool visited;
        public bool north;
        public bool east;
        public bool south;
        public bool west;

        public NodeData(int x, int y)
        {
            parent = (x, y);
            visited = false;
            north = false;
            east = false;
            south = false;
            west = false;
        }

        public NodeData(int x, int y, bool visited, bool north, bool east, bool south, bool west)
        {
            parent = (x, y);
            this.visited = visited;
            this.north = north;
            this.east = east;
            this.south = south;
            this.west = west;
        }
    }

    [System.Serializable]
    public class MazeGrid
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        private Dictionary<(int, int), NodeData> grid;

        public MazeGrid(int width, int height)
        {
            Width = width;
            Height = height;
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
            Width = width;
            Height = height;
            InitializeGrid();
        }

        /// <summary>
        /// Get the grid dictionary.
        /// </summary>
        /// <returns>The grid dictionary</returns>
        public Dictionary<(int, int), NodeData> GetGrid()
        {
            return grid;
        }

        /// <summary>
        /// Gets nodeData from a specific element in the grid dictionary.
        /// </summary>
        /// <param name="coordinates">Coordinates of the requested element.</param>
        /// <returns>nodeData dictionary of requested element</returns>
        public NodeData GetNodeData((int, int) coordinates)
        {
            return grid[coordinates];
        }

        public void SetNodeData(NodeData nodeData)
        {
            grid[nodeData.parent] = nodeData;
        }

        /// <summary>
        /// Initialize the grid dictionary with all coordinates and the corresponding (default) nodeData.
        /// The nodeData will be cloned to prevent assigning it by reference.
        /// </summary>
        private void InitializeGrid()
        {
            grid = new Dictionary<(int, int), NodeData>();
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    grid.Add((x, y), new NodeData(x, y));
                }
            }
        }
    }
}