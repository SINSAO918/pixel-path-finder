using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFindAgent : MonoBehaviour
{
    public PathDrawer pathDrawer;
    public PixelPathFinder pathFinder;

    [Space]
    public GameObject startMark;
    public GameObject endMark;

    private void Awake()
    {
        startMark.SetActive(false);
        endMark.SetActive(false);
    }

    private void Update()
    {
        DeposetMark();
    }

    public void Find()
    {
        if (!startMark.activeSelf || !endMark.activeSelf) return;
        Vector2Int start = new Vector2Int((int)startMark.transform.position.x, (int)startMark.transform.position.y);
        Vector2Int end = new Vector2Int((int)endMark.transform.position.x, (int)endMark.transform.position.y);

        Utill.Timer.StartTimer("PathFindingStart");

        PixelPathData data = pathFinder.GetPath(start, end);
        if (data != null)
        {
            pathDrawer.DrawPath(data);

            PathfindInfomationUI.Instance.SetTime(Utill.Timer.GetTimer("PathFindingStart"));
        }
        else
        {
            //Debug.Log("길 찾기 실패");
        }
    }

    private void DeposetMark()
    {
        Vector2Int index = GetMouseIndex();
        if (!IsInMap(index.x, index.y)) return;

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!startMark.activeSelf)
                startMark.SetActive(true);

            startMark.transform.position = index + Vector2.one * 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!endMark.activeSelf)
                endMark.SetActive(true);

            endMark.transform.position = index + Vector2.one * 0.5f;
        }
    }

    private Vector2Int GetMouseIndex()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2Int((int)mousePosition.x, (int)mousePosition.y);
    }
    private bool IsInMap(int x, int y)
    {
        return x >= 0 && y >= 00 && x < MapDataController.Instance.width && y < MapDataController.Instance.height;
    }
}
