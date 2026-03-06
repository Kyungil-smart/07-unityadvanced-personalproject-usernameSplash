public class CircleEmitter : IEmitter
{
    private int _bulletNum;
    private int _bulletType;

    BulletPool _pool;

    public CircleEmitter(int bulletNum, int bulletType)
    {
        _bulletNum = bulletNum;
        _bulletType = bulletType;

        _pool = GameState.BulletManagerInstance.Pool[bulletType];
    }

    public void Emit(BulletContext context)
    {
        float step = 360.0f / _bulletNum;

        for (int i = 0; i < _bulletNum; ++i)
        {
            float deg = step * i;

            context._degree = deg;

            Bullet bullet = _pool.Get();
            bullet.Init(context);
        }
    }
}