using System.Collections;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;

    public BulletPool[] Pool { get; private set; }

    private void Awake()
    {
        Pool = new BulletPool[_prefabs.Length];

        for (int i = 0; i < _prefabs.Length; ++i)
        {
            Pool[i] = new BulletPool(_prefabs[i]);
        }

        GameState.BulletManagerInstance = this;
    }

    private void Start()
    {
        StartCoroutine(SpawnCircle());
    }

    IEnumerator SpawnCommon()
    {
        WaitForSeconds w1 = new WaitForSeconds(1.2f);

        CommonBarrage commonBarrage = new CommonBarrage();
        Barrage1 barrage1 = new Barrage1();
        Barrage2 barrage2 = new Barrage2();

        while (true)
        {
            commonBarrage.Execute(transform.position);
            yield return w1;
        }
    }

    IEnumerator SpawnCircle()
    {
        WaitForSeconds w3 = new WaitForSeconds(1.5f);

        Barrage1 barrage1 = new Barrage1();

        for (int i = 0; i < 3; ++i)
        {
            barrage1.Execute(transform.position);
            yield return w3;
        }

        StartCoroutine(SpawnCommon());

        for (int i = 0; i < 3; ++i)
        {
            barrage1.Execute(transform.position);
            yield return w3;
        }

        StartCoroutine(SpawnCircleHarder());
    }

    IEnumerator SpawnCircleHarder()
    {
        WaitForSeconds w7 = new WaitForSeconds(5f);
        WaitForSeconds w05 = new WaitForSeconds(0.5f);

        Barrage1 barrage1 = new Barrage1();
        Barrage2 barrage2 = new Barrage2();

        while (true)
        {
            barrage1.Execute(transform.position);
            yield return w05;
            barrage2.Execute(transform.position);
            yield return w05;
            barrage1.Execute(transform.position);
            yield return w7;
        }
    }
}