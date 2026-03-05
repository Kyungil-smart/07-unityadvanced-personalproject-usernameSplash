using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStageSceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.LoadScene("GameStageSceneUI", LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
