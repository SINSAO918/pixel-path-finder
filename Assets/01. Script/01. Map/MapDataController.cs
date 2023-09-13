using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class MapDataController : MonoSingleton<MapDataController>
{
    [SerializeField] private MapController mapController;
    [SerializeField] private GridDrawer gridDrawer;
    
    [field: SerializeField] public int width { get; private set; }
    [field: SerializeField] public int height { get; private set; }

    private Color[] blockStateColor = new Color[3] { Color.white, Color.gray, Color.red };

    private Boundary2DArray<BlockState> mapData;

    protected override void Awake()
    {
        base.Awake();
        AwakeSetting();
    }

    public void SetState(int x, int y, BlockState blockState)
    {
        Color color = blockStateColor[(int)blockState];
        mapData[x, y] = blockState;
        mapController.SetPixel(x, y, color);
    }

    public BlockState GetState(int x, int y)
    {
        return mapData[x, y];
    }

    private void AwakeSetting()
    {
        mapData = new Boundary2DArray<BlockState>(width, height, BlockState.NULL);
        mapController.SetMapSize(width, height);
        gridDrawer.SetGridSize(width,height);
    }
}