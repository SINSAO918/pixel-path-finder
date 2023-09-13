using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPathDrawer : PathDrawer
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private MapDataController mapDataController;

    private PixelPathData lastData;

    public override void DrawPath(PixelPathData pixelPathData)
    {
        ClearPath();
        lastData = pixelPathData;
        foreach (var point in pixelPathData.points)
        {
            spriteRenderer.sprite.texture.SetPixel(point.x, point.y, Color.red);
        }
        spriteRenderer.sprite.texture.Apply();
    }

    public void ClearPath()
    {
        if (lastData == null) return;

        foreach (var point in lastData.points)
        {
            BlockState blockState = mapDataController.GetState(point.x, point.y);
            mapDataController.SetState(point.x, point.y, blockState);
        }
    }
}
