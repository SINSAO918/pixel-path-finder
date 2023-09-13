    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using System.Linq;

    public class Dijkstra : PixelPathFinder
    {
        private MapDataController mapData;
        private Queue<Vector2Int> openQueue;
        private Dictionary<Vector2Int, Vector2Int> closeDict;

        private Vector2Int startPoint, endPoint;

        public override PixelPathData GetPath(Vector2Int startPoint, Vector2Int endPoint)
        {
            InitSetting(startPoint, endPoint);
            AddPoint(startPoint, startPoint);

            while (openQueue.Count != 0)
            {
                Vector2Int current = openQueue.Dequeue();

                if (IsFindPath(current, out PixelPathData path))
                {
                    OnFind();
                    return path;
                }

                if (pathFindDirection == PathFindDirection.Eight)
                {
                    FindEightDirection(current, Vector2Int.right, Vector2Int.up);
                    FindEightDirection(current, Vector2Int.left, Vector2Int.up);
                    FindEightDirection(current, Vector2Int.right, Vector2Int.down);
                    FindEightDirection(current, Vector2Int.left, Vector2Int.down);
                }
                DirectSearch(current, Vector2Int.right);
                DirectSearch(current, Vector2Int.left);
                DirectSearch(current, Vector2Int.up);
                DirectSearch(current, Vector2Int.down);
            }
            return new();
        }

        private void FindEightDirection(Vector2Int index, Vector2Int hDirection, Vector2Int vDirection)
        {
            Vector2Int current = index + hDirection + vDirection;

            if (CanDiagonalAdd(index, hDirection, vDirection))
            {
                AddPoint(current, index);
            }
        }
        private void DirectSearch(Vector2Int index, Vector2Int direction)
        {
            Vector2Int current = index + direction;
            if (CanAdd(current))
                AddPoint(current, index);
        }

        private bool CanAdd(Vector2Int current)
        {
            return mapData.GetState(current.x, current.y) == BlockState.None && !closeDict.ContainsKey(current);
        }
        private bool CanDiagonalAdd(Vector2Int index, Vector2Int horizontal, Vector2Int vertical)
        {
            if (Dont_Cross_Corners)
            {
                return CanAdd(index + horizontal) && CanAdd(index + vertical) && CanAdd(index + horizontal + vertical);
            }
            else
            {
                if (!CanAdd(index + horizontal + vertical)) return false;

                if (closeDict.ContainsKey(index + horizontal) || closeDict.ContainsKey(index + vertical)) return false;

                return (mapData.GetState(index.x + horizontal.x, index.y) == BlockState.None || mapData.GetState(index.x, index.y + vertical.y) == BlockState.None);
            }
        }

        private bool IsFindPath(Vector2Int index, out PixelPathData path)
        {
            bool isFind = index == endPoint;

            if (isFind)
            {
                path = ExtractionPath(index);
            }
            else
            {
                path = null;
            }

            return isFind;
        }
        private void OnFind()
        {
            MapController.Instance.DisplayOpenPixel(openQueue.ToList(), closeDict.Extraction((dict) => dict.Key));
            PathfindInfomationUI.Instance.SetCount(closeDict.Count);
        }

        private void InitSetting(Vector2Int startPoint, Vector2Int endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
            mapData = MapDataController.Instance;
            openQueue = new();
            closeDict = new();
        }
        private void AddPoint(Vector2Int current, Vector2Int past)
        {
            openQueue.Enqueue(current);
            closeDict.Add(current, past);
        }
        private PixelPathData ExtractionPath(Vector2Int index)
        {
            Vector2Int currentIndex = index;
            PixelPathData pixelPathData = new();

            while (true)
            {
                pixelPathData.points.Add(currentIndex);

                if (currentIndex == closeDict[currentIndex])
                    return pixelPathData;

                currentIndex = closeDict[currentIndex];
            }
        }
    }