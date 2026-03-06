using UnityEngine;

public class CircleBarrage2 : IBarrage
{
    CircleEmitter _circleEmitter;

    public CircleBarrage2()
    {
        _circleEmitter = new CircleEmitter(20, 1);
    }

    public override void Execute(Vector3 origin)
    {
        BulletContext circleBulletcontext = new BulletContext(
            position: origin,
            velocity: 5f,
            acceleration: 0f,
            accelStartSecond: 0f,
            angularVelocityInit: 0f,
            angularAcceleration: 0f,
            curveStartSecond: 0f,
            canReflect: true,
            maxReflectNum: 3,
            degree: 0
        );

        _circleEmitter.Emit(circleBulletcontext);
    }
}
