using UnityEngine;

public class GameStageUIManager : MonoBehaviour
{
    [SerializeField] Canvas _statusUI;
    [SerializeField] Canvas _gameoverUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        GameState.PlayerData.OnPlayerDeadUI += OnPlayerDead;
    }

    private void OnDisable()
    {
        GameState.PlayerData.OnPlayerDeadUI -= OnPlayerDead;
    }

    void OnPlayerDead()
    {
        _statusUI.gameObject.SetActive(false);
        _gameoverUI.gameObject.SetActive(true);
    }

}
