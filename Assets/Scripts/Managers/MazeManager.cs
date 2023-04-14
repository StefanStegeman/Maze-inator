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

        private void ResetMaze()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// Generate a grid.
        /// </summary>
        /// <param name="visualize">Whether the grid will be visualized or not</param>
        public void CreateGrid()
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