
public class TargetEmitter : IEmitter
{
    int _bulletNum;
    int _bulletType;

    BulletPool _pool;

    public TargetEmitter(int bulletNum, int bulletType)
    {
        _bulletNum = bulletNum;
        _bulletType = bulletType;

        _pool = GameState.BulletManagerInstance.Pool[bulletType];
    }

    public void Emit(BulletContext context)
    {
        Bullet bullet = _pool.Get();
        bullet.Init(context);
    }
}