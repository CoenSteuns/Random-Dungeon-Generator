using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseGrid : MonoBehaviour {

    public static Grid<float> CreatePerlinNoiseGrid(int width, int height, float scale = 6, bool randomOffset = true, float offsetX =0, float offsetY = 0)
    {
        var grid = new Grid<float>(width, height);

        if (randomOffset)
        {
            offsetX = Random.Range(0, 999);
            offsetY = Random.Range(0, 999);
        }

        grid.ForEachNode((node, x, y) =>
        {
            node = Mathf.PerlinNoise((float)x / grid.Width * scale + offsetX, (float)y / grid.Height * scale + offsetY);
            return node;
        });

        return grid;
    }
}
