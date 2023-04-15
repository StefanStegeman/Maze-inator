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
        [SerializeField] private Vector2Int defaultSize;
        private MazeGrid grid;
        #endregion

        #region Algorithms
        DepthFirstSearch dfsAlgorithm;
        #endregion

        private void Start()
        {
            grid = gameObject.GetComponent<MazeGrid>();
            dfsAlgorithm = new DepthFirstSearch();
        }

        public void ResetMaze()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        public void ChangeVisualizationSpeed()
        {
            dfsAlgorithm.ChangeSpeed((int)speedSlider.value);
        }

        /// <summary>
        /// Generates the maze.
        /// </summary>
        public void GenerateMaze()
        {
            ResetMaze();
            if (int.TryParse(inputWidth.text, out int width) && int.TryParse(inputHeight.text, out int height))
            {
                grid.CreateGrid(width, height);
            }
            else
            {
                Debug.Log("I require dimensions");
            }
            dfsAlgorithm.RunAlgorithm(grid);
        }
    }
}