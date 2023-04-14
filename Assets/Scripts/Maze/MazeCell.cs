using UnityEngine;

namespace Mazinator
{
    public class MazeCell : MonoBehaviour
    {
        public GameObject northWall;
        public GameObject eastWall;
        public GameObject southWall;
        public GameObject westWall;
        public (int, int) Coordinates { get; set; }
        public bool Visited = false;

        public void Start()
        {
            Visited = false;
        }

        public void DisableWall(string direction)
        {   
            switch(direction)
            {
                case "north":
                    northWall.gameObject.SetActive(false);
                    break;
                case "east":
                    eastWall.gameObject.SetActive(false);
                    break;
                case "south":
                    southWall.gameObject.SetActive(false);
                    break;
                case "west":
                    westWall.gameObject.SetActive(false);
                    break;
                default:
                    Debug.Log("This is not a valid direction.");
                    break;
            }
        }

        public void EnableWall(string direction)
        {   
            switch(direction)
            {
                case "north":
                    northWall.gameObject.SetActive(true);
                    break;
                case "east":
                    eastWall.gameObject.SetActive(true);
                    break;
                case "south":
                    southWall.gameObject.SetActive(true);
                    break;
                case "west":
                    westWall.gameObject.SetActive(true);
                    break;
                default:
                    Debug.Log("This is not a valid direction.");
                    break;
            }
        }
    }
}