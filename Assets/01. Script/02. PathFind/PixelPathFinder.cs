
    
    using UnityEngine;

    public enum PathFindDirection
    {
        Four,
        Eight
    }

    public abstract class PixelPathFinder : MonoBehaviour
    {
        public PathFindDirection pathFindDirection;
        public bool Dont_Cross_Corners;

        public abstract PixelPathData GetPath(Vector2Int StartPoint, Vector2Int EndPoint);
    }