using UnityEngine;
using UnityEngine.Pool;

public struct BulletContext
{
    public Vector3 _position;
    public float _velocity;
    public float _acceleration;
    public float _accelStartSecond;
    public float _angularVelocity;
    public float _angularVelocityInit;
    public float _angularAcceleration;
    public float _curveStartSecond;
    public bool _canReflect;
    public int _maxReflectNum;
    public float _degree;
    public IBulletSteering _steering;

    public BulletContext(
        Vector3 position, float velocity, float acceleration, float accelStartSecond,
        float angularVelocityInit, float angularAcceleration, float curveStartSecond,
        bool canReflect, int maxReflectNum, float degree
    )
    {
        _position = position;
        _velocity = velocity;
        _acceleration = acceleration;
        _accelStartSecond = accelStartSecond;
        _angularVelocity = 0;
        _angularVelocityInit = angularVelocityInit;
        _angularAcceleration = angularAcceleration;
        _curveStartSecond = curveStartSecond;
        _canReflect = canReflect;
        _maxReflectNum = maxReflectNum;
        _degree = degree;
        _steering = new ConstantSteering();
    }

    public BulletContext(
        Vector3 position, float velocity, float acceleration, float accelStartSecond,
        float angularVelocityInit, float angularAcceleration, float curveStartSecond,
        bool canReflect, int maxReflectNum, float degree,
        IBulletSteering steering
    )
    {
        _position = position;
        _velocity = velocity;
        _acceleration = acceleration;
        _accelStartSecond = accelStartSecond;
        _angularVelocity = 0;
        _angularVelocityInit = angularVelocityInit;
        _angularAcceleration = angularAcceleration;
        _curveStartSecond = curveStartSecond;
        _canReflect = canReflect;
        _maxReflectNum = maxReflectNum;
        _degree = degree;
        _steering = steering;
    }

    public BulletContext(
        Vector3 position, float velocity, float acceleration, float accelStartSecond,
        float angularVelocityInit, float angularAcceleration, float curveStartSecond,
        bool canReflect, int maxReflectNum, Vector2 dir
    )
    {
        _position = position;
        _velocity = velocity;
        _acceleration = acceleration;
        _accelStartSecond = accelStartSecond;
        _angularVelocity = 0;
        _angularVelocityInit = angularVelocityInit;
        _angularAcceleration = angularAcceleration;
        _curveStartSecond = curveStartSecond;
        _canReflect = canReflect;
        _maxReflectNum = maxReflectNum;
        _degree = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _steering = new ConstantSteering();
    }
    public BulletContext(
        Vector3 position, float velocity, float acceleration, float accelStartSecond,
        float angularVelocityInit, float angularAcceleration, float curveStartSecond,
        bool canReflect, int maxReflectNum, Vector2 dir,
        IBulletSteering steering
    )
    {
        _position = position;
        _velocity = velocity;
        _acceleration = acceleration;
        _accelStartSecond = accelStartSecond;
        _angularVelocity = 0;
        _angularVelocityInit = angularVelocityInit;
        _angularAcceleration = angularAcceleration;
        _curveStartSecond = curveStartSecond;
        _canReflect = canReflect;
        _maxReflectNum = maxReflectNum;
        _degree = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _steering = steering;
    }

}

public class Bullet : MonoBehaviour
{
    private bool _isInitialized = false;

    private BulletContext _context;
    public BulletContext Context => _context;
    public Vector2 Dir { get; private set; }

    private int _reflectCnt = 0;
    private float _elapsedTime = 0.0f;

    ObjectPool<Bullet> _pool;

    public void Init(BulletContext context)
    {
        transform.position = context._position;

        _context = context;

        float rad = context._degree * Mathf.Deg2Rad;
        Dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
        Dir = Dir.normalized;

        transform.up = Dir;

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

        Dir = _context._steering.GetNextDirection(Dir, this);
        transform.up = Dir;

        if (_elapsedTime >= _context._accelStartSecond)
        {
            _context._velocity = _context._velocity + _context._acceleration * dt;
        }

        if (_elapsedTime >= _context._curveStartSecond)
        {
            if (_context._angularVelocity == 0.0f)
            {
                _context._angularVelocity = _context._angularVelocityInit;
            }
            _context._angularVelocity += _context._angularAcceleration;
        }

        _elapsedTime += dt;

        transform.position += (Vector3)(Dir * _context._velocity * dt);
    }

    private void OnDisable()
    {
        _isInitialized = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            if (_context._canReflect == false)
            {
                return;
            }

            if (_reflectCnt >= _context._maxReflectNum)
            {
                return;
            }

            Vector2 closest = collision.ClosestPoint(transform.position);
            Vector2 normal = ((Vector2)transform.position - closest).normalized;

            Dir = Vector2.Reflect(Dir, normal);
            _reflectCnt++;
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
