using TMPro;
using UnityEngine;
using System;

namespace Mazinator
{
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

        public void CreateGrid()
        {
            int width = Int32.Parse(inputWidth.text);
            int height = Int32.Parse(inputHeight.text);
            if (width == 0 || height == 0)
            {
                grid = new MazeGrid(defaultSize.x, defaultSize.y);
            }
            else
            {
                grid = new MazeGrid(width, height);
            }
        }
    }
}