using System;
using System.Collections;
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
    bool _isInvincible = false;

    Animator _playerAnimator;

    public int HP => _hp;
    public int Score => _score;
    public PlayerState State { get; private set; }

    public event Action OnPlayerDeadUI;

    SpriteRenderer playerRenderer;

    private void Awake()
    {
        _hp = 100;
        _score = 0;
        _timer = 0;

        _playerAnimator = GetComponent<Animator>();

        State = PlayerState.Alive;

        GameState.PlayerData = this;

        playerRenderer = gameObject.GetComponent<SpriteRenderer>();
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

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet") && _isInvincible == false)
        {
            _hp -= 5;
            StartCoroutine(HandleInvincible());
        }
    }

    IEnumerator HandleInvincible()
    {
        _isInvincible = true;

        float t = 1.0f;
        while (t > 0)
        {
            t -= 0.1f;

            playerRenderer.enabled = !playerRenderer.enabled;

            yield return new WaitForSeconds(0.1f);
        }

        playerRenderer.enabled = true;
        _isInvincible = false;
    }
}
