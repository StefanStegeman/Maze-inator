using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

namespace Mazinator
{
    public class MazeTileTests
    {
        [Test]
        public void DisableNorthWall()
        {
            GameObject tilemapObject = new GameObject();
            Tilemap tilemap = tilemapObject.AddComponent<Tilemap>();
            Dictionary<string, Tile> tileDictionary = new Dictionary<string, Tile>()
            {
                { "esw", new Tile() },
            };

            MazeTile tile = new MazeTile(new Vector3Int(0, 0, 0), Color.white, Color.white, tileDictionary);
            tile.DisableWall("north", tilemap);
            Assert.AreEqual("esw", tile.walls);
        }

        [Test]
        public void DisableEastWall()
        {
            GameObject tilemapObject = new GameObject();
            Tilemap tilemap = tilemapObject.AddComponent<Tilemap>();
            Dictionary<string, Tile> tileDictionary = new Dictionary<string, Tile>()
            {
                { "nsw", new Tile() },
            };

            MazeTile tile = new MazeTile(new Vector3Int(0, 0, 0), Color.white, Color.white, tileDictionary);
            tile.DisableWall("east", tilemap);
            Assert.AreEqual("nsw", tile.walls);
        }

        [Test]
        public void DisableSouthWall()
        {
            GameObject tilemapObject = new GameObject();
            Tilemap tilemap = tilemapObject.AddComponent<Tilemap>();
            Dictionary<string, Tile> tileDictionary = new Dictionary<string, Tile>()
            {
                { "new", new Tile() },
            };

            MazeTile tile = new MazeTile(new Vector3Int(0, 0, 0), Color.white, Color.white, tileDictionary);
            tile.DisableWall("south", tilemap);
            Assert.AreEqual("new", tile.walls);
        }

        [Test]
        public void DisableWestWall()
        {
            GameObject tilemapObject = new GameObject();
            Tilemap tilemap = tilemapObject.AddComponent<Tilemap>();
            Dictionary<string, Tile> tileDictionary = new Dictionary<string, Tile>()
            {
                { "nes", new Tile() },
            };

            MazeTile tile = new MazeTile(new Vector3Int(0, 0, 0), Color.white, Color.white, tileDictionary);
            tile.DisableWall("west", tilemap);
            Assert.AreEqual("nes", tile.walls);
        }
    }
}