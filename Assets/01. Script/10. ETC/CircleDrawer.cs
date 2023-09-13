using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDrawer : MonoBehaviour
{
    [SerializeField] private int radius;

    private List<Vector2Int> draws = new();

    private void Start()
    {
        SetRadius(radius);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            Vector2Int index = MapEditor.GetMouseIndex();
            if (MapEditor.IsInMap(index.x, index.y))
            {
                foreach (var item in draws)
                {
                    MapController.Instance.SetPixel(index.x + item.x, index.y + item.y,Color.yellow);
                }
            }
        }
    }

    public void SetRadius(int radius)
    {
        draws.Clear();
        this.radius = radius;
        int powRadius = radius * radius;

        for (int h = 0; h < radius; h++)
        {
            for (int w = 0; w < radius; w++)
            {
                if (h * h + w * w <= powRadius)
                {
                    draws.Add(new Vector2Int(w,h));
                    draws.Add(new Vector2Int(-w,h));
                    draws.Add(new Vector2Int(w,-h));
                    draws.Add(new Vector2Int(-w,-h));
                }
            }
        }
    }
}
