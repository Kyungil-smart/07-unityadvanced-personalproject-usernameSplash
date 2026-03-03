using UnityEngine;

public class HomingSteering : IBulletSteering
{
    Transform _target;
    float _maxTurnSpeed;
    bool _isInfinite;
    float _duration;
    float _elapsedTime;

    public void Init(Transform target, float maxTurnSpeed)
    {
        _target = target;
        _maxTurnSpeed = maxTurnSpeed;
        _isInfinite = true;
        _duration = 0;
    }
    public void Init(Transform target, float maxTurnSpeed, float duration)

    {
        _target = target;
        _maxTurnSpeed = maxTurnSpeed;
        _isInfinite = false;
        _duration = duration;
        _elapsedTime = 0;
    }

    public Vector2 GetNextDirection(Vector2 curDir, Bullet bullet)
    {
        if ((_isInfinite == false && _elapsedTime >= _duration) || _target == null)
        {
            return curDir;
        }

        _elapsedTime += Time.deltaTime;

        Vector2 toTarget = (_target.position - bullet.transform.position).normalized;

        float angle = Vector2.SignedAngle(curDir, toTarget);
        float maxRotate = _maxTurnSpeed * Time.deltaTime;
        float clampedAngle = Mathf.Clamp(angle, -maxRotate, maxRotate);

        Vector2 direction = Quaternion.Euler(0, 0, clampedAngle) * curDir;
        direction.Normalize();

        return direction;
    }
}