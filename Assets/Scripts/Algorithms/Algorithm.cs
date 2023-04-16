using UnityEngine;

namespace Mazinator
{
    public class Algorithm : MonoBehaviour
    {
        protected int miliseconds;

        public Algorithm()
        {
            miliseconds = 100;
        }

        public void ChangeSpeed(int newSpeed)
        {
            miliseconds = newSpeed;
        }
    }
}