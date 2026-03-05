
using UnityEngine;

public interface IBulletSteering
{
    Vector2 GetNextDirection(Vector2 curDir, Bullet bullet);
}