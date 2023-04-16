using UnityEngine;

namespace Mazinator
{
    public class Algorithm : MonoBehaviour
    {
        protected float miliseconds = 100f;

        /// <summary>
        /// Change speed of the visualization.
        /// </summary>
        /// <param name="newSpeed">The new speed</param>
        public void ChangeSpeed(int newSpeed)
        {
            miliseconds = newSpeed;
        }
    }
}