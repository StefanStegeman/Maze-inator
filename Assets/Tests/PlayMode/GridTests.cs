using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

namespace Mazinator
{
    public class GridTests
    {
        [Test]
        public void InitializeEvenGrid()
        {
            int width = 5;
            int height = 5;
            GameObject gridObject = new GameObject();
            gridObject.AddComponent<Tilemap>();

            MazeGrid grid = gridObject.AddComponent<MazeGrid>();
            grid.SetTileMap(gridObject.GetComponent<Tilemap>());
            grid.CreateTileMap(width, height);
            Assert.AreEqual(width * height, grid.Grid.Length);
        }

        [Test]
        public void InitializeUnevenGrid()
        {
            int width = 5;
            int height = 8;
            GameObject gridObject = new GameObject();
            gridObject.AddComponent<Tilemap>();
            
            MazeGrid grid = gridObject.AddComponent<MazeGrid>();
            grid.SetTileMap(gridObject.GetComponent<Tilemap>());
            grid.CreateTileMap(width, height);
            Assert.AreEqual(width * height, grid.Grid.Length);
        }

        [Test]
        public void FlipDirectionNorth()
        {
            GameObject gridObject = new GameObject();
            MazeGrid grid = gridObject.AddComponent<MazeGrid>();
            Assert.AreEqual("south", grid.FlipDirection("north"));
        }

        [Test]
        public void FlipDirectionEast()
        {
            GameObject gridObject = new GameObject();
            MazeGrid grid = gridObject.AddComponent<MazeGrid>();
            Assert.AreEqual("west", grid.FlipDirection("east"));
        }

        [Test]
        public void FlipDirectionSouth()
        {
            GameObject gridObject = new GameObject();
            MazeGrid grid = gridObject.AddComponent<MazeGrid>();
            Assert.AreEqual("north", grid.FlipDirection("south"));
        }

        [Test]
        public void FlipDirectionWest()
        {
            GameObject gridObject = new GameObject();
            MazeGrid grid = gridObject.AddComponent<MazeGrid>();
            Assert.AreEqual("east", grid.FlipDirection("west"));
        }

        [Test]
        public void NorthToCoordinates()
        {
            GameObject gridObject = new GameObject();
            MazeGrid grid = gridObject.AddComponent<MazeGrid>();
            Vector3Int correctCoordinates = new Vector3Int(0, 1, 0);
            Assert.AreEqual(correctCoordinates, grid.DirectionToCoordinates("north", new Vector3Int(0, 0, 0)));
        }

        [Test]
        public void EastToCoordinates()
        {
            GameObject gridObject = new GameObject();
            MazeGrid grid = gridObject.AddComponent<MazeGrid>();
            Vector3Int correctCoordinates = new Vector3Int(1, 0, 0);
            Assert.AreEqual(correctCoordinates, grid.DirectionToCoordinates("east", new Vector3Int(0, 0, 0)));
        }

        [Test]
        public void SouthToCoordinates()
        {
            GameObject gridObject = new GameObject();
            MazeGrid grid = gridObject.AddComponent<MazeGrid>();
            Vector3Int correctCoordinates = new Vector3Int(0, -1, 0);
            Assert.AreEqual(correctCoordinates, grid.DirectionToCoordinates("south", new Vector3Int(0, 0, 0)));
        }

        [Test]
        public void WestToCoordinates()
        {
            GameObject gridObject = new GameObject();
            MazeGrid grid = gridObject.AddComponent<MazeGrid>();
            Vector3Int correctCoordinates = new Vector3Int(-1, 0, 0);
            Assert.AreEqual(correctCoordinates, grid.DirectionToCoordinates("west", new Vector3Int(0, 0, 0)));
        }

        [Test]
        public void IsVisited()
        {
            int width = 3;
            int height = 3;
            GameObject gridObject = new GameObject();
            gridObject.AddComponent<Tilemap>();

            MazeGrid grid = gridObject.AddComponent<MazeGrid>();
            grid.SetTileMap(gridObject.GetComponent<Tilemap>());
            grid.CreateTileMap(width, height);
            
            Vector3Int coordinates = new Vector3Int(0, 0, 0);
            grid.Grid[coordinates.x, coordinates.y].visited = true;
            Assert.AreEqual(1, grid.IsVisited(coordinates));
        }

        [Test]
        public void IsNotVisited()
        {
            int width = 3;
            int height = 3;
            GameObject gridObject = new GameObject();
            gridObject.AddComponent<Tilemap>();

            MazeGrid grid = gridObject.AddComponent<MazeGrid>();
            grid.SetTileMap(gridObject.GetComponent<Tilemap>());
            grid.CreateTileMap(width, height);
            
            Vector3Int coordinates = new Vector3Int(0, 0, 0);
            Assert.AreEqual(0, grid.IsVisited(coordinates));
        }

        [Test]
        public void IsVisitedInvalid()
        {
            
            int width = 3;
            int height = 3;
            GameObject gridObject = new GameObject();
            gridObject.AddComponent<Tilemap>();

            MazeGrid grid = gridObject.AddComponent<MazeGrid>();
            grid.SetTileMap(gridObject.GetComponent<Tilemap>());
            grid.CreateTileMap(width, height);
            
            Vector3Int coordinates = new Vector3Int(-1, -1, 0);
            Assert.AreEqual(-1, grid.IsVisited(coordinates));
        }

        [Test]
        public void GetUnvisitedNeighbours()
        {
            int width = 3;
            int height = 3;
            GameObject gridObject = new GameObject();
            gridObject.AddComponent<Tilemap>();

            MazeGrid grid = gridObject.AddComponent<MazeGrid>();
            grid.SetTileMap(gridObject.GetComponent<Tilemap>());
            grid.CreateTileMap(width, height);
            
            Vector3Int coordinates = new Vector3Int(1, 1, 0);
            List<string> unvisitedNeighbours = new List<string>() { "north", "east", "south", "west"};
            Assert.AreEqual(unvisitedNeighbours, grid.GetUnvisitedNeighbours(coordinates.x, coordinates.y));
        }
    }
}