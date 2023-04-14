using TMPro;
using UnityEngine;

namespace Mazinator
{
    [RequireComponent(typeof(MazeGrid))]
    public class MazeManager : MonoBehaviour
    {
        #region UI Input Fields
        [SerializeField] private TMP_InputField inputWidth;
        [SerializeField] private TMP_InputField inputHeight;
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

        /// <summary>
        /// Generate a grid.
        /// </summary>
        /// <param name="visualize">Whether the grid will be visualized or not</param>
        public void CreateGrid()
        {
            if (int.TryParse(inputWidth.text, out int width) && int.TryParse(inputHeight.text, out int height))
            {
                grid.CreateGrid(width, height);
            }
            else
            {
                grid.CreateGrid(5, 5);
                Debug.Log("I require dimensions");
            }
            dfsAlgorithm.RunAlgorithm(grid, 0, 0);
        }
    }
}