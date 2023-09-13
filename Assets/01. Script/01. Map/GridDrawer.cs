using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GridDrawer : MonoBehaviour
{
    private Material mat;

    [SerializeField] private int width;
    [SerializeField] private int height;

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
        GL.Color(Color.black);

        for (int w = 0; w < width; w++)
        {
            GL.Vertex(new Vector3(w, 0, -5));
            GL.Vertex(new Vector3(w, height, -5));
        }
        for (int h = 0; h < height; h++)
        {
            GL.Vertex(new Vector3(0, h, -5));
            GL.Vertex(new Vector3(width, h, -5));
        }

        GL.End();
    }

    public void SetGridSize(int width, int height)
    {
        this.width = width;
        this.height = height;
    }
}
