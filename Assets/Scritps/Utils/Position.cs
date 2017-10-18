using UnityEngine;

public struct Position
{
    public float X;
    public float Y;

    public Position(float x, float y)
    {
        X = x;
        Y = y;
    }

    public Position(Vector2 v)
    {
        X = v.x;
        Y = v.y;
    }

    public Vector2 ToVector2()
    {
        return new Vector2(X, Y);
    }
}
