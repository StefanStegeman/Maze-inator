using System.Collections.Generic;
using UnityEngine;

namespace Mazinator
{
    public enum UIType
    {
        GameScreen
    }

    public class CanvasManager : MonoBehaviour
    {
        [SerializeField]
        private List<UIElement> uiElements;
        private UIElement currentUI;
        private UIElement previousUI;

        private void Start()
        {
            uiElements.ForEach(element => element.gameObject.SetActive(false));
            SwitchUIElement(UIType.GameScreen);
        }

        
        /// <summary>
        /// Switch the current UIElement to the preferred new one.
        /// </summary>
        /// <param name="type">New UIType to be activated.</param>
        public void SwitchUIElement(UIType type)
        {
            if (currentUI != null)
            {
                currentUI.gameObject.SetActive(false);
                previousUI = currentUI;
            }
            UIElement element = uiElements.Find(element => element.uiType == type);
            if (element != null)
            {
                element.gameObject.SetActive(true);
                currentUI = element;
            }
        }

        /// <summary>
        /// Revert the current UI back to the previous one.
        /// </summary>
        public void RevertUIElement()
        {
            currentUI.gameObject.SetActive(false);
            previousUI.gameObject.SetActive(true);
            (currentUI, previousUI) = (previousUI, currentUI);
        }
    }
}