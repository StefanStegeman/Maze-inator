using NUnit.Framework;
using UnityEngine;

namespace Mazinator
{
    public class GridTests
    {
        // [Test]
        // public void InitializeGrid()
        // {
        //     GameObject obj = new GameObject();
        //     MazeGrid grid = obj.AddComponent<MazeGrid>();
        //     int width = 3;
        //     int height = 3;
        //     grid.CreateGrid(width, height);
        //     Assert.AreEqual(width * height, grid.Grid.Length);
        // }

        // [Test]
        // public void InitializeNodeData()
        // {
        //     MazeGrid grid = new MazeGrid(2, 2);
        //     NodeData nodeData = new NodeData(0, 0);
        //     Assert.AreEqual(nodeData, grid.GetNodeData((0, 0)));
        // }

        // [Test]
        // public void ChangeNodeData()
        // {
        //     MazeGrid grid = new MazeGrid(1, 1);
        //     NodeData node = grid.GetNodeData((0, 0));
        //     node.visited = true;
        //     node.north = true;
        //     grid.SetNodeData(node);
        //     NodeData nodeData = new NodeData(0, 0, true, true, false, false, false);
        //     Assert.AreEqual(nodeData, grid.GetNodeData((0, 0)));
        // }

        // [Test]
        // public void ResetGrid()
        // {
        //     MazeGrid grid = new MazeGrid(1, 1);
        //     NodeData node = grid.GetNodeData((0, 0));
        //     node.visited = true;
        //     node.north = true;
        //     grid.SetNodeData(node);
        //     NodeData nodeData = new NodeData(0, 0);
        //     Assert.AreEqual(nodeData, grid.GetNodeData((0, 0)));
        // }
    }
}