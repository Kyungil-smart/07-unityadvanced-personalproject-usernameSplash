using UnityEngine;

public class Barrage2 : IBarrage
{
    public override void Execute(Vector3 origin)
    {
        BulletPool[] pools = GameState.BulletManagerInstance.Pool;

        int bulletCnt = 20;

        for (int i = 0; i < bulletCnt; ++i)
        {
            Bullet bullet = pools[1].Get();

            BulletContext context = new BulletContext
            {
                position = origin,
                velocity = 5f,
                acceleration = 0f,
                canReflect = true,
                maxReflectNum = 3,
                angularVelocity = 0f,
                degree = (360 / bulletCnt) * i + 15
            };
            Debug.Log(context.degree);
            bullet.Init(context);
        }
    }

}
