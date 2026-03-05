using UnityEngine;
using UnityEngine.Pool;

public struct BulletContext
{
    public Vector3 position;
    public float velocity;
    public float acceleration;
    public float angularVelocity;
    public bool canReflect;
    public int maxReflectNum;
    public float degree;
}

public class Bullet : MonoBehaviour
{
    private bool _isInitialized = false;

    public float Velocity { get; private set; }
    public float Acceleration { get; private set; }
    public float AngularVelocity { get; private set; }
    public bool CanReflect { get; private set; }
    public int MaxReflectNum { get; private set; }
    public int ReflectCnt { get; private set; }
    public Vector2 Dir { get; private set; }

    IBulletSteering _steering;
    ObjectPool<Bullet> _pool;

    public void Init(BulletContext context)
    {
        transform.position = context.position;

        Velocity = context.velocity;
        Acceleration = context.acceleration;
        AngularVelocity = context.angularVelocity;
        CanReflect = context.canReflect;

        if (CanReflect)
        {
            MaxReflectNum = context.maxReflectNum;
        }
        else
        {
            MaxReflectNum = 0;
        }

        ReflectCnt = 0;

        float rad = context.degree * Mathf.Deg2Rad;
        Dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Dir = Dir.normalized;

        transform.up = Dir;

        _steering = new ConstantSteering();

        _isInitialized = true;
    }

    public void Init(BulletContext context, IBulletSteering steering)
    {
        transform.position = context.position;

        Velocity = context.velocity;
        Acceleration = context.acceleration;
        AngularVelocity = context.angularVelocity;
        CanReflect = context.canReflect;

        if (CanReflect)
        {
            MaxReflectNum = context.maxReflectNum;
        }
        else
        {
            MaxReflectNum = 0;
        }

        ReflectCnt = 0;

        float rad = context.degree * Mathf.Deg2Rad;
        Dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Dir = Dir.normalized;

        transform.rotation.SetLookRotation(Dir);

        _steering = steering;

        _isInitialized = true;
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
        if (_isInitialized == false)
        {
            return;
        }

        float dt = Time.deltaTime;

        Dir = _steering.GetNextDirection(Dir, this);
        Velocity = Velocity + Acceleration * dt;
        transform.position += (Vector3)(Dir * Velocity * dt);
    }

    private void OnDisable()
    {
        _isInitialized = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Debug.Log("OnTrigger");

            if (CanReflect == false)
            {
                return;
            }

            if (ReflectCnt >= MaxReflectNum)
            {
                return;
            }

            Vector2 closest = collision.ClosestPoint(transform.position);
            Vector2 normal = ((Vector2)transform.position - closest).normalized;

            Dir = Vector2.Reflect(Dir, normal);
            ReflectCnt++;

            Debug.Log($"ReflectCnt : {ReflectCnt}");
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            _pool.Release(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            _pool.Release(this);
        }
    }
}
