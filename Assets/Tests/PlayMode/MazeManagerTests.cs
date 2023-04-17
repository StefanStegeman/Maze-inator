using NUnit.Framework;
using UnityEngine;
using TMPro;

namespace Mazinator
{
    public class MazeManagerTests
    {
        [Test]
        public void ValidDimensions()
        {
            GameObject mazeManagerObject = new GameObject();
            MazeManager mazeManager = mazeManagerObject.AddComponent<MazeManager>();
            int width = 15;
            int height = 20;
            Assert.AreEqual(true, mazeManager.ValidDimensions(width, height));
        }

        [Test]
        public void InvalidWidthDimensionsMin()
        {
            GameObject mazeManagerObject = new GameObject();
            MazeManager mazeManager = mazeManagerObject.AddComponent<MazeManager>();
            mazeManager.SetErrorText(mazeManagerObject.AddComponent<TextMeshPro>());
            int width = 8;
            int height = 20;
            Assert.AreEqual(false, mazeManager.ValidDimensions(width, height));
        }

        [Test]
        public void InvalidHeightDimensionsMin()
        {
            GameObject mazeManagerObject = new GameObject();
            MazeManager mazeManager = mazeManagerObject.AddComponent<MazeManager>();
            mazeManager.SetErrorText(mazeManagerObject.AddComponent<TextMeshPro>());
            int width = 64;
            int height = 2;
            Assert.AreEqual(false, mazeManager.ValidDimensions(width, height));
        }

        [Test]
        public void InvalidWidthDimensionsMax()
        {
            GameObject mazeManagerObject = new GameObject();
            MazeManager mazeManager = mazeManagerObject.AddComponent<MazeManager>();
            mazeManager.SetErrorText(mazeManagerObject.AddComponent<TextMeshPro>());
            int width = 800;
            int height = 23;
            Assert.AreEqual(false, mazeManager.ValidDimensions(width, height));
        }

        [Test]
        public void InvalidHeightDimensionsMax()
        {
            GameObject mazeManagerObject = new GameObject();
            mazeManagerObject.AddComponent<MazeManager>();
            MazeManager mazeManager = mazeManagerObject.GetComponent<MazeManager>();
            // MazeManager mazeManager = mazeManagerObject.AddComponent<MazeManager>();
            mazeManager.SetErrorText(mazeManagerObject.AddComponent<TextMeshPro>());
            int width = 64;
            int height = 283;
            Assert.AreEqual(false, mazeManager.ValidDimensions(width, height));
        }
    }
}