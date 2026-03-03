using UnityEngine;
using UnityEngine.Pool;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab; // Todo : 배열(4개)로 관리

    private ObjectPool<Bullet> _pool; // Todo : 배열(4개)로 관리

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(CreateBullet, OnGetBullet, OnReleaseBullet, OnDestroyBullet, false, 100);
    }

    private Bullet CreateBullet()
    {
        Bullet bullet = Instantiate(prefab).GetComponent<Bullet>();
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
        Destroy(bullet.gameObject);
    }
}