using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Mazinator
{
    public class AlgorithmDropDown : MonoBehaviour
    {
        [SerializeField] private List<Algorithm> algorithms;
        private TMP_Dropdown dropdown;

        private void Start()
        {
            dropdown = gameObject.GetComponent<TMP_Dropdown>();
            PopulateDropdown();
        }

        /// <summary>
        /// Populate the dropdown with all algorithms.
        /// </summary>
        private void PopulateDropdown()
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(algorithms.ConvertAll(algorithm => algorithm.gameObject.name));
        }

        /// <summary>
        /// Get selected algorithm.
        /// </summary>
        /// <returns>selected algorithm</returns>
        public Algorithm GetAlgorithm()
        {
            return algorithms[dropdown.value];
        }
    }
}