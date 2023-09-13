using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JPS : PixelPathFinder
{
    [SerializeField] private MapDataController dataController;

    public override PixelPathData GetPath(Vector2Int StartPoint, Vector2Int EndPoint)
    {
        PixelPathData pixelPathData = new();

        return pixelPathData;
    }

    private BlockState GetPixel(int x, int y) => dataController.GetState(x, y);
}
