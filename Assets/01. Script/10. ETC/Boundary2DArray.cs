public class Boundary2DArray<T>
{
    private int width;
    private int height;

    private T[,] arr;

    public T this[int w, int h]
    {
        get => arr[w + 1, h + 1];
        set => arr[w + 1, h + 1] = value;
    }

    private void SetArr(T BoundaryValue)
    {
        arr = new T[width + 2, height + 2];

        for (int w = 0; w < width + 2; w++)
        {
            arr[w, 0] = BoundaryValue;
            arr[w, height + 1] = BoundaryValue;
        }
        for (int h = 0; h < height + 2; h++)
        {
            arr[0, h] = BoundaryValue;
            arr[width + 1, h] = BoundaryValue;
        }
    }

    public Boundary2DArray(int width, int height, T BoundaryValue)
    {
        this.width = width;
        this.height = height;
        SetArr(BoundaryValue);
    }
}