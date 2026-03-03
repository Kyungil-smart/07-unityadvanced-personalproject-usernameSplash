using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public float Velocity { get; private set; }
    public float Acceleration { get; private set; }
    public float AngularVelocity { get; private set; }
    public bool CanReflect { get; private set; }
    public int MaxReflectNum { get; private set; }
    public int ReflectCnt { get; private set; }
    public Vector2 Dir { get; private set; }

    IBulletSteering _steering;
    ObjectPool<Bullet> _pool;

    public void Init(float velocity, float acceleration, float angularVelocity, bool canReflect, int maxReflectNum, Vector2 direction)
    {
        Velocity = velocity;
        Acceleration = acceleration;
        AngularVelocity = angularVelocity;
        CanReflect = canReflect;

        if (CanReflect)
        {
            MaxReflectNum = maxReflectNum;
        }
        else
        {
            MaxReflectNum = 0;
        }

        ReflectCnt = 0;
        Dir = direction;
    }

    public void Init(float velocity, float acceleration, float angularVelocity, bool canReflect, int maxReflectNum, Vector2 direction, IBulletSteering steering)
    {
        Velocity = velocity;
        Acceleration = acceleration;
        AngularVelocity = angularVelocity;
        CanReflect = canReflect;

        if (CanReflect)
        {
            MaxReflectNum = maxReflectNum;
        }
        else
        {
            MaxReflectNum = 0;
        }

        ReflectCnt = 0;
        Dir = direction;

        _steering = steering;
    }

    public void SetManagedPool(ObjectPool<Bullet> pool)
    {
        _pool = pool;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        Dir = _steering.GetNextDirection(Dir, this);
        Velocity = Velocity + Acceleration * dt;
        transform.position += (Vector3)(Dir * Velocity * dt);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (CanReflect == false)
            {
                return;
            }

            if (ReflectCnt >= MaxReflectNum)
            {
                return;
            }

            Vector2 normal = collision.contacts[0].normal;
            Dir = Vector2.Reflect(Dir, normal);
            ReflectCnt++;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            _pool.Release(this);
        }
    }
}
