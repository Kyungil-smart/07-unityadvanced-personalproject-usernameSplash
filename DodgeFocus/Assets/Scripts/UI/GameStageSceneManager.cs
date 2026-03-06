using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStageSceneManager : MonoBehaviour
{
    [SerializeField] BulletManager _bulletManager;
    [SerializeField] StageManager _stageManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _bulletManager.Init();

        _stageManager.Init();
        _stageManager.SetStage(0);

        SceneManager.LoadScene("GameStageSceneUI", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
