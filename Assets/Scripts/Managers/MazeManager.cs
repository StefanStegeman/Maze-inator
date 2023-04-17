using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

namespace Mazinator
{
    [RequireComponent(typeof(MazeGrid))]
    public class MazeManager : MonoBehaviour
    {
        #region UI Input Fields
        [Header("UI Input Fields")]
        [SerializeField] private TMP_InputField inputWidth;
        [SerializeField] private TMP_InputField inputHeight;
        [SerializeField] private Slider speedSlider;
        [SerializeField] private AlgorithmDropDown algorithmSelector;
        #endregion

        #region Maze
        [Header("Maze")]
        [SerializeField] private GameObject tilemap;
        [SerializeField] private Camera mazeCamera;
        [SerializeField] private int maxSize = 250;
        [SerializeField] private int minSize = 10;
        private MazeGrid grid;
        private Vector3 defaultPosition;
        #endregion

        [Header("Algorithms")]
        #region Algorithms
        [SerializeField] private Dummy dummyAlgorithm;
        private Algorithm currentAlgorithm;
        #endregion

        [Header("Error")]
        #region Error
        [SerializeField] private TMP_Text errorText;
        [SerializeField] private float errorTime;
        #endregion

        private void Start()
        {
            grid = gameObject.GetComponent<MazeGrid>();
            defaultPosition = tilemap.gameObject.transform.position;
            currentAlgorithm = dummyAlgorithm;
        }

        /// <summary>
        /// Reset the maze on button press.
        /// </summary>
        public void ResetMaze()
        {
            currentAlgorithm.Stop();
            grid.Reset();
            mazeCamera.transform.position = defaultPosition;
            tilemap.gameObject.transform.position = defaultPosition;
            ChangeVisualizationSpeed();
        }

        /// <summary>
        /// Generate maze on button press.
        /// </summary>
        public void GenerateMaze()
        {
            currentAlgorithm = algorithmSelector.GetAlgorithm();
            ResetMaze();
            if (int.TryParse(inputWidth.text, out int width) && int.TryParse(inputHeight.text, out int height))
            {
                if (ValidDimensions(width, height))
                {
                    grid.CreateTileMap(width, height);
                    ScaleMaze(width, height);
                    currentAlgorithm.Run(grid);
                }
            }
            else
            {
                StartCoroutine(DisplayError("Specify grid size", errorTime));
            }
        }

        /// <summary>
        /// Display error.
        /// </summary>
        /// <param name="message">the error message</param>
        /// <param name="seconds">amount of seconds the message is being displayed</param>
        public IEnumerator DisplayError(string message, float seconds)
        {
            errorText.text = message;
            yield return new WaitForSeconds(seconds);
            errorText.text = "";
        }

        /// <summary>
        /// Change the speed of the maze visualization on slider value change.
        /// </summary>
        public void ChangeVisualizationSpeed()
        {
            currentAlgorithm.ChangeSpeed(speedSlider.value);
        }


        /// <summary>
        /// Check whether dimensions are valid.
        /// </summary>
        /// <param name="width">user defined width of the grid</param>
        /// <param name="height">user defined height of the grid</param>
        /// <returns>boolean which indicates whethere the dimensions are valid</returns>
        public bool ValidDimensions(int width, int height)
        {
            if (width < minSize || height < minSize)
            {
                string lowestAxis = new[]
                {
                    Tuple.Create(width, "width"),
                    Tuple.Create(height, "height")
                }.Min().Item2;

                StartCoroutine(DisplayError(string.Format("{0} violates the minimum grid size of {1}", lowestAxis, minSize), errorTime));
                return false;
            }

            if (width > maxSize || height > maxSize)
            {
                string highestAxis = new[]
                {
                    Tuple.Create(width, "width"),
                    Tuple.Create(height, "height")
                }.Max().Item2;

                StartCoroutine(DisplayError(string.Format("{0} violates the maxiumum grid size of {1}", highestAxis, maxSize), errorTime));
                return false;
            }
            return true;
        }

        /// <summary>
        /// Set the error textfield for test purposes.
        /// </summary>
        /// <param name="textfield">textfield which will be used</param>
        public void SetErrorText(TMP_Text textfield)
        {
            errorText = textfield;
        }
        
        /// <summary>
        /// Scale the size of the grid to fit the mazeCamera.
        /// </summary>
        /// <param name="width">width of the maze</param>
        /// <param name="height">height of the maze</param>
        private void ScaleMaze(int width, int height)
        {
            float size = width > height ? width : height;
            tilemap.gameObject.transform.position = new Vector3(tilemap.gameObject.transform.position.x - (float)width / 20f, tilemap.gameObject.transform.position.y - (float)height / 20f, 0);
            mazeCamera.transform.position = new Vector3(mazeCamera.transform.position.x, mazeCamera.transform.position.y, mazeCamera.transform.position.z * size / 10);
        }
    }
}