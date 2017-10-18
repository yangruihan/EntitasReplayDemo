using UnityEngine;

public class PlayerViewBehaviour : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 pos)
    {
        _rigidbody2D.MovePosition(pos);
    }
}
