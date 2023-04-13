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

        private void Start()
        {
            visualizer = gameObject.GetComponent<MazeVisualizer>();
        }

        public void CreateGrid()
        {
            if (int.TryParse(inputWidth.text, out int width) && int.TryParse(inputHeight.text, out int height))
            {
                grid = new MazeGrid(width, height);
            }
            else
            {
                grid = new MazeGrid(defaultSize.x, defaultSize.y);
            }
            visualizer.DrawStartGrid(grid);
        }
    }
}