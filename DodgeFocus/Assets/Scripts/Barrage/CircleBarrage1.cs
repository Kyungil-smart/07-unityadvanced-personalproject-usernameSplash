using UnityEngine;

public class CircleBarrage1 : IBarrage
{
    CircleEmitter _circleEmitter;

    public CircleBarrage1()
    {
        _circleEmitter = new CircleEmitter(20, 0);
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
            canReflect: false,
            maxReflectNum: 0,
            degree: 0
        );

        _circleEmitter.Emit(circleBulletcontext);
    }
}
