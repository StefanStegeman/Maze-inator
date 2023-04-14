using TMPro;
using UnityEngine;

namespace Mazinator
{
    [RequireComponent(typeof(MazeVisualizer))]
    public class MazeManager : MonoBehaviour
    {
        #region UI Input Fields
        [SerializeField] private TMP_InputField inputWidth;
        [SerializeField] private TMP_InputField inputHeight;
        #endregion

        #region Maze
        [SerializeField] private Vector2Int defaultSize;
        private MazeGrid grid;
        private MazeVisualizer visualizer;
        #endregion

        #region Algorithms
        DepthFirstSearch dfsAlgorithm;
        #endregion

        private void Start()
        {
            visualizer = gameObject.GetComponent<MazeVisualizer>();
            dfsAlgorithm = new DepthFirstSearch();
        }

        /// <summary>
        /// Generate a grid.
        /// </summary>
        /// <param name="visualize">Whether the grid will be visualized or not</param>
        public void CreateGrid(bool visualize)
        {
            if (int.TryParse(inputWidth.text, out int width) && int.TryParse(inputHeight.text, out int height))
            {
                grid = new MazeGrid(width, height);
            }
            else
            {
                grid = new MazeGrid(defaultSize.x, defaultSize.y);
            }
            dfsAlgorithm.Run(grid);
            if (visualize)
            {
                visualizer.DrawGrid(grid);
            }
        }
    }
}