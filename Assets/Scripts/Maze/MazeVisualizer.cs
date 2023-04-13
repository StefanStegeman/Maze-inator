using UnityEngine;
using UnityEngine.UI;

namespace Mazinator
{
    public class MazeVisualizer : MonoBehaviour
    {
        #region Visualization properties
        [SerializeField] private Image image;
        [SerializeField] private int cellSize;
        [SerializeField] private int wallSize;
        #endregion

        #region Colors
        [SerializeField] private Color cellColor;
        [SerializeField] private Color wallColor;
        [SerializeField] private Color visitedColor;
        #endregion

        public SpriteRenderer spriteRenderer;

        public void DrawStartGrid(MazeGrid grid)
        {
            int textureWidth = (grid.Width * cellSize) + (grid.Width * wallSize + 1);
            int textureHeight = (grid.Height * cellSize) + (grid.Height * wallSize + 1);
            Texture2D texture = new Texture2D(textureWidth, textureHeight);
            DrawBackGround(texture);
            DrawRectangle(texture, cellColor, new Vector2Int(2, 2), new Vector2Int(20, 20));
            SetSprite(texture);
        }

        private void DrawRectangle(Texture2D texture, Color color, Vector2Int start, Vector2Int end)
        {
            for (int y = start.y; y < end.y; y++)
            {
                DrawLine(texture, color, new Vector2Int(start.x, y), new Vector2Int(end.x, y));
            }
            texture.Apply();
        }

        private void DrawLine(Texture2D texture, Color color, Vector2Int start, Vector2Int end)
        {
            for (int y = start.y; y < end.y + 1; y++)
            {
                for (int x = start.x; x < end.x + 1; x++)
                {
                    texture.SetPixel(x, y, color);
                }
            }
            texture.Apply();
        }

        private void DrawBackGround(Texture2D texture)
        {
            for (int y = 0; y < texture.height; y++)
            {
                for (int x = 0; x < texture.width; x++)
                {
                    texture.SetPixel(x, y, Color.black);
                }
            }
            texture.Apply();
        }

        // private void DrawCells(Texture2D texture, MazeGrid grid)
        // {
        //     for (int y = 0; y < grid.Height; y++)
        //     {
        //         for (int x = 0; x < grid.Width; x++)
        //         {
        //             texture.SetPixel(wallSize + )
        //         }
        //     }
        // }

        private void SetSprite(Texture2D texture)
        {
            Rect rect = new Rect(0.0f, 0.0f, texture.width, texture.height);
            image.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), 100.0f);
            spriteRenderer.sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), 100.0f);
        }
    }
}