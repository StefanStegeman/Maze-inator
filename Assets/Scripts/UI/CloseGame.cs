using UnityEngine;

namespace Mazinator
{
    public class CloseGame : MonoBehaviour
    {
        public void Close()
        {
            /// <summary>
            /// Close the game on button press.
            /// </summary>
            Application.Quit();
        }
    }
}