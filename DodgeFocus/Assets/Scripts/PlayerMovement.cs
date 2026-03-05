using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 _moveInput;
    private float _speed = 5f;
    private Rigidbody2D _rigidBody;

    void Awake()
    {
        GameState.PlayerTransform = transform;
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        _rigidBody.MovePosition(_rigidBody.position + _moveInput * _speed * Time.fixedDeltaTime);
    }
}