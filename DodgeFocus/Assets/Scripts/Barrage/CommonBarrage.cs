
using UnityEngine;

public class CommonBarrage : IBarrage
{
    public override void Execute(Vector3 origin)
    {
        BulletPool[] pools = GameState.BulletManagerInstance.Pool;


        Vector2 dir = (Vector2)(GameState.PlayerTransform.position - origin).normalized;
        float dirToDeg = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < 4; ++i)
        {
            Bullet straightBullet = pools[2].Get();

            BulletContext context = new BulletContext
            {
                position = origin,
                velocity = 1f,
                acceleration = 7f,
                canReflect = false,
                maxReflectNum = 0,
                angularVelocity = 0f,
                degree = dirToDeg + 90 * i
            };

            straightBullet.Init(context);
        }
    }
}
