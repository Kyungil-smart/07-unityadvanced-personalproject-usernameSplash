using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;
    private IStage[] _stages;
    private IStage _curStage;

    public void Init()
    {
        Instance = this;
        _stages = new IStage[] { new Stage1() };
    }

    public void SetStage(int stageIndex)
    {
        _curStage = _stages[stageIndex];
        StartCoroutine(_curStage.Run());
    }
}