using UnityEngine;

public class Barrage1 : IBarrage
{
    public override void Execute(Vector3 origin)
    {
        BulletPool[] pools = GameState.BulletManagerInstance.Pool;

        int bulletCnt = 20;

        for (int i = 0; i < bulletCnt; ++i)
        {
            Bullet bullet = pools[0].Get();

            BulletContext context = new BulletContext
            {
                position = origin,
                velocity = 5f,
                acceleration = 0f,
                canReflect = false,
                maxReflectNum = 0,
                angularVelocity = 0f,
                degree = (360 / bulletCnt) * i + 15
            };

            bullet.Init(context);
        }
    }
}
