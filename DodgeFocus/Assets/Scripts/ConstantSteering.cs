
using UnityEngine;

public class ConstantSteering : IBulletSteering
{
    public Vector2 GetNextDirection(Vector2 curDir, Bullet bullet)
    {
        return Quaternion.Euler(0, 0, bullet.AngularVelocity * Time.deltaTime) * curDir;
    }
}