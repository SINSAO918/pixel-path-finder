using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapEditor : MonoBehaviour
{
    [SerializeField] private BlockState lastClickState = BlockState.None;
    private bool mouseDown;

    private void Update()
    {
        SetObstacle();
    }

    private void SetObstacle()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Vector2Int index = GetMouseIndex();
        if (!IsInMap(index.x, index.y)) return;

        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
            lastClickState = GetState(index.x, index.y);
        }
        if (mouseDown)
        {
            BlockState brushState = lastClickState == BlockState.None ? BlockState.Obstacle : BlockState.None;
            MapDataController.Instance.SetState(index.x,index.y,brushState);
        }
        if (Input.GetMouseButtonUp(0)) mouseDown = false;
    }

    public static Vector2Int GetMouseIndex()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2Int((int)mousePosition.x, (int)mousePosition.y);
    }
    public static bool IsInMap(int x, int y)
    {
        return x >= 0 && y >= 00 && x < MapDataController.Instance.width && y < MapDataController.Instance.height;
    }
    private BlockState GetState(int x, int y)
    {
        return MapDataController.Instance.GetState(x, y);
    }
}
