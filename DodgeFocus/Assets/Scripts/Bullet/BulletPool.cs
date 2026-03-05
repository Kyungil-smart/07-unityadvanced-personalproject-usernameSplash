using UnityEngine;
using UnityEngine.Pool;

public class BulletPool
{
    private GameObject _prefab;
    private ObjectPool<Bullet> _pool;
    public BulletPool(GameObject prefab)
    {
        _prefab = prefab;

        _pool = new ObjectPool<Bullet>(
            CreateBullet,
            OnGetBullet,
            OnReleaseBullet,
            OnDestroyBullet,
            false, 20
        );
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = GameObject.Instantiate(_prefab).GetComponent<Bullet>();
        bullet.SetManagedPool(_pool);
        return bullet;
    }

    private void OnGetBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnReleaseBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        GameObject.Destroy(bullet.gameObject);
    }

    public Bullet Get()
    {
        return _pool.Get();
    }

    public void Release(Bullet bullet)
    {
        _pool.Release(bullet);
    }
}