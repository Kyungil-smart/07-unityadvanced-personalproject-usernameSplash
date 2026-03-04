using System.Collections;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs; // Todo : 배열(4개)로 관리

    private BulletPool[] _pool; // Todo : 배열(4개)로 관리

    private void Awake()
    {
        _pool = new BulletPool[_prefabs.Length];

        for (int i = 0; i < _prefabs.Length; ++i)
        {
            _pool[i] = new BulletPool(_prefabs[i]);
        }
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        WaitForSeconds w7 = new WaitForSeconds(7f);
        WaitForSeconds w05 = new WaitForSeconds(0.5f);
        while (true)
        {

            //for (int cnt = 0; cnt < 3; ++cnt)
            {
                for (int i = 0; i < 24; ++i)
                {
                    Bullet b = _pool[0].Get();
                    b.Init(transform.position, 6f, 0f, 0f, false, 0, 45 + (i * 15));
                }

                yield return w05;

                for (int i = 0; i < 24; ++i)
                {
                    Bullet b = _pool[1].Get();
                    b.Init(transform.position, 6f, 0f, 0f, true, 3, 45 + (i * 15));
                }

                yield return w05;

                for (int i = 0; i < 24; ++i)
                {
                    Bullet b = _pool[0].Get();
                    b.Init(transform.position, 6f, 0f, 0f, false, 0, 45 + (i * 15));
                }
            }

            yield return w7;
        }
    }
}