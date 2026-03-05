using TMPro;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _hpText;
    [SerializeField] TextMeshProUGUI _scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _hpText.text = $"HP:{GameState.PlayerData.HP}";
        _scoreText.text = $"Score:{GameState.PlayerData.Score}";
    }
}
