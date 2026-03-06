using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;

    public BulletPool[] Pool { get; private set; }

    public void Init()
    {
        Pool = new BulletPool[_prefabs.Length];

        for (int i = 0; i < _prefabs.Length; ++i)
        {
            Pool[i] = new BulletPool(_prefabs[i]);
        }

        GameState.BulletManagerInstance = this;
    }
}