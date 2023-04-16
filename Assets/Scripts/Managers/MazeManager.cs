using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mazinator
{
    [RequireComponent(typeof(MazeGrid))]
    public class MazeManager : MonoBehaviour
    {
        #region UI Input Fields
        [SerializeField] private TMP_InputField inputWidth;
        [SerializeField] private TMP_InputField inputHeight;
        [SerializeField] private Slider speedSlider;
        #endregion

        #region Maze
        [SerializeField] private int minSize = 10;
        [SerializeField] private int maxSize = 250;
        [SerializeField] private float defaultScale = 0.8f;
        [SerializeField] private float maxScale = 0.031f;
        [SerializeField] private GameObject tilemap;
        private MazeGrid grid;
        [SerializeField] private Camera camera;
        private Vector3 defaultPosition;
        private float scaleFactor;
        #endregion

        #region Algorithms
        public DepthFirstSearch dfsAlgorithm;
        #endregion

        private void Start()
        {
            grid = gameObject.GetComponent<MazeGrid>();
            scaleFactor = (defaultScale - maxScale) / (maxSize - minSize);
            defaultPosition = tilemap.gameObject.transform.position;
            ChangeVisualizationSpeed();
        }

        /// <summary>
        /// Reset the maze on button press.
        /// </summary>
        public void ResetMaze()
        {
            dfsAlgorithm.Stop();
            grid.ResetTileMap();
            grid.visited.Clear();
            camera.transform.position = new Vector3(-350, -10, -1);
            tilemap.gameObject.transform.position = new Vector3(-350, -10, -1);
        }

        /// <summary>
        /// Generate maze on button press.
        /// </summary>
        public void GenerateMaze()
        {
            ResetMaze();
            if (int.TryParse(inputWidth.text, out int width) && int.TryParse(inputHeight.text, out int height))
            {
                grid.CreateTileMap(width, height);
                ScaleMaze(width, height);
            }
            else
            {
                Debug.Log("I require dimensions");
            }
            dfsAlgorithm.RunAlgorithm(ref grid);
        }

        public void ChangeVisualizationSpeed()
        {
            dfsAlgorithm.ChangeSpeed((int)speedSlider.value);
        }

        /// <summary>
        /// Scale the size of the grid to fit the camera.
        /// </summary>
        /// <param name="width">width of the maze</param>
        /// <param name="height">height of the maze</param>
        private void ScaleMaze(int width, int height)
        {
            float size = width > height ? width : height;
            tilemap.gameObject.transform.position = new Vector3(tilemap.gameObject.transform.position.x - (float)width / 20f, tilemap.gameObject.transform.position.y - (float)height / 20f, 0);
            camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z * size / 10);
        }
    }
}