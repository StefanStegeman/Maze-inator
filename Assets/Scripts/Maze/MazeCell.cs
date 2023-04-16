using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

namespace Mazinator
{
    public struct MazeTile
    {
        public Vector3Int coordinates;
        public bool visited;
        public string walls;

        private Color startingColor;
        private Color finalColor;
        private Dictionary<string, Tile> tileDictionary;

        public MazeTile(Vector3Int coordinates, Color startingColor, Color finalColor, Dictionary<string, Tile> tileDictionary)
        {
            this.coordinates = coordinates;
            this.startingColor = startingColor;
            this.finalColor = finalColor;
            this.tileDictionary = tileDictionary;
            visited = false;
            walls = "nesw";
        }

        public void ResetTile(Tilemap tilemap)
        {
            ChangeTile(tileDictionary["tile"], tilemap);
        }

        public void Hoi()
        {
            Debug.Log(walls);
        }

        public void DisableWall(string direction, Tilemap tilemap)
        {
            walls = walls.Replace(direction.Substring(0, 1), "");
            switch (walls)
            {
                case "nsw":
                    ChangeTile(tileDictionary["nsw"], tilemap);
                    break;
                case "new":
                    ChangeTile(tileDictionary["new"], tilemap);
                    break;
                case "nes":
                    ChangeTile(tileDictionary["nes"], tilemap);
                    break;
                case "esw":
                    ChangeTile(tileDictionary["esw"], tilemap);
                    break;
                case "ns":
                    ChangeTile(tileDictionary["ns"], tilemap);
                    break;
                case "ew":
                    ChangeTile(tileDictionary["ew"], tilemap);
                    break;    
                case "ne":
                    ChangeTile(tileDictionary["ne"], tilemap);
                    break;  
                case "nw":
                    ChangeTile(tileDictionary["nw"], tilemap);
                    break;  
                case "es":
                    ChangeTile(tileDictionary["es"], tilemap);
                    break;  
                case "sw":
                    ChangeTile(tileDictionary["sw"], tilemap);
                    break;  
                case "n":
                    ChangeTile(tileDictionary["n"], tilemap);
                    break;
                case "e":
                    ChangeTile(tileDictionary["e"], tilemap);
                    break;
                case "s":
                    ChangeTile(tileDictionary["s"], tilemap);
                    break;
                case "w":
                    ChangeTile(tileDictionary["w"], tilemap);
                    break;
            }
        }

        private void ChangeTile(Tile tile, Tilemap tilemap)
        {
            tilemap.SetTile(coordinates, tile);
        }
    }
}