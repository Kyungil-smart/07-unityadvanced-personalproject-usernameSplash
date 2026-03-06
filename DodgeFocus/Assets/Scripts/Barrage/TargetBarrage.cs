
using UnityEngine;

public class TargetBarrage : IBarrage
{
    TargetEmitter _targetEmitter;

    public TargetBarrage()
    {
        _targetEmitter = new TargetEmitter(1, 2);
    }

    public override void Execute(Vector3 origin)
    {
        Vector2 dir = (Vector2)(GameState.PlayerTransform.position - origin).normalized;
        float dirToDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < 4; ++i)
        {
            BulletContext context = new BulletContext(
                origin,
                1f,
                7f,
                0f,
                0f,
                0f,
                0f,
                false,
                0,
                dirToDeg + 90 * i
            );

            _targetEmitter.Emit(context);
        }
    }
}
