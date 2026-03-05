using System;
using UnityEngine;
public enum PlayerState
{
    Alive,
    Dead
}

public class PlayerLogic : MonoBehaviour
{

    [SerializeField] int _hp;
    [SerializeField] int _score;

    float _timer;

    Animator _playerAnimator;

    public int HP => _hp;
    public int Score => _score;
    public PlayerState State { get; private set; }

    public event Action OnPlayerDeadUI;

    private void Awake()
    {
        _hp = 100;
        _score = 0;
        _timer = 0;

        _playerAnimator = GetComponent<Animator>();

        State = PlayerState.Alive;

        GameState.PlayerData = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (State == PlayerState.Alive)
        {
            if (_hp <= 0)
            {
                State = PlayerState.Dead;
                _playerAnimator.SetTrigger("IsDead");
                OnPlayerDeadUI?.Invoke();
            }

            _timer += Time.deltaTime;

            if (_timer > 0.2f)
            {
                _timer = 0.0f;
                _score++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (State == PlayerState.Dead)
        {
            return;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            _hp -= 20;
        }
    }
}
