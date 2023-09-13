using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapController : MonoSingleton<MapController>
{
    [SerializeField] private MapGenerator mapGenerator;

    [SerializeField] private Color closeColor;
    [SerializeField] private Color openColor;

    private SpriteRenderer spriteRenderer;

    private bool isChanged = false;
    private int width;
    private int height;

    private void Start()
    {
        spriteRenderer = mapGenerator.GetMap(width, height);
    }

    private void Update()
    {
        if (isChanged)
        {
            spriteRenderer.sprite.texture.Apply();
        }

        isChanged = false;
    }

    public void SetPixel(int x, int y, Color color)
    {
        spriteRenderer.sprite.texture.SetPixel(x, y, color);
        isChanged = true;
    }

    public void SetMapSize(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    private List<Vector2Int> closes = new();
    private List<Vector2Int> opens = new();

    public void DisplayOpenPixel(List<Vector2Int> oepn, List<Vector2Int> close)
    {
        foreach (var item in opens)
        {
            BlockState bs = MapDataController.Instance.GetState(item.x, item.y);
            MapDataController.Instance.SetState(item.x, item.y, bs);
        }
        foreach (var item in closes)
        {
            BlockState bs = MapDataController.Instance.GetState(item.x, item.y);
            MapDataController.Instance.SetState(item.x, item.y, bs);
        }

        foreach (var item in close)
        {
            SetPixel(item.x, item.y, closeColor);
        }
        foreach (var item in oepn)
        {
            SetPixel(item.x, item.y, openColor);
        }
        opens = oepn;
        closes = close;
    }

}
