using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePainter : MonoBehaviour
{
    public Tile nesw;
    public Vector3Int position;
    public Tilemap tilemap;
    
    [ContextMenu("Paint")]

    public void Paint()
    {
        Debug.Log("start");
        tilemap.size = new Vector3Int(250, 250, 0);
        for (int y = 0; y < 250; y++)
        {
            for (int x = 0; x < 250; x++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), nesw);
            }
        }
        Debug.Log("end");
    }
}
