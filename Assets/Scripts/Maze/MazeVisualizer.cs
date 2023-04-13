using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mazinator
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class MazeVisualizer : MonoBehaviour
    {
        #region Visualization properties
        [SerializeField] private int cellSize = 2;
        [SerializeField] private int wallSize = 1;
        [SerializeField] private int maxPixels = 900;
        #endregion

        #region Colors
        [SerializeField] private Color cellColor;
        [SerializeField] private Color wallColor;
        [SerializeField] private Color visitedColor;
        #endregion

        private SpriteRenderer spriteRenderer;
        private Dictionary<(int, int), Dictionary<(int, int), Sprite>> previousMazes;

        private void Start()
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            previousMazes = new Dictionary<(int, int), Dictionary<(int, int), Sprite>>();
        }

        /// <summary>
        /// Draws the grid on a texture.
        /// This is 
        /// </summary>
        /// <param name="grid">The grid which will be drawn on the texture/param>
        /// <returns></returns>
        public async void DrawGrid(MazeGrid grid)
        {
            if (previousMazes.ContainsKey((grid.Width, grid.Height)))
            {
                if (previousMazes[(grid.Width, grid.Height)].ContainsKey((cellSize, wallSize)))
                {
                    SetSprite(previousMazes[(grid.Width, grid.Height)][(cellSize, wallSize)]);
                }
                else
                {
                    await DrawBasics(grid, GenerateTexture(grid.Width, grid.Height));
                }
            }
            else
            {
                await DrawBasics(grid, GenerateTexture(grid.Width, grid.Height));
            }
        }

        /// <summary>
        /// Generates a texture based on the grid size and pixel sizes.
        /// </summary>
        /// <param name="width">Width of the grid</param>
        /// <param name="height">Height of the grid</param>
        /// <returns>The newly generated texture</returns>
        private Texture2D GenerateTexture(int width, int height)
        {
            int textureWidth = (width * cellSize) + (width * wallSize + 1);
            int textureHeight = (height * cellSize) + (height * wallSize + 1);
            Texture2D texture = new Texture2D(textureWidth, textureHeight);
            texture.filterMode = FilterMode.Point;
            return texture;
        }

        /// <summary>
        /// Draw the background and cells of the grid.
        /// </summary>
        /// <param name="grid">The grid which will be drawn</param>
        /// <param name="texture">The texture which will be drawn upon</param>
        private async Task DrawBasics(MazeGrid grid, Texture2D texture)
        {
            DrawBackGround(texture);
            await DrawCells(texture, grid);
            SetTexture(texture, grid, false);
            gameObject.transform.localScale = new Vector3(maxPixels = 900 / texture.width, maxPixels = 900 / texture.height, 0f);
        }

        /// <summary>
        /// Draws a rectangle using lines.
        /// </summary>
        /// <param name="texture">The texture which will be drawn upon</param>
        /// <param name="color">The color of the pixels which will be drawn</param>
        /// <param name="xStart">The x starting coordinate</param>
        /// <param name="yStart">The y starting coordinate</param>
        /// <param name="xEnd">The x ending coordinate</param>
        /// <param name="yEnd">The y ending coordinate</param>
        private async Task DrawRectangle(Texture2D texture, Color color, int xStart, int yStart, int xEnd, int yEnd)
        {
            for (int y = yStart; y < yEnd + 1; y++)
            {
                await DrawLine(texture, color, xStart, y, xEnd, y);
            }
            texture.Apply();
        }

        /// <summary>
        /// Draws a line on the texture.
        /// </summary>
        /// <param name="texture">The texture which will be drawn upon</param>
        /// <param name="color">The color of the pixels which will be drawn</param>
        /// <param name="xStart">The x starting coordinate</param>
        /// <param name="yStart">The y starting coordinate</param>
        /// <param name="xEnd">The x ending coordinate</param>
        /// <param name="yEnd">The y ending coordinate</param>
        private async Task DrawLine(Texture2D texture, Color color, int xStart, int yStart, int xEnd, int yEnd)
        {
            for (int y = yStart; y < yEnd + 1; y++)
            {
                for (int x = xStart; x < xEnd + 1; x++)
                {
                    texture.SetPixel(x, y, color);
                    await Task.Yield();
                }
            }
            texture.Apply();
        }

        /// <summary>
        /// Fill the texture with one color.
        /// </summary>
        /// <param name="texture">The texture which will be drawn upon</param>
        private void DrawBackGround(Texture2D texture)
        {
            Color[] colors = new Color[texture.width * texture.height];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = wallColor;
            }
            texture.SetPixels(colors);
            texture.Apply(false);
        }

        /// <summary>
        /// Draw all cells from the grid on a texture.
        /// </summary>
        /// <param name="texture">The texture which will be drawn upon</param>
        /// <param name="grid">The grid which will be drawn</param>
        private async Task DrawCells(Texture2D texture, MazeGrid grid)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                for (int x = 0; x < grid.Width; x++)
                {
                    await DrawRectangle(texture, cellColor, 
                        (wallSize * (x + 1) + x * cellSize), 
                        (wallSize * (y + 1) + y * cellSize), 
                        (wallSize * x + (x + 1) * cellSize), 
                        (wallSize * y + (y + 1) * cellSize));
                }
            }
            texture.Apply();
        }

        /// <summary>
        /// Set the texture on the sprite renderer.
        /// </summary>
        /// <param name="texture">The texture which will be drawn upon</param>
        /// <param name="grid">The grid which will be drawn</param>
        /// <param name="addToDict">Whether the sprite will be added to the previously built sprites</param>
        private void SetTexture(Texture2D texture, MazeGrid grid, bool addToDict)
        {
            Rect rect = new Rect(0.0f, 0.0f, texture.width, texture.height);
            Sprite sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), 100.0f);
            spriteRenderer.sprite = sprite;
            if (addToDict)
            {
                Dictionary<(int, int), Sprite> sizeDictionary = new Dictionary<(int, int), Sprite>() 
                { 
                    { (texture.width, texture.height), sprite }
                };
                previousMazes.Add((grid.Width, grid.Height), sizeDictionary);
            }
        }

        /// <summary>
        /// Set the sprite on the sprite renderer.
        /// </summary>
        /// <param name="sprite">The sprite which will be set</param>
        private void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }
    }
}