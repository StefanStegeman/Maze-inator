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
        [SerializeField] private GameObject cellObjectPool;
        #endregion

        #region Algorithms
        public DepthFirstSearch dfsAlgorithm;
        #endregion

        private void Start()
        {
            grid = gameObject.GetComponent<MazeGrid>();
            // dfsAlgorithm = new DepthFirstSearch();
        }

        public void ResetTileMap()
        {
            grid.ResetTileMap();
            grid.visited.Clear();
        }

        public void GenerateTileMaze()
        {
            ResetTileMap();
            if (int.TryParse(inputWidth.text, out int width) && int.TryParse(inputHeight.text, out int height))
            {
                grid.CreateTileMap(width, height);
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
    }
}