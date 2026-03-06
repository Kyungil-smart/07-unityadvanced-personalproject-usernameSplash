using UnityEngine;

public class CurveBarrage1 : IBarrage
{
    CircleEmitter _circleEmitter;

    public CurveBarrage1()
    {
        _circleEmitter = new CircleEmitter(6, 0);
    }

    public override void Execute(Vector3 origin)
    {
        BulletContext circleBulletcontext = new BulletContext(
            position: origin,
            velocity: 5f,
            acceleration: 0f,
            accelStartSecond: 0f,
            angularVelocityInit: 45f,
            angularAcceleration: 0f,
            curveStartSecond: 0.5f,
            canReflect: false,
            maxReflectNum: 0,
            degree: 0
        );

        _circleEmitter.Emit(circleBulletcontext);
    }
}
