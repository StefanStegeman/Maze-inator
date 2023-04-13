using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Mazinator
{
    public class GridTests
    {
        [Test]
        public void InitializeSize()
        {
            MazeGrid grid = new MazeGrid(2, 2);
            Assert.AreEqual(4, grid.GetGrid().Count);
        }

        [Test]
        public void InitializeNodeData()
        {
            MazeGrid grid = new MazeGrid(2, 2);
            Dictionary<string, bool> nodeData = new Dictionary<string, bool>()
            {
                { "Visited", false },
                { "North", false },
                { "East", false },
                { "South", false },
                { "West", false }
            };
            Assert.AreEqual(nodeData, grid.GetNodeData((0, 0)));
        }

        [Test]
        public void ChangeNodeData()
        {
            MazeGrid grid = new MazeGrid(1, 1);
            grid.ChangeNodeData((0, 0), "Visited", true);
            grid.ChangeNodeData((0, 0), "North", true);
            Dictionary<string, bool> nodeData = new Dictionary<string, bool>()
            {
                { "Visited", true },
                { "North", true },
                { "East", false },
                { "South", false },
                { "West", false }
            };
            Assert.AreEqual(nodeData, grid.GetNodeData((0, 0)));
        }

        [Test]
        public void ResetGrid()
        {
            MazeGrid grid = new MazeGrid(1, 1);
            grid.ChangeNodeData((0, 0), "Visited", true);
            grid.ChangeNodeData((0, 0), "North", true);
            grid.ResetGrid();
            Dictionary<string, bool> nodeData = new Dictionary<string, bool>()
            {
                { "Visited", false },
                { "North", false },
                { "East", false },
                { "South", false },
                { "West", false }
            };
            Debug.Log(grid.GetNodeData((0, 0))["Visited"]);
            Debug.Log(grid.GetNodeData((0, 0))["North"]);
            Assert.AreEqual(nodeData, grid.GetNodeData((0, 0)));
        }
    }
}