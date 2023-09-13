using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinePathDrawer : PathDrawer
{
    [SerializeField] private LinePathDrawer cameraDrawer;
    
    [SerializeField] private List<Vector3> points = new List<Vector3>();
    private Material mat;

    private void OnPostRender()
    {
        if (mat == null)
        {
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            mat = new Material(shader);
            mat.hideFlags = HideFlags.HideAndDontSave;
        }
        mat.SetPass(0);
        GL.Begin(GL.LINES);
        GL.Color(Color.red);

        for (int i = 0; i < points.Count - 1; i++)
        {
            GL.Vertex(points[i]);
            GL.Vertex(points[i+1]);
        }

        GL.End();
    }


    public override void DrawPath(PixelPathData pixelPathData)
    {
        if (cameraDrawer != null)
        {
            cameraDrawer.DrawPath(pixelPathData);
            return;
        }

        points.Clear();
        foreach (var point in pixelPathData.points)
        {
            points.Add((Vector3)(Vector2)point + Vector3.one * 0.5f + Vector3.back * 5);
        }
    }

}
