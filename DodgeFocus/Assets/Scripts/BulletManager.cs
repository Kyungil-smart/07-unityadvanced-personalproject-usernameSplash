using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject[] prefab; // Todo : 배열(4개)로 관리

    private ObjectPool<Bullet>[] _pool; // Todo : 배열(4개)로 관리

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>[prefab.Length];

        for (int i = 0; i < prefab.Length; ++i)
        {
            int index = i;
            _pool[index] = new ObjectPool<Bullet>(
                () => CreateBullet(index),
                OnGetBullet,
                OnReleaseBullet,
                OnDestroyBullet,
                false, 100
            );
        }
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        WaitForSeconds w = new WaitForSeconds(5f);

        while (true)
        {
            Bullet b = _pool[0].Get();
            b.Init(transform.position, 10f, 0f, 0f, true, 3, new Vector2(1, 1).normalized);

            yield return w;
        }
    }

    private Bullet CreateBullet(int index)
    {
        Bullet bullet = Instantiate(prefab[index]).GetComponent<Bullet>();
        bullet.SetManagedPool(_pool[index]);
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