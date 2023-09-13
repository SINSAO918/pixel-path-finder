    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(SpriteRenderer))]
    public class MapGenerator : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private int width;
        private int height;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public SpriteRenderer GetMap(int width, int height)
        {
            this.width = width;
            this.height = height;

            Generate();
            AllWhite();

            return spriteRenderer;
        }

        private void Generate()
        {
            Texture2D texture = new Texture2D(width, height);
            Rect rect = new Rect(0, 0, width, height);
            Vector2 pivot = new Vector2(0, 0);
            Sprite sprite = Sprite.Create(texture, rect, pivot, pixelsPerUnit: 1);
            sprite.texture.filterMode = FilterMode.Point;
            sprite.texture.wrapMode = TextureWrapMode.Clamp;

            spriteRenderer.sprite = sprite;
            sprite.texture.Apply();
        }

        private void AllWhite()
        {
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    spriteRenderer.sprite.texture.SetPixel(w,h,Color.white);
                }
            }
            spriteRenderer.sprite.texture.Apply();
        }
    }
