
using System.Collections;
using UnityEngine;

public class Stage1 : IStage
{
    CircleBarrage1 _circle1;
    CircleBarrage2 _circle2;
    CurveBarrage1 _curve1;
    HomingBarrage1 _homingBarrage1;
    TargetBarrage _targetBarrage;

    public Stage1()
    {
        _circle1 = new CircleBarrage1();
        _circle2 = new CircleBarrage2();
        _curve1 = new CurveBarrage1();
        _homingBarrage1 = new HomingBarrage1();
        _targetBarrage = new TargetBarrage();
    }
    public IEnumerator Run()
    {
        StageManager.Instance.StartCoroutine(TargetRoutine());
        StageManager.Instance.StartCoroutine(HomingRoutine());
        yield return new WaitForSeconds(5.0f);
        StageManager.Instance.StartCoroutine(CurveRoutine());
        yield return new WaitForSeconds(15.0f);
        StageManager.Instance.StartCoroutine(CircleRoutine());
    }

    IEnumerator CurveRoutine()
    {
        while (true)
        {
            _curve1.Execute(GameState.Center);
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator CircleRoutine()
    {
        for (int i = 0; i < 20; ++i)
        {
            _circle1.Execute(GameState.Center);
            yield return new WaitForSeconds(1.5f);
        }

        while (true)
        {
            _circle1.Execute(GameState.Center);
            yield return new WaitForSeconds(0.5f);
            _circle2.Execute(GameState.Center);
            yield return new WaitForSeconds(0.5f);
            _circle1.Execute(GameState.Center);
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator TargetRoutine()
    {
        while (true)
        {
            _targetBarrage.Execute(GameState.Center);
            yield return new WaitForSeconds(1.2f);
        }
    }

    IEnumerator HomingRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(15.0f);
            _homingBarrage1.Execute(GameState.Center);
        }
    }
}