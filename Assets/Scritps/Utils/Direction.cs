using UnityEngine;

public class Direction
{
    public float X;
    public float Y;

    public Direction(float x, float y)
    {
        X = x;
        Y = y;
    }

    public Direction(Vector2 v)
    {
        X = v.x;
        Y = v.y;
    }

    public Vector2 ToVector2()
    {
        return new Vector2(X, Y);
    }
}
