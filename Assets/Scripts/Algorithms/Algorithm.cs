using UnityEngine;

namespace Mazinator
{
    public abstract class Algorithm : MonoBehaviour
    {
        protected float miliseconds = 100f;

        /// <summary>
        /// Change speed of the visualization.
        /// </summary>
        /// <param name="newSpeed">The new speed</param>
        public void ChangeSpeed(float newSpeed)
        {
            miliseconds = newSpeed * 0.001f;
        }

        /// <summary>
        /// Stop the algorithm.
        /// </summary>
        public void Stop()
        {
            StopAllCoroutines();
        }

        /// <summary>
        /// Run the selected algorithm.
        /// </summary>
        public abstract void Run(MazeGrid grid);
    }
}