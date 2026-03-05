using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 _moveInput;
    private float _speed = 5f;
    private Rigidbody2D _rigidBody;

    private PlayerLogic _playerLogic;

    void Awake()
    {
        GameState.PlayerTransform = transform;
        _rigidBody = GetComponent<Rigidbody2D>();
        _playerLogic = GetComponent<PlayerLogic>();
    }

    public void OnMove(InputValue value)
    {
        if (_playerLogic.State == PlayerState.Dead)
        {
            _moveInput = Vector2.zero;
            return;
        }

        _moveInput = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        if (_playerLogic.State == PlayerState.Alive)
        {
            _rigidBody.MovePosition(_rigidBody.position + _moveInput * _speed * Time.fixedDeltaTime);
        }
    }
}