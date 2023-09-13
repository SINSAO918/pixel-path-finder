using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node
{
    public Node parentNode;

    public Vector2Int index;

    public int x, y;

    public int G = 0;
    public int H = 0;
    public int F => G + H;
}

public class AStar : PixelPathFinder
{
    private MapDataController dataController;
    private Node maxNode = new() { G = int.MaxValue };

    private Node[,] nodes;
    private HashSet<Node> open = new();
    private HashSet<Node> close = new();
    private Node current;

    private Vector2Int startPoint, endPoint;

    private void Start()
    {
        dataController = MapDataController.Instance;
    }

    public override PixelPathData GetPath(Vector2Int startPoint, Vector2Int endPoint)
    {
        InitSetting(startPoint, endPoint);

        while (open.Count > 0)
        {
            GetCurrent();

            if (IsFindPath(out PixelPathData path))
            {
                OnFind();
                return path;
            }

            AddOpenDirect(current.x + 1, current.y);
            AddOpenDirect(current.x - 1, current.y);
            AddOpenDirect(current.x, current.y + 1);
            AddOpenDirect(current.x, current.y - 1);

            if (pathFindDirection == PathFindDirection.Eight)
            {
                AddOpenDiagonal(current.x + 1, current.y + 1);
                AddOpenDiagonal(current.x + 1, current.y - 1);
                AddOpenDiagonal(current.x - 1, current.y + 1);
                AddOpenDiagonal(current.x - 1, current.y - 1);
            }
        }
        return new();
    }

    private void AddOpenDirect(int x, int y)
    {
        if (dataController.GetState(x, y) != BlockState.None) return;
        Node node = nodes[x, y];

        if (node != null)
        {
            if (close.Contains(node)) return;

            if (node.G >= current.G + 10)
            {
                node.G = current.G + 10;
                node.parentNode = current;
            }
        }
        else
        {
            open.Add(CreateNode(x, y, current.G + 10, current));
        }
    }
    private void AddOpenDiagonal(int x, int y)
    {
        if (dataController.GetState(x, y) != BlockState.None) return;

        if (Dont_Cross_Corners)
        {
            if (dataController.GetState(current.x, y) != BlockState.None ||
                dataController.GetState(x, current.y) != BlockState.None) return;
        }
        else
        {
            if (dataController.GetState(current.x, y) != BlockState.None &&
                dataController.GetState(x, current.y) != BlockState.None) return;
        }

        Node node = nodes[x, y];
        if (node != null)
        {
            if (close.Contains(node)) return;

            if (node.G >= current.G + 14)
            {
                node.G = current.G + 14;
                node.parentNode = current;
            }
        }
        else
        {
            open.Add(CreateNode(x, y, current.G + 14, current));
        }
    }

    private bool IsFindPath(out PixelPathData pixelPathData)
    {
        bool isFind = current.index == endPoint;

        if (isFind)
        {
            pixelPathData = new PixelPathData();

            while (current.parentNode != null)
            {
                pixelPathData.points.Add(new Vector2Int(current.x, current.y));
                current = current.parentNode;
            }
            pixelPathData.points.Add(startPoint);
        }
        else
        {
            pixelPathData = null;
        }

        return isFind;
    }
    private void OnFind()
    {
        MapController.Instance.DisplayOpenPixel(open.Extraction((node) => node.index), close.Extraction((node) => node.index));
        PathfindInfomationUI.Instance.SetCount(close.Count + open.Count);
    }

    private void InitSetting(Vector2Int startPoint, Vector2Int endPoint)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        nodes = new Node[dataController.width, dataController.height];
        open = new() { CreateNode(startPoint.x, startPoint.y, 0, null) };
        close = new();
        current = null;
    }
    private Node CreateNode(int x, int y, int g, Node parent)
    {
        Node node = new Node() { x = x, y = y, G = g, parentNode = parent, index = new Vector2Int(x, y) };
        node.H = GetHuristick(x, y);
        nodes[x, y] = node;
        return node;
    }
    private int GetHuristick(int x, int y)
    {
        x = Mathf.Abs(endPoint.x - x);
        y = Mathf.Abs(endPoint.y - y);
        int min = Mathf.Min(x, y);
        int max = Mathf.Max(x, y);
        return min * 14 + (max - min) * 10;
    }
    private void GetCurrent()
    {
        current = maxNode;
        foreach (var item in open)
        {
            if (current.F >= item.F)
            {
                current = item;
            }
        }
        open.Remove(current);
        close.Add(current);
    }
}