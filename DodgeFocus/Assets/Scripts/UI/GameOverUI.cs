using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        _scoreText.text = $"Score:{GameState.PlayerData.Score}";
    }

    public void OnClickQTLButton()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void OnClickRestartButton()
    {
        SceneManager.LoadScene("GameStageScene");
    }
}
